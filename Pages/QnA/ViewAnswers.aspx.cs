using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace All_in_one_Study_Companion.Pages.QnA
{
    public partial class ViewAnswers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if a question ID is provided in the query string
                if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out int questionId))
                {
                    // Load the question and its answers
                    LoadQuestionAndAnswers(questionId);
                }
                else
                {
                    // Redirect to the Q&A page if no valid question ID is provided
                    Response.Redirect("~/Pages/QnA/QnA.aspx");
                }
            }
        }

        private void LoadQuestionAndAnswers(int questionId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StudyCompanionDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Load question
                string questionQuery = "SELECT QuestionText FROM Questions WHERE QuestionID = @QuestionID";
                using (SqlCommand command = new SqlCommand(questionQuery, connection))
                {
                    command.Parameters.AddWithValue("@QuestionID", questionId);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        QuestionText.Text = result.ToString();
                    }
                    else
                    {
                        // Redirect to the Q&A page if the question is not found
                        Response.Redirect("~/Pages/QnA/QnA.aspx");
                    }
                }

                // Load answers
                string answersQuery = @"
                    SELECT a.AnswerText, a.ImagePath, u.UserName
                    FROM Answers a
                    JOIN Users u ON a.UserID = u.UserID
                    WHERE a.QuestionID = @QuestionID
                    ORDER BY a.AnswerID DESC";  // Assuming you want the newest answers first

                using (SqlCommand command = new SqlCommand(answersQuery, connection))
                {
                    command.Parameters.AddWithValue("@QuestionID", questionId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Bind the answers to the Repeater control
                        AnswersRepeater.DataSource = reader;
                        AnswersRepeater.DataBind();
                    }
                }
            }
        }
    }
}