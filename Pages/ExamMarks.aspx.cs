using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace All_in_one_Study_Companion.Pages
{
    public partial class ExamMarks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load exam marks on initial page load
                LoadExamMarks();
            }
        }

        // Method to load exam marks from the database
        private void LoadExamMarks()
        {
            try
            {
                string connectionString = GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new ConfigurationErrorsException("Connection string 'DefaultConnection' not found in web.config");
                }

                int userId = GetUserIdFromSession();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT SubjectName, CurrentMark, TargetMark FROM ExamMarks WHERE UserID = @UserID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        // Bind exam marks to the GridView
                        ExamMarksGridView.DataSource = dt;
                        ExamMarksGridView.DataBind();
                        NoDataLabel.Visible = false;
                    }
                    else
                    {
                        // Display message if no exam marks are available
                        ExamMarksGridView.DataSource = null;
                        ExamMarksGridView.DataBind();
                        NoDataLabel.Text = "No exam marks available.";
                        NoDataLabel.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Error loading exam marks: {ex.Message}");
            }
        }

        // Event handler for save marks button click
        protected void SaveMarks_Click(object sender, EventArgs e)
        {
            try
            {
                string subjectName = SubjectName.Text;
                int currentMark = Convert.ToInt32(CurrentMark.Text);
                int targetMark = Convert.ToInt32(TargetMark.Text);
                int userId = GetUserIdFromSession();

                string connectionString = GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new ConfigurationErrorsException("Connection string 'DefaultConnection' not found in web.config");
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO ExamMarks (UserID, SubjectName, CurrentMark, TargetMark) VALUES (@UserID, @SubjectName, @CurrentMark, @TargetMark)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@SubjectName", subjectName);
                    cmd.Parameters.AddWithValue("@CurrentMark", currentMark);
                    cmd.Parameters.AddWithValue("@TargetMark", targetMark);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                // Clear input fields
                SubjectName.Text = "";
                CurrentMark.Text = "";
                TargetMark.Text = "";

                // Reload the GridView
                LoadExamMarks();

                ShowMessage("Marks saved successfully!");
            }
            catch (Exception ex)
            {
                ShowMessage($"Error saving marks: {ex.Message}");
            }
        }

        // Method to get connection string from web.config
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["StudyCompanionDB"]?.ConnectionString;
        }

        // Method to get user ID from session
        private int GetUserIdFromSession()
        {
            if (Session["UserID"] == null)
            {
                throw new InvalidOperationException("User is not logged in.");
            }
            return Convert.ToInt32(Session["UserID"]);
        }

        // Method to show message to the user
        private void ShowMessage(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowMessage", 
                $"alert('{message.Replace("'", "\\'")}')", true);
        }

    }
}
