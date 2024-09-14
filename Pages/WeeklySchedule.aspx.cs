using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace All_in_one_Study_Companion.Pages
{
    public partial class WeeklySchedule : System.Web.UI.Page
    {
        private List<TimeSlot> timeSlots = new List<TimeSlot>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateDropDownLists();
            }
            GenerateWeeklyCalendar();
        }

        private void PopulateDropDownLists()
        {
            DateTime today = DateTime.Today;
            for (int i = 0; i < 7; i++)
            {
                DateTime day = today.AddDays(i);
                DayDropDownList.Items.Add(new ListItem(day.ToString("ddd MM/dd"), day.ToString("yyyy-MM-dd")));
            }

            for (int i = 4; i < 24; i++)
            {
                string time = $"{i % 24:D2}:00";
                StartTimeDropDownList.Items.Add(time);
                EndTimeDropDownList.Items.Add(time);
            }
        }

        protected void AddTimeSlotButton_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = DateTime.ParseExact(DayDropDownList.SelectedValue, "yyyy-MM-dd", null);
            DateTime startTime = selectedDate.Add(TimeSpan.Parse(StartTimeDropDownList.SelectedValue));
            DateTime endTime = selectedDate.Add(TimeSpan.Parse(EndTimeDropDownList.SelectedValue));

            int userId = GetUserIdFromSession();

            InsertTimeSlot(userId, selectedDate, startTime, endTime);

            GenerateWeeklyCalendar();
        }

        private void InsertTimeSlot(int userId, DateTime slotDate, DateTime startTime, DateTime endTime)
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
                        connection.Open();
                        int result = command.ExecuteNonQuery();
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

        private int GetUserIdFromSession()
        {
            // Implement this method to retrieve the UserID from the session
            // For example:
            // return Convert.ToInt32(Session["UserID"]);
            return 1; // Placeholder, replace with actual implementation
        }

        private void GenerateWeeklyCalendar()
        {
            DateTime today = DateTime.Today;

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='weekly-schedule-section'>");
            sb.Append("<h1>Weekly Schedule</h1>");
            sb.Append("<div class='table-container'>");
            sb.Append("<table class='weekly-schedule'>");
            sb.Append("<tr><th class='time-header'>Time</th>");

            for (int day = 0; day < 7; day++)
            {
                DateTime currentDay = today.AddDays(day);
                string dayClass = day == 0 ? "current-day" : "";
                sb.Append($"<th class='day-header {dayClass}'>{currentDay.ToString("ddd MM/dd")}</th>");
            }
            sb.Append("</tr>");

            for (int hour = 4; hour < 24; hour++)
            {
                sb.Append("<tr>");
                sb.Append($"<td class='time-cell'>{hour % 24:D2}:00</td>");

                for (int day = 0; day < 7; day++)
                {
                    DateTime currentDay = today.AddDays(day);
                    string cellContent = GetTimeSlotsForDateAndTime(currentDay, hour % 24);
                    sb.Append($"<td class='time-slot-cell'>{cellContent}</td>");
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");
            sb.Append("</div>");
            sb.Append("</div>");

            WeeklyCalendarLiteral.Text = sb.ToString();
        }

        private string GetTimeSlotsForDateAndTime(DateTime date, int hour)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StudyCompanionDB"].ConnectionString;
            List<TimeSlot> timeSlots = new List<TimeSlot>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT UserId, SlotDate, StartTime, EndTime, SubjectName 
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
                                    UserId = (int)reader["UserId"],
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
                        // Log the exception
                        return $"Error: {ex.Message}";
                    }
                }
            }

            if (timeSlots.Any())
            {
                var slot = timeSlots.First(); // Assuming we're only displaying one slot per cell
                if (!string.IsNullOrEmpty(slot.SubjectName))
                {
                    return $"<div class='booked-slot'><span class='subject-name'>{slot.SubjectName}</span></div>";
                }
                else
                {
                    return "<div class='booked-slot'></div>";
                }
            }
            return "&nbsp;";
        }
    }

    public class TimeSlot
    {
        public int UserId { get; set; }
        public DateTime SlotDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string SubjectName { get; set; }
    }
}