using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using All_in_one_Study_Companion.Classes; // Make sure this namespace is correct
using System.Threading.Tasks;

namespace All_in_one_Study_Companion.Pages
{
    public partial class WeeklySchedule : System.Web.UI.Page
    {
        private StudyPlannerAPI studyPlanner;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["StudyCompanionDB"].ConnectionString;
                studyPlanner = new StudyPlannerAPI(connectionString);

                if (!IsPostBack)
                {
                    // Generate the weekly calendar
                    GenerateWeeklyCalendar();
                    // Populate dropdown lists on initial page load
                    PopulateDropDownLists();
                }
            }
            else
            {
                // Redirect to login page if not logged in
                Response.Redirect("~/Pages/Account/LandIn.aspx");
            }

        }

        // Method to populate day and time dropdown lists
        private void PopulateDropDownLists()
        {
            DateTime today = DateTime.Today;
            // Populate day dropdown for the next 7 days
            for (int i = 0; i < 7; i++)
            {
                DateTime day = today.AddDays(i);
                DayDropDownList.Items.Add(new ListItem(day.ToString("ddd MM/dd"), day.ToString("yyyy-MM-dd")));
            }

            // Populate time dropdowns from 4 AM to 11 PM
            for (int i = 4; i < 24; i++)
            {
                string time = $"{i % 24:D2}:00";
                StartTimeDropDownList.Items.Add(time);
                EndTimeDropDownList.Items.Add(time);
            }
        }

        // Event handler for adding a new time slot
        protected async void AddTimeSlotButton_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = DateTime.ParseExact(DayDropDownList.SelectedValue, "yyyy-MM-dd", null);
            DateTime startTime = selectedDate.Add(TimeSpan.Parse(StartTimeDropDownList.SelectedValue));
            DateTime endTime = selectedDate.Add(TimeSpan.Parse(EndTimeDropDownList.SelectedValue));
            int userId = Convert.ToInt32(Session["UserID"]);

            // Insert the new time slot into the database
            await InsertTimeSlot(userId, selectedDate, startTime, endTime);

            // Get study recommendations and update time slots
            await UpdateStudyRecommendations(userId);

            // Regenerate the weekly calendar to reflect changes
            GenerateWeeklyCalendar();
        }

        private async Task InsertTimeSlot(int userId, DateTime slotDate, DateTime startTime, DateTime endTime)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StudyCompanionDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO TimeSlots (UserId, SlotDate, StartTime, EndTime) 
                                 VALUES (@UserId, @SlotDate, @StartTime, @EndTime)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@SlotDate", slotDate.Date);
                    command.Parameters.AddWithValue("@StartTime", startTime);
                    command.Parameters.AddWithValue("@EndTime", endTime);

                    try
                    {
                        await connection.OpenAsync();
                        int result = await command.ExecuteNonQueryAsync();
                        if (result < 0)
                        {
                            // Handle the error, perhaps show a message to the user
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Failed to add time slot.');", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception and show an error message
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('An error occurred: {ex.Message}');", true);
                    }
                }
            }
        }

        private async Task UpdateStudyRecommendations(int userId)
        {
            try
            {
                string recommendations = await studyPlanner.GetStudyRecommendations(userId);
                
                // Log the recommendations received from the LLM
                System.Diagnostics.Debug.WriteLine($"Recommendations received from LLM: {recommendations}");

                if (string.IsNullOrEmpty(recommendations))
                {
                    throw new Exception("Received empty recommendations from StudyPlanner.");
                }

                await studyPlanner.AssignSubjectsToTimeSlots(recommendations);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception in UpdateStudyRecommendations: {ex}");
                string errorMessage = ex.InnerException != null ? 
                    $"{ex.Message} Inner Exception: {ex.InnerException.Message}" : 
                    ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", 
                    $"alert('Failed to update study recommendations. Error: {errorMessage}');", true);
            }
        }

        // Existing GenerateWeeklyCalendar method remains unchanged

        private string GetTimeSlotsForDateAndTime(DateTime date, int hour)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StudyCompanionDB"].ConnectionString;
            List<TimeSlot> timeSlots = new List<TimeSlot>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT SlotID, UserID, SlotDate, StartTime, EndTime, SubjectName 
                                 FROM TimeSlots 
                                 WHERE SlotDate = @SlotDate 
                                 AND DATEPART(HOUR, StartTime) <= @Hour 
                                 AND DATEPART(HOUR, EndTime) > @Hour";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SlotDate", date.Date);
                    command.Parameters.AddWithValue("@Hour", hour);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                timeSlots.Add(new TimeSlot
                                {
                                    SlotID = (int)reader["SlotID"],
                                    UserID = (int)reader["UserID"],
                                    SlotDate = (DateTime)reader["SlotDate"],
                                    StartTime = (DateTime)reader["StartTime"],
                                    EndTime = (DateTime)reader["EndTime"],
                                    SubjectName = reader["SubjectName"] as string
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return $"Error: {ex.Message}";
                    }
                }
            }

            if (timeSlots.Any())
            {
                StringBuilder slotHtml = new StringBuilder();
                foreach (var slot in timeSlots)
                {
                    string subjectName = !string.IsNullOrEmpty(slot.SubjectName) ? slot.SubjectName : "Free";
                    slotHtml.Append($"<div class='booked-slot'><span class='subject-name'>{subjectName}</span></div>");
                }
                return slotHtml.ToString();
            }
            return "<div class='booked-slot'><span class='subject-name'>Free</span></div>";
        }

        private void GenerateWeeklyCalendar()
        {
            StringBuilder calendarHtml = new StringBuilder();
            DateTime startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);

            calendarHtml.Append("<table class='weekly-schedule'>");
            calendarHtml.Append("<tr><th class='time-header'>Time</th>");

            for (int i = 0; i < 7; i++)
            {
                DateTime day = startOfWeek.AddDays(i);
                calendarHtml.Append($"<th class='day-header'>{day.ToString("ddd MM/dd")}</th>");
            }
            calendarHtml.Append("</tr>");

            for (int hour = 4; hour < 24; hour++)
            {
                calendarHtml.Append("<tr>");
                calendarHtml.Append($"<td class='time-cell'>{hour:D2}:00</td>");

                for (int i = 0; i < 7; i++)
                {
                    DateTime day = startOfWeek.AddDays(i);
                    string cellContent = GetTimeSlotsForDateAndTime(day, hour);
                    calendarHtml.Append($"<td class='event-cell'>{cellContent}</td>");
                }

                calendarHtml.Append("</tr>");
            }

            calendarHtml.Append("</table>");

            WeeklyCalendarLiteral.Text = calendarHtml.ToString();
        }
    }
}