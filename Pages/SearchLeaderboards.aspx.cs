using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace All_in_one_Study_Companion.Pages
{
    public partial class SearchLeaderboards : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load leaderboard data on initial page load
                LoadLeaderboardData();
            }
        }

        // Method to load leaderboard data from the database
        private void LoadLeaderboardData()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["StudyCompanionDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT u.UserID, u.Username, l.Points, l.BadgeIMG, l.QuestionsAnswered, l.HoursStudied
                                 FROM Users u
                                 JOIN Leaderboard l ON u.UserID = l.UserID
                                 ORDER BY l.Points DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Bind leaderboard data to the repeater control
                    LeaderboardRepeater.DataSource = dt;
                    LeaderboardRepeater.DataBind();
                }
            }
        }

        // Event handler for search button click
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            string searchTerm = SearchBox.Text.Trim();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Perform search in the database
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["StudyCompanionDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT u.FullName, u.Institution, u.AcademicLevel, u.Email, 
                                     l.Points, l.BadgeIMG, l.QuestionsAnswered, l.HoursStudied
                                     FROM Users u
                                     JOIN Leaderboard l ON u.UserID = l.UserID
                                     WHERE u.FullName LIKE @SearchTerm";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        // Build HTML for search results
                        StringBuilder sb = new StringBuilder();
                        while (reader.Read())
                        {
                            sb.Append("<div class='user-profile'>");
                            sb.Append($"<div class='profile-header'>");
                            sb.Append($"<h3>{reader["FullName"]}</h3>");
                            sb.Append($"<img src='{reader["BadgeIMG"]}' alt='Badge' class='badge-icon' />");
                            sb.Append("</div>");
                            sb.Append($"<p><strong>Institution:</strong> {reader["Institution"]}</p>");
                            sb.Append($"<p><strong>Academic Level:</strong> {reader["AcademicLevel"]}</p>");
                            sb.Append($"<p><strong>Email:</strong> {reader["Email"]}</p>");
                            sb.Append("<div class='stats'>");
                            sb.Append($"<div class='stat-item'><span class='stat-label'>Points</span><span class='stat-value'>{reader["Points"]}</span></div>");
                            sb.Append($"<div class='stat-item'><span class='stat-label'>Questions</span><span class='stat-value'>{reader["QuestionsAnswered"]}</span></div>");
                            sb.Append($"<div class='stat-item'><span class='stat-label'>Hours</span><span class='stat-value'>{reader["HoursStudied"]}</span></div>");
                            sb.Append("</div>");
                            sb.Append("</div>");
                        }

                        // Display search results
                        searchResults.InnerHtml = sb.ToString();
                    }
                }
            }
            else
            {
                // Clear search results if search term is empty
                searchResults.InnerHtml = "";
            }
        }
    }
}