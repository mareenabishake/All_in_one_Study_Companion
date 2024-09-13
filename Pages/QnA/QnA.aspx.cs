using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Services;
using System.Web.Script.Serialization;

namespace All_in_one_Study_Companion.Pages.QnA
{
    public partial class QnA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] != null)
                {
                    int userId = Convert.ToInt32(Session["UserID"]);
                    SetUserAcademicLevel(userId);
                    PopulateSubjectDropdown(userId);
                    SetDefaultFilterAcademicLevel(userId);
                }
                else
                {
                    Response.Redirect("~/Pages/Account/LandIn.aspx");
                }
                LoadQuestions();
            }
        }

        private void SetUserAcademicLevel(int userId)
        {
            string userAcademicLevel = GetUserAcademicLevel(userId);
            academicLevel.Text = string.IsNullOrEmpty(userAcademicLevel) ? "Not available" : userAcademicLevel;
        }

        private void SetDefaultFilterAcademicLevel(int userId)
        {
            string userAcademicLevel = GetUserAcademicLevel(userId);
            if (!string.IsNullOrEmpty(userAcademicLevel))
            {
                filterAcademicLevel.SelectedValue = userAcademicLevel;
            }
        }

        private string GetUserAcademicLevel(int userId)
        {
            string academicLevel = "";
            string connectionString = ConfigurationManager.ConnectionStrings["StudyCompanionDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT AcademicLevel FROM Users WHERE UserID = @UserID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId);
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            academicLevel = result.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }
            }
            return academicLevel;
        }

        private void PopulateSubjectDropdown(int userId)
        {
            List<string> subjects = GetUserSubjects(userId);
            subjectArea.DataSource = subjects;
            subjectArea.DataBind();

            if (subjects.Count == 0)
            {
                subjectArea.Items.Insert(0, new ListItem("No subjects available", ""));
            }
            else
            {
                subjectArea.Items.Insert(0, new ListItem("Select a subject", ""));
            }
        }

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
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            subjects.Add(reader["SubjectName"].ToString());
                        }
                    }
                }
            }
            return subjects;
        }

        protected void FilterAcademicLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadQuestions();
        }

        protected void FilterSubjectArea_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = filterSubjectArea.Text.Trim();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                List<string> suggestions = GetSubjectSuggestions(searchTerm);
                subjectSuggestions.DataSource = suggestions;
                subjectSuggestions.DataBind();
                subjectSuggestions.Visible = true;
            }
            else
            {
                subjectSuggestions.Visible = false;
            }
        }

        protected void SubjectSuggestion_Selected(object sender, EventArgs e)
        {
            if (subjectSuggestions.SelectedItem != null)
            {
                filterSubjectArea.Text = subjectSuggestions.SelectedItem.Text;
                subjectSuggestions.Visible = false;
                LoadQuestions();
            }
        }

        private List<string> GetSubjectSuggestions(string searchTerm)
        {
            List<string> suggestions = new List<string>();
            string connectionString = ConfigurationManager.ConnectionStrings["StudyCompanionDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT DISTINCT SubjectName FROM Questions WHERE SubjectName LIKE @SearchTerm + '%'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SearchTerm", searchTerm);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suggestions.Add(reader["SubjectName"].ToString());
                        }
                    }
                }
            }
            return suggestions;
        }

        private void LoadQuestions()
        {
            string academicLevel = filterAcademicLevel.SelectedValue;
            string subjectArea = filterSubjectArea.Text.Trim();

            string connectionString = ConfigurationManager.ConnectionStrings["StudyCompanionDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT q.*, 
                                 (SELECT COUNT(*) FROM Answers a WHERE a.QuestionID = q.QuestionID) AS AnswerCount
                                 FROM Questions q
                                 WHERE (@AcademicLevel = '' OR q.AcademicLevel = @AcademicLevel)
                                 AND (@SubjectArea = '' OR q.SubjectName LIKE '%' + @SubjectArea + '%')";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AcademicLevel", academicLevel);
                    command.Parameters.AddWithValue("@SubjectArea", subjectArea);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        questionList.InnerHtml = ""; // Clear existing questions
                        while (reader.Read())
                        {
                            int answerCount = Convert.ToInt32(reader["AnswerCount"]);
                            string viewAnswersButton = answerCount > 0 
                                ? $"<a href='ViewAnswers.aspx?id={reader["QuestionID"]}' class='view-answers-button'>View Answers ({answerCount})</a>" 
                                : "";

                            string questionHtml = $@"
                                <li>
                                    <div class='question-header'>
                                        <h3>{reader["QuestionText"]}</h3>
                                        <span class='question-id'>ID: {reader["QuestionID"]}</span>
                                    </div>
                                    <p><strong>Level:</strong> {reader["AcademicLevel"]} | <strong>Subject:</strong> {reader["SubjectName"]}</p>
                                    <div class='button-container'>
                                        <a href='Answer.aspx?id={reader["QuestionID"]}' class='answer-button'>Answer</a>
                                        {viewAnswersButton}
                                    </div>
                                </li>";
                            questionList.InnerHtml += questionHtml;
                        }
                    }
                }
            }
        }

        protected void SubmitQuestion_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Pages/Account/LandIn.aspx");
                return;
            }

            int userId = Convert.ToInt32(Session["UserID"]);
            string selectedSubject = subjectArea.SelectedValue;
            string questionText = question.Text;
            string imagePath = null;
            string userAcademicLevel = academicLevel.Text;

            if (imageUpload.HasFile)
            {
                string fileName = Path.GetFileName(imageUpload.FileName);
                string extension = Path.GetExtension(fileName);
                string uniqueFileName = $"{Guid.NewGuid()}{extension}";
                string uploadPath = Server.MapPath("~/Uploads/QuestionImages/");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                string filePath = Path.Combine(uploadPath, uniqueFileName);
                imageUpload.SaveAs(filePath);
                imagePath = $"~/Uploads/QuestionImages/{uniqueFileName}";
            }

            SaveQuestionToDatabase(userId, selectedSubject, questionText, imagePath, userAcademicLevel);

            ClearForm();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your question has been submitted successfully!');", true);
        }

        private void SaveQuestionToDatabase(int userId, string subjectName, string questionText, string imagePath, string academicLevel)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StudyCompanionDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Questions (UserID, SubjectName, QuestionText, ImagePath, AcademicLevel) 
                                 VALUES (@UserID, @SubjectName, @QuestionText, @ImagePath, @AcademicLevel)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId);
                    command.Parameters.AddWithValue("@SubjectName", subjectName);
                    command.Parameters.AddWithValue("@QuestionText", questionText);
                    command.Parameters.AddWithValue("@ImagePath", (object)imagePath ?? DBNull.Value);
                    command.Parameters.AddWithValue("@AcademicLevel", academicLevel);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('An error occurred while submitting your question. Please try again.');", true);
                    }
                }
            }
        }

        private void ClearForm()
        {
            question.Text = string.Empty;
            subjectArea.SelectedIndex = 0;
        }

        [WebMethod]
        public static string GetFilteredQuestions(string academicLevel, string subjectArea)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StudyCompanionDB"].ConnectionString;
            List<object> questions = new List<object>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT q.*, 
                           (SELECT COUNT(*) FROM Answers a WHERE a.QuestionID = q.QuestionID) AS AnswerCount
                    FROM Questions q
                    WHERE (@AcademicLevel = '' OR q.AcademicLevel = @AcademicLevel)
                    AND (@SubjectArea = '' OR q.SubjectName LIKE '%' + @SubjectArea + '%')";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AcademicLevel", academicLevel);
                    command.Parameters.AddWithValue("@SubjectArea", subjectArea);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            questions.Add(new
                            {
                                QuestionID = reader["QuestionID"],
                                QuestionText = reader["QuestionText"].ToString(),
                                AcademicLevel = reader["AcademicLevel"].ToString(),
                                SubjectName = reader["SubjectName"].ToString(),
                                AnswerCount = Convert.ToInt32(reader["AnswerCount"])
                            });
                        }
                    }
                }
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(questions);
        }
    }
}