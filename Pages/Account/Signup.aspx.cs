using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using All_in_one_Study_Companion.Helpers;
using System.Data.SqlClient;

namespace All_in_one_Study_Companion.Pages.Account
{
    public partial class Signup : System.Web.UI.Page
    {
        private DbHelper dbHelper = new DbHelper();

        protected void Page_Load(object sender, EventArgs e)
        {

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
                // Display error message: Passwords do not match
                return;
            }

            // Insert user into database
            string query = @"INSERT INTO Users (FullName, Institution, AcademicLevel, Username, Email, Password) 
                             VALUES (@FullName, @Institution, @AcademicLevel, @Username, @Email, @Password)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@FullName", fullName),
                new SqlParameter("@Institution", institution),
                new SqlParameter("@AcademicLevel", academicLevel),
                new SqlParameter("@Username", username),
                new SqlParameter("@Email", email),
                new SqlParameter("@Password", password) // Consider hashing the password before storing
            };

            try
            {
                int rowsAffected = dbHelper.ExecuteNonQuery(query, parameters);
                if (rowsAffected > 0)
                {
                    // Registration successful
                    Response.Redirect("~/Pages/Account/LandIn.aspx");
                }
                else
                {
                    // Registration failed
                    // Display error message to user
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                // Display error message to user
            }
        }
    }
}