using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Threading.Tasks;
using All_in_one_Study_Companion.Classes; // Make sure this namespace is correct

namespace All_in_one_Study_Companion.Pages
{
    public partial class ChatWithLLM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadChatHistory();
            }
        }


        protected async void SendButton_Click(object sender, EventArgs e)
        {
            string userMessage = MessageInput.Text.Trim();
            if (!string.IsNullOrEmpty(userMessage))
            {
                LLM.AddMessageToHistory("You", userMessage);
                AddMessageToChat("You", userMessage);

                try
                {
                    string groqResponse = await LLM.QueryGroqAsync(userMessage);
                    LLM.AddMessageToHistory("Groq AI", groqResponse);
                    AddMessageToChat("Groq AI", groqResponse);
                }
                catch (Exception ex)
                {
                    LLM.AddMessageToHistory("System", $"Error: {ex.Message}");
                    AddMessageToChat("System", $"Error: {ex.Message}");
                }

                MessageInput.Text = "";
            }
        }

        protected void ClearChatButton_Click(object sender, EventArgs e)
        {
            Session["ChatHistory"] = null;
            chatHistory.InnerHtml = "";
        }

        private void AddMessageToChat(string sender, string message)
        {
            chatHistory.InnerHtml += $"<div class='chat-message'><strong>{sender}:</strong> {message}</div>";
        }

        private void LoadChatHistory()
        {
            chatHistory.InnerHtml = "";
            var history = LLM.GetChatHistory();
            foreach (var message in history)
            {
                AddMessageToChat(message.Sender, message.Message);
            }
        }
    }
}
