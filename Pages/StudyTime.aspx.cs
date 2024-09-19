using System;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace All_in_one_Study_Companion.Pages
{
    public partial class StudyTime : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user is logged in
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Pages/Account/LandIn.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    // Get user ID from session
                    int userId = Convert.ToInt32(Session["UserID"]);
                    // Get user subjects and serialize to JSON for client-side use
                    List<string> subjects = GetUserSubjects(userId);
                    string subjectsJson = new JavaScriptSerializer().Serialize(subjects);
                    ClientScript.RegisterStartupScript(this.GetType(), "subjectsArray", $"var userSubjects = {subjectsJson};", true);
                }
            }
        }

        // Method to get user subjects from the database
        private List<string> GetUserSubjects(int userId)
        {
            List<string> subjects = new List<string>();
            string connectionString = ConfigurationManager.ConnectionStrings["StudyCompanionDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT DISTINCT SubjectName FROM ExamMarks WHERE UserID = @UserID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                subjects.Add(reader["SubjectName"].ToString());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }
            }
            return subjects;
        }

        // Web method to save study record
        [WebMethod]
        public static bool SaveStudyRecord(string subject, double duration)
        {
            var context = HttpContext.Current;
            if (context == null || context.Session["UserID"] == null)
            {
                return false;
            }

            int userId = Convert.ToInt32(context.Session["UserID"]);
            string connectionString = ConfigurationManager.ConnectionStrings["StudyCompanionDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO StudyTimeRecords (UserID, SubjectName, Time) VALUES (@UserID, @SubjectName, @Time)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId);
                    command.Parameters.AddWithValue("@SubjectName", subject);
                    command.Parameters.AddWithValue("@Time", duration);

                    try
                    {
                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                    catch (Exception ex)
                    {
                        // Log the exception
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        return false;
                    }
                }
            }
        }
    }
}