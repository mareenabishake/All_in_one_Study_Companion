using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Collections.Generic; // Added for List<string> claimableBadges

namespace All_in_one_Study_Companion.Pages
{
    public partial class Badges : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Pages/Account/LandIn.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    LoadBadges();
                }
            }
        }

        protected void FetchBadgesButton_Click(object sender, EventArgs e)
        {
            CheckEligibilityForBadges();
        }

        private void CheckEligibilityForBadges()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StudyCompanionDB"].ConnectionString;
            int userId = GetUserIdFromSession();
            List<string> claimableBadges = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT BadgeName
                    FROM Badges b
                    LEFT JOIN BadgesAssigned ba ON b.BadgeID = ba.BadgeID AND ba.UserID = @UserID
                    WHERE ba.BadgeID IS NULL
                    AND EXISTS (
                        SELECT 1
                        FROM Users u
                        WHERE u.UserID = @UserID
                        AND u.QuestionsAnswered >= b.AnswersRequired
                        AND u.MinutesStudied >= b.MinutesRequired
                    )";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            claimableBadges.Add(reader["BadgeName"].ToString());
                        }
                    }
                }
            }

            if (claimableBadges.Count > 0)
            {
                ClaimBadgesButton.Enabled = true;
                string message = string.Join(", ", claimableBadges) + " badge(s) can be claimed now.";
                // Display message to the user
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                ClaimBadgesButton.Enabled = false;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No badges available to claim.');", true);
            }
        }

        protected void ClaimBadgesButton_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StudyCompanionDB"].ConnectionString;
            int userId = GetUserIdFromSession();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    INSERT INTO BadgesAssigned (BadgeID, UserID)
                    SELECT b.BadgeID, @UserID
                    FROM Badges b
                    LEFT JOIN BadgesAssigned ba ON b.BadgeID = ba.BadgeID AND ba.UserID = @UserID
                    WHERE ba.BadgeID IS NULL
                    AND EXISTS (
                        SELECT 1
                        FROM Users u
                        WHERE u.UserID = @UserID
                        AND u.QuestionsAnswered >= b.AnswersRequired
                        AND u.MinutesStudied >= b.MinutesRequired
                    )";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            // Reload badges to reflect changes
            LoadBadges();
            ClaimBadgesButton.Enabled = false;
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Badges claimed successfully.');", true);
        }

        private void LoadBadges()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StudyCompanionDB"].ConnectionString;
            int userId = GetUserIdFromSession();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT b.*, CASE WHEN ba.BadgeID IS NOT NULL THEN 1 ELSE 0 END AS IsEarned
                    FROM Badges b
                    LEFT JOIN BadgesAssigned ba ON b.BadgeID = ba.BadgeID AND ba.UserID = @UserID
                    ORDER BY b.BadgeID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    BadgesRepeater.DataSource = dt;
                    BadgesRepeater.DataBind();
                }
            }
        }


        private int GetUserIdFromSession()
        {
            if (Session["UserID"] != null)
            {
                return Convert.ToInt32(Session["UserID"]);
            }
            else
            {
                // Redirect to login page if user is not logged in
                Response.Redirect("~/Pages/Account/LandIn.aspx");
                return 0; // This line will never be reached due to the redirect
            }
        }
    }
}