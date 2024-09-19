using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Security.Cryptography;
using System.Text;

namespace All_in_one_Study_Companion.Pages
{
    public partial class Settings : System.Web.UI.Page
    {
        // Event handler for the Page_Load event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the user is logged in
                if (Session["UserID"] == null)
                {
                    // Redirect to the login page if not logged in
                    Response.Redirect("~/Pages/Account/LandIn.aspx");
                }
                else
                {
                    // Load user information
                    LoadUserInfo();
                }
            }
        }

        // Method to load user information from the database
        private void LoadUserInfo()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StudyCompanionDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT FullName, Institution, AcademicLevel, Username, Email FROM Users WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", Session["UserID"]);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    // Populate form fields with user data
                    txtFullName.Text = reader["FullName"].ToString();
                    txtInstitution.Text = reader["Institution"].ToString();
                    ddlAcademicLevel.SelectedValue = reader["AcademicLevel"].ToString();
                    txtUsername.Text = reader["Username"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                }
                reader.Close();
            }
        }

        // Event handler for the update button click
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            // Check if the passwords match
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "passwordMismatch", "alert('New password and confirm password do not match.');", true);
                return;
            }

            // Update user information in the database
            string connectionString = ConfigurationManager.ConnectionStrings["StudyCompanionDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET FullName = @FullName, Institution = @Institution, AcademicLevel = @AcademicLevel, Username = @Username, Email = @Email";
                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    query += ", PasswordHash = @PasswordHash";
                }
                query += " WHERE UserID = @UserID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FullName", txtFullName.Text);
                command.Parameters.AddWithValue("@Institution", txtInstitution.Text);
                command.Parameters.AddWithValue("@AcademicLevel", ddlAcademicLevel.SelectedValue);
                command.Parameters.AddWithValue("@Username", txtUsername.Text);
                command.Parameters.AddWithValue("@Email", txtEmail.Text);
                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    command.Parameters.AddWithValue("@PasswordHash", HashPassword(txtPassword.Text));
                }
                command.Parameters.AddWithValue("@UserID", Session["UserID"]);

                connection.Open();
                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "updateSuccess", "alert('User information updated successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "updateFailed", "alert('Failed to update user information.');", true);
                }
            }
        }

        // Method to hash the password (using the same method as in LandIn.aspx.cs)
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        // Event handler for the delete account button click
        protected void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Users WHERE UserID = @UserID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", Session["UserID"]);

                connection.Open();
                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    // Delete successful
                    Session.Clear();
                    Response.Redirect("~/Pages/Account/LandIn.aspx");
                }
                else
                {
                    // Delete failed
                    ScriptManager.RegisterStartupScript(this, GetType(), "deleteFailed", "alert('Failed to delete account.');", true);
                }
            }
        }

        // Event handler for the logout button click
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Pages/Account/LandIn.aspx");
        }
    }
}