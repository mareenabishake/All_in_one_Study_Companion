using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace All_in_one_Study_Companion.Pages
{
    public partial class Settings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("~/Pages/Account/LandIn.aspx");
                }
                else
                {
                    LoadUserInfo();
                }
            }
        }

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
                    txtFullName.Text = reader["FullName"].ToString();
                    txtInstitution.Text = reader["Institution"].ToString();
                    ddlAcademicLevel.SelectedValue = reader["AcademicLevel"].ToString();
                    txtUsername.Text = reader["Username"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                }
                reader.Close();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "passwordMismatch", "alert('New password and confirm password do not match.');", true);
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["StudyCompanionDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET FullName = @FullName, Institution = @Institution, AcademicLevel = @AcademicLevel, Username = @Username, Email = @Email";
                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    query += ", Password = @Password";
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
                    command.Parameters.AddWithValue("@Password", HashPassword(txtPassword.Text)); // Implement a proper password hashing method
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

        // Implement a proper password hashing method
        private string HashPassword(string password)
        {
            // Use a secure hashing algorithm like bcrypt or Argon2
            // This is just a placeholder
            return password;
        }

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

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Pages/Account/LandIn.aspx");
        }
    }
}