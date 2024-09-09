using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;
using All_in_one_Study_Companion.Helpers;
using System.Security.Cryptography;
using System.Text;

namespace All_in_one_Study_Companion.Pages.Account
{
    public partial class LandIn : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Your existing code here
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = Request.Form["Username"];
            string password = Request.Form["Password"];

            if (ValidateUser(username, password))
            {
                // Successful login
                Session["Username"] = username;
                Response.Redirect("~/Pages/Dashboard.aspx");
            }
            else
            {
                // Failed login
                ShowMessageBox("Invalid username or password.");
            }
        }

        private bool ValidateUser(string username, string password)
        {
            string hashedPassword = HashPassword(password);
            DbHelper dbHelper = new DbHelper();
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND PasswordHash = @PasswordHash";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", username),
                new SqlParameter("@PasswordHash", hashedPassword)
            };

            try
            {
                DataTable result = dbHelper.ExecuteQuery(query, parameters);
                int count = Convert.ToInt32(result.Rows[0][0]);
                return count > 0;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error validating user: {ex.Message}");
                return false;
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private void ShowMessageBox(string message)
        {
            string script = $"alert('{message.Replace("'", "\\'")}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
        }
    }
}
