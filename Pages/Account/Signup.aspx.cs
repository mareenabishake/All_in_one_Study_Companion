using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using All_in_one_Study_Companion.Classes;
using System.Data.SqlClient;

namespace All_in_one_Study_Companion.Pages.Account
{
    public partial class Signup : System.Web.UI.Page
    {
        private DbHelper dbHelper = new DbHelper();

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        // Add this method to the class
        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        // Add this method to the class
        private void ShowMessageBox(string message)
        {
            string script = $"alert('{message.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            
            // Get user input from form fields
            string fullName = txtFullName.Text;
            string institution = txtInstitution.Text;
            string academicLevel = ddlAcademicLevel.SelectedValue;
            string username = txtUsername.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // Perform validation
            if (password != confirmPassword)
            {
                ShowMessageBox("Passwords do not match");
                return;
            }

            // Hash the password
            string hashedPassword = HashPassword(password);

            // Insert user into database
            string query = @"INSERT INTO Users (FullName, Institution, AcademicLevel, Username, Email, PasswordHash) 
                             VALUES (@FullName, @Institution, @AcademicLevel, @Username, @Email, @PasswordHash)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@FullName", fullName),
                new SqlParameter("@Institution", institution),
                new SqlParameter("@AcademicLevel", academicLevel),
                new SqlParameter("@Username", username),
                new SqlParameter("@Email", email),
                new SqlParameter("@PasswordHash", hashedPassword)
            };

            try
            {
                int rowsAffected = dbHelper.ExecuteNonQuery(query, parameters);
                if (rowsAffected > 0)
                {
                    // Registration successful
                    ShowMessageBox("Registration successful! You can now log in.");
                    Response.Redirect("~/Pages/Account/LandIn.aspx");
                }
                else
                {
                    // Registration failed
                    ShowMessageBox("Registration failed. Please try again.");
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                ShowMessageBox("An error occurred: " + ex.Message);
            }
        }

        protected void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}