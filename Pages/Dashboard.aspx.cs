using System;
using System.Web.UI;

namespace All_in_one_Study_Companion.Pages
{
    public partial class Dashboard : System.Web.UI.Page
    {
        // Event handler for page load
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user is logged in, redirect to login page if not
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Pages/Account/LandIn.aspx");
            }
        }

        // Event handler for logout button click
        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            // Clear session and redirect to login page
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Pages/Account/LandIn.aspx");
        }
    }
}
