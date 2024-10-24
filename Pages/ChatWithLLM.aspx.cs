using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Threading.Tasks;
using All_in_one_Study_Companion.Classes; // Make sure this namespace is correct
using System.Text.RegularExpressions;

namespace All_in_one_Study_Companion.Pages
{
    public partial class ChatWithLLM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load chat history on initial page load
                LoadChatHistory();
            }
        }

        // Event handler for send button click
        protected async void SendButton_Click(object sender, EventArgs e)
        {
            string userMessage = MessageInput.Text.Trim();
            if (!string.IsNullOrEmpty(userMessage))
            {
                // Add user message to chat history
                LLM.AddMessageToHistory("You", userMessage);
                AddMessageToChat("You", userMessage);

                try
                {
                    // Query Groq AI and add response to chat history
                    string groqResponse = await LLM.QueryGroqAsync(userMessage);
                    LLM.AddMessageToHistory("Llama 3.1", groqResponse);
                    AddMessageToChat("Llama 3.1", groqResponse);
                }
                catch (Exception ex)
                {
                    // Handle and display any errors
                    LLM.AddMessageToHistory("System", $"Error: {ex.Message}");
                    AddMessageToChat("System", $"Error: {ex.Message}");
                }

                // Clear input field after sending message
                MessageInput.Text = "";
            }
        }

        // Event handler for clear chat button click
        protected void ClearChatButton_Click(object sender, EventArgs e)
        {
            // Clear chat history from session and UI
            Session["ChatHistory"] = null;
            chatHistory.InnerHtml = "";
        }

        // Method to add a message to the chat UI
        private void AddMessageToChat(string sender, string message)
        {
            string formattedMessage = FormatMessage(message);
            chatHistory.InnerHtml += $"<div class='chat-message'><strong>{sender}:</strong> {formattedMessage}</div>";
        }

        // Method to load chat history from session
        private void LoadChatHistory()
        {
            chatHistory.InnerHtml = "";
            var history = LLM.GetChatHistory();
            foreach (var message in history)
            {
                AddMessageToChat(message.Sender, message.Message);
            }
        }

        private string FormatMessage(string message)
        {
            // Replace "\n\n" with double line breaks
            message = message.Replace("\\n\\n", "<br><br>");
            
            // Replace remaining "\n" with single line breaks
            message = message.Replace("\\n", "<br>");

            // Bold text between star signs
            message = Regex.Replace(message, @"\*(.*?)\*", "<strong>$1</strong>");

            // Format numbered lists
            message = Regex.Replace(message, @"(\d+\.\s)", "<br><strong>$1</strong>");

            // Add extra line break before "Llama 3.1:" if present
            message = message.Replace("Llama 3.1:", "<br><strong>Llama 3.1:</strong>");

            return message;
        }
    }
}
