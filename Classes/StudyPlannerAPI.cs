using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq; // For LINQ methods like Select

namespace All_in_one_Study_Companion.Classes
{
    public class StudyPlannerAPI
    {
        private readonly string _connectionString;

        public StudyPlannerAPI(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<string> GetStudyRecommendations(int userId)
        {
            var studyData = new StudyData();

            try
            {
                studyData.pastRecords = await FetchPastRecords(userId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching past records: {ex.Message}");
                studyData.pastRecords = new List<StudyTimeRecord>(); // Fallback to empty list
            }

            try
            {
                studyData.ExamMarks = await FetchExamMarks(userId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching exam marks: {ex.Message}");
                studyData.ExamMarks = new List<ExamMark>(); // Fallback to empty list
            }

            try
            {
                // Fetch only future available study time slots
                studyData.AvailableStudyTime = (await FetchAvailableStudyTime(userId))
                    .Where(t => t.StartTime > DateTime.Now) // Filter for future slots
                    .ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching available study time: {ex.Message}");
                studyData.AvailableStudyTime = new List<TimeSlot>(); // Fallback to empty list
            }

            // Prepare the LLM query with whatever data is available
            var llmQuery = PrepareQueryForLLM(studyData);
    
            // Log the LLM query
            System.Diagnostics.Debug.WriteLine("LLM Query:");
            System.Diagnostics.Debug.WriteLine(llmQuery);

            return await LLM.QueryGroqAsync(llmQuery);
        }

        private async Task<StudyData> FetchStudyData(int userId)
        {
            return new StudyData
            {
                pastRecords = await FetchPastRecords(userId),
                ExamMarks = await FetchExamMarks(userId),
                AvailableStudyTime = await FetchAvailableStudyTime(userId)
            };
        }

        private string PrepareQueryForLLM(StudyData data)
        {
            var formattedPastRecords = data.pastRecords.Select(r => new
            {
                SubjectName = r.SubjectName,
                TotalTime = r.TotalTime
            }).ToList(); // Ensure this is a list

            var formattedExamMarks = data.ExamMarks.Select(m => new
            {
                SubjectName = m.SubjectName,
                CurrentMark = m.CurrentMark,
                TargetMark = m.TargetMark
            }).ToList(); // Ensure this is a list

            var formattedAvailableStudyTime = data.AvailableStudyTime.Select(t => new
            {
                SlotID = t.SlotID,
                StartTime = t.StartTime,
                EndTime = t.EndTime,
                SubjectName = t.SubjectName
            }).ToList(); // Ensure this is a list

            return $@"Based on the following study data, recommend subjects for each available time slot:

Past Study Records (Total hours per subject):
{JsonConvert.SerializeObject(formattedPastRecords, Formatting.Indented)}

Exam Marks:
{JsonConvert.SerializeObject(formattedExamMarks, Formatting.Indented)}

Available Study Time:
{JsonConvert.SerializeObject(formattedAvailableStudyTime, Formatting.Indented)}

Instructions:
1. Analyze the provided data to determine which subjects need the most attention.
2. For each available time slot, assign a subject to study.
3. Provide your recommendations in the following JSON format:

[
  {{
    ""SlotID"": [SlotID],
    ""SubjectName"": ""[Subject to study]"" 
  }},
  ...
]

Ensure that:
- Each SlotID from the Available Study Time is assigned a subject.
- Subjects are distributed based on their need for improvement and current study time.
- The output is valid JSON that can be directly parsed and used to update the TimeSlots table.

Do not include any explanations or additional text in your response. Your response should only include the JSON output as specified above.";
        }

        private async Task<List<StudyTimeRecord>> FetchPastRecords(int userId)
        {
            var pastRecords = new List<StudyTimeRecord>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    SELECT SubjectName, CAST(SUM(Time) AS INT) AS TotalTime
                    FROM StudyTimeRecords
                    WHERE UserID = @UserID
                    GROUP BY SubjectName";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId); // Set the parameter

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            try
                            {

                                pastRecords.Add(new StudyTimeRecord
                                {
                                    SubjectName = reader.GetString(0),
                                    TotalTime = reader.GetInt32(1),
                                });
                            }
                            catch (InvalidCastException ex)
                            {
                                Console.WriteLine(pastRecords);
                                System.Diagnostics.Debug.WriteLine($"InvalidCastException: {ex.Message}");
                                System.Diagnostics.Debug.WriteLine($"Data read - SubjectName: {reader[0]}, TotalTime: {reader[1]}");
                            }
                        }
                    }
                }
            }
            return pastRecords;
        }
        private async Task<List<ExamMark>> FetchExamMarks(int userId)
        {
            var examMarks = new List<ExamMark>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    SELECT UserID, SubjectName, CurrentMark, TargetMark 
                    FROM ExamMarks 
                    WHERE UserID = @UserID"; // Filter by UserID

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId); // Set the parameter

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            examMarks.Add(new ExamMark
                            {
                                UserID = reader.GetInt32(0),
                                SubjectName = reader.GetString(1),
                                CurrentMark = reader.GetInt32(2),
                                TargetMark = reader.GetInt32(3)
                            });
                        }
                    }
                }
            }
            return examMarks;
        }
        private async Task<List<TimeSlot>> FetchAvailableStudyTime(int userId)
        {
            var studySlots = new List<TimeSlot>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    SELECT SlotID, UserID, SlotDate, StartTime, EndTime, SubjectName
                    FROM TimeSlots
                    WHERE UserID = @UserID AND SubjectName IS NULL
                    ORDER BY SlotDate, StartTime";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId); // Set the parameter

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            studySlots.Add(new TimeSlot
                            {
                                SlotID = reader.GetInt32(0),
                                UserID = reader.GetInt32(1),
                                SlotDate = reader.GetDateTime(2),
                                StartTime = reader.GetDateTime(3),
                                EndTime = reader.GetDateTime(4),
                                SubjectName = reader.IsDBNull(5) ? null : reader.GetString(5)
                            });
                        }
                    }
                }
            }
            return studySlots;
        }
        
    }

    public class TimeSlotAssignment
    {
        public int SlotID { get; set; }
        public string SubjectName { get; set; }
    }

    // Data models
    public class StudyData
    {
        public List<StudyTimeRecord> pastRecords { get; set; }
        public List<ExamMark> ExamMarks { get; set; }
        public List<TimeSlot> AvailableStudyTime { get; set; }
    }

    public class StudyTimeRecord
    {
        public string SubjectName { get; set; }
        public float TotalTime { get; set; }
    }

    public class ExamMark
    {
        public int UserID { get; set; }
        public string SubjectName { get; set; }
        public int CurrentMark { get; set; }
        public int TargetMark { get; set; }
    }

    public class TimeSlot
    {
        public int SlotID { get; set; }
        public int UserID { get; set; }
        public DateTime SlotDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string SubjectName { get; set; }
    }
}