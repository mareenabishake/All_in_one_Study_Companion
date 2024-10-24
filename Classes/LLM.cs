using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Diagnostics;

namespace All_in_one_Study_Companion.Classes
{
    // Static class for handling LLM (Language Model) operations
    public static class LLM
    {
        // HttpClient instance for making API requests
        private static readonly HttpClient client = new HttpClient();
        // API endpoint for Groq's chat completions
        private const string GroqApiUrl = "https://api.groq.com/openai/v1/chat/completions";

        // Static constructor to set up the HttpClient with API key
        static LLM()
        {
            // Retrieve API key from configuration
            string apiKey = ConfigurationManager.AppSettings["GROQ_API_KEY"];
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ConfigurationErrorsException("GROQ_API_KEY is not set in the configuration.");
            }
            // Add API key to request headers
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        }

        // Method to send a query to the Groq API
        public static async Task<string> QueryGroqAsync(string userMessage)
        {
            // Get chat history
            var history = GetChatHistory();
            var messages = new List<object>();

            // Convert chat history to API-compatible format
            foreach (var msg in history)
            {
                messages.Add(new { role = msg.Sender == "You" ? "user" : "assistant", content = msg.Message });
            }
            // Add current user message
            messages.Add(new { role = "user", content = userMessage });

            // Prepare request body
            var requestBody = new
            {
                model = "llama-3.1-70b-versatile",
                messages = messages
            };

            // Log the prompt being sent to the LLM
            System.Diagnostics.Debug.WriteLine("Sending prompt to LLM:");
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(requestBody, Formatting.Indented));

            // Serialize and send request
            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            try
            {
                // Send POST request to API
                var response = await client.PostAsync(GroqApiUrl, content);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();

                // Log the response from the LLM
                System.Diagnostics.Debug.WriteLine("Received response from LLM:");
                System.Diagnostics.Debug.WriteLine(responseContent);

                // Deserialize and extract response
                var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);
                string formattedResponse = responseObject.choices[0].message.content.ToString();
                return formattedResponse.Replace("\n", "\\n"); // Preserve newline characters
            }
            catch (HttpRequestException e)
            {
                throw new Exception($"API request failed: {e.Message}");
            }
        }

        // Method to retrieve chat history from session
        public static List<ChatMessage> GetChatHistory()
        {
            if (HttpContext.Current.Session["ChatHistory"] == null)
            {
                HttpContext.Current.Session["ChatHistory"] = new List<ChatMessage>();
            }
            return (List<ChatMessage>)HttpContext.Current.Session["ChatHistory"];
        }

        // Method to add a new message to chat history
        public static void AddMessageToHistory(string sender, string message)
        {
            var history = GetChatHistory();
            history.Add(new ChatMessage { Sender = sender, Message = message });
            HttpContext.Current.Session["ChatHistory"] = history;
        }
    }

    // Class to represent a chat message
    public class ChatMessage
    {
        public string Sender { get; set; }
        public string Message { get; set; }
    }
}
