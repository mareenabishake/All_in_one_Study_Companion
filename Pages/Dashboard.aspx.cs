using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Threading.Tasks;
using All_in_one_Study_Companion.Classes; // Make sure this namespace is correct

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
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Pages/Account/LandIn.aspx");
        }

        protected async void SendButton_Click(object sender, EventArgs e)
        {
            string userMessage = MessageInput.Text.Trim();
            if (!string.IsNullOrEmpty(userMessage))
            {
                AddMessageToChat("You", userMessage);

                try
                {
                    string groqResponse = await LLM.QueryGroqAsync(userMessage);
                    AddMessageToChat("Groq AI", groqResponse);
                }
                catch (Exception ex)
                {
                    AddMessageToChat("System", $"Error: {ex.Message}");
                }

                MessageInput.Text = "";
            }
        }

        private void AddMessageToChat(string sender, string message)
        {
            HtmlGenericControl messageDiv = new HtmlGenericControl("div");
            messageDiv.Attributes["class"] = "chat-message";
            messageDiv.InnerHtml = $"<strong>{sender}:</strong> {message}";
            chatHistory.Controls.Add(messageDiv);
        }
    }
}
