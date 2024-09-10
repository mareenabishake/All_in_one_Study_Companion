using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace All_in_one_Study_Companion.Pages
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Pages/Account/LandIn.aspx");
            }
            else
            {
                // Optionally, you can display a welcome message
                string username = Session["Username"].ToString();
                // Add a Label control in your ASPX file with ID "WelcomeMessage"
                // WelcomeMessage.Text = $"Welcome, {username}!";
            }
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Pages/Account/LandIn.aspx");
        }
    }
}