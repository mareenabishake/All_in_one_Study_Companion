using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;

namespace All_in_one_Study_Companion.Pages.QnA
{
    public partial class Answer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int questionId;
                    if (int.TryParse(Request.QueryString["id"], out questionId))
                    {
                        QuestionID.Value = questionId.ToString();
                        LoadQuestion(questionId);
                    }
                    else
                    {
                        // Handle invalid question ID
                        Response.Redirect("~/Pages/QnA/QnA.aspx");
                    }
                }
                else
                {
                    // No question ID provided
                    Response.Redirect("~/Pages/QnA/QnA.aspx");
                }
            }
        }

        private void LoadQuestion(int questionId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StudyCompanionDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT QuestionText FROM Questions WHERE QuestionID = @QuestionID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@QuestionID", questionId);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        QuestionText.Text = result.ToString();
                    }
                    else
                    {
                        // Question not found
                        Response.Redirect("~/Pages/QnA/QnA.aspx");
                    }
                }
            }
        }

        protected void ConfirmAnswer_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Pages/Account/LandIn.aspx");
                return;
            }

            int userId = Convert.ToInt32(Session["UserID"]);
            int questionId = Convert.ToInt32(QuestionID.Value);
            string answerText = AnswerText.Text;
            string imagePath = null;

            if (AnswerFileUpload.HasFile)
            {
                string fileName = Path.GetFileName(AnswerFileUpload.FileName);
                string extension = Path.GetExtension(fileName);
                string uniqueFileName = $"{Guid.NewGuid()}{extension}";
                string uploadPath = Server.MapPath("~/Uploads/AnswerImages/");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                string filePath = Path.Combine(uploadPath, uniqueFileName);
                AnswerFileUpload.SaveAs(filePath);
                imagePath = $"~/Uploads/AnswerImages/{uniqueFileName}";
            }

            SaveAnswerToDatabase(userId, questionId, answerText, imagePath);

            // Redirect back to the QnA page
            Response.Redirect("~/Pages/QnA/QnA.aspx");
        }

        private void SaveAnswerToDatabase(int userId, int questionId, string answerText, string imagePath)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StudyCompanionDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Answers (QuestionID, AnswerText, ImagePath, UserID) 
                                 VALUES (@QuestionID, @AnswerText, @ImagePath, @UserID)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@QuestionID", questionId);
                    command.Parameters.AddWithValue("@AnswerText", string.IsNullOrEmpty(answerText) ? (object)DBNull.Value : answerText);
                    command.Parameters.AddWithValue("@ImagePath", string.IsNullOrEmpty(imagePath) ? (object)DBNull.Value : imagePath);
                    command.Parameters.AddWithValue("@UserID", userId);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your answer has been submitted successfully!');", true);
                    }
                    catch (Exception ex)
                    {
                        // Log the exception
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('An error occurred while submitting your answer. Please try again.');", true);
                    }
                }
            }
        }
    }
}