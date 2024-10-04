using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

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
                string query = @"SELECT u.UserID, u.Username, u.Points, 
                                        STRING_AGG(ba.BadgeID, ',') AS BadgeIDs, 
                                        u.QuestionsAnswered, u.MinutesStudied
                                 FROM Users u
                                 LEFT JOIN BadgesAssigned ba ON u.UserID = ba.UserID
                                 LEFT JOIN Badges b ON ba.BadgeID = b.BadgeID
                                 GROUP BY u.UserID, u.Username, u.Points, u.QuestionsAnswered, u.MinutesStudied
                                 ORDER BY u.Points DESC"; // Changed to join Users, BadgesAssigned, and Badges tables and aggregate BadgeIDs

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
                                            u.Points, STRING_AGG(ba.BadgeID, ',') AS BadgeIDs, 
                                            u.QuestionsAnswered, u.MinutesStudied
                                     FROM Users u
                                     LEFT JOIN BadgesAssigned ba ON u.UserID = ba.UserID
                                     LEFT JOIN Badges b ON ba.BadgeID = b.BadgeID
                                     WHERE u.FullName LIKE @SearchTerm
                                     GROUP BY u.FullName, u.Institution, u.AcademicLevel, u.Email, 
                                              u.Points, u.QuestionsAnswered, u.MinutesStudied
                                     ORDER BY u.Points DESC"; // Changed to join Users, BadgesAssigned, and Badges tables and aggregate BadgeIDs

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
                            sb.Append("</div>");
                            sb.Append($"<p><strong>Institution:</strong> {reader["Institution"]}</p>");
                            sb.Append($"<p><strong>Academic Level:</strong> {reader["AcademicLevel"]}</p>");
                            sb.Append($"<p><strong>Email:</strong> {reader["Email"]}</p>");
                            sb.Append("<div class='stats'>");
                            sb.Append($"<div class='stat-item'><span class='stat-label'>Questions</span><span class='stat-value'>{reader["QuestionsAnswered"]}</span></div>");
                            sb.Append($"<div class='stat-item'><span class='stat-label'>Hours Studied</span><span class='stat-value'>{Convert.ToDouble(reader["MinutesStudied"]) / 60.0:F1}</span></div>");
                            sb.Append("</div>");

                            // Retrieve and display badges
                            List<string> badgeIDs = new List<string>();
                            if (!reader.IsDBNull(reader.GetOrdinal("BadgeIDs")))
                            {
                                badgeIDs.AddRange(reader["BadgeIDs"].ToString().Split(','));
                            }

                            if (badgeIDs.Count > 0)
                            {
                                sb.Append("<div class='badges'>");
                                foreach (var badgeID in badgeIDs)
                                {
                                    sb.Append($"<img src='/Content/img/{badgeID}.png' alt='Badge {badgeID}' class='badge-icon' />");
                                }
                                sb.Append("</div>");
                            }

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

        // Update the method to generate badge images
        protected string GetBadgeImages(object badgeIDs)
        {
            if (badgeIDs == null || string.IsNullOrEmpty(badgeIDs.ToString()))
            {
                return string.Empty;
            }

            var badgeIDList = badgeIDs.ToString().Split(',');
            StringBuilder sb = new StringBuilder();
            foreach (var badgeID in badgeIDList)
            {
                sb.Append($"<img src='/Content/img/{badgeID}.png' alt='Badge {badgeID}' class='badge-icon' />");
            }
            return sb.ToString();
        }

        // Add this method to convert minutes to hours
        protected string ConvertMinutesToHours(object minutes)
        {
            if (minutes == null || !double.TryParse(minutes.ToString(), out double minutesValue))
            {
                return "0.0";
            }
            return (minutesValue / 60.0).ToString("F1");
        }
    }
}