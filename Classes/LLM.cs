using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Configuration;
using System.Collections.Generic;
using System.Web;

namespace All_in_one_Study_Companion.Classes
{
    public static class LLM
    {
        private static readonly HttpClient client = new HttpClient();
        private const string GroqApiUrl = "https://api.groq.com/openai/v1/chat/completions";

        static LLM()
        {
            string apiKey = ConfigurationManager.AppSettings["GROQ_API_KEY"];
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ConfigurationErrorsException("GROQ_API_KEY is not set in the configuration.");
            }
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        }

        public static async Task<string> QueryGroqAsync(string userMessage)
        {
            var history = GetChatHistory();
            var messages = new List<object>();

            foreach (var msg in history)
            {
                messages.Add(new { role = msg.Sender == "You" ? "user" : "assistant", content = msg.Message });
            }
            messages.Add(new { role = "user", content = userMessage });

            var requestBody = new
            {
                model = "mixtral-8x7b-32768",
                messages = messages
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(GroqApiUrl, content);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);
                return responseObject.choices[0].message.content.ToString();
            }
            catch (HttpRequestException e)
            {
                throw new Exception($"API request failed: {e.Message}");
            }
        }

        public static List<ChatMessage> GetChatHistory()
        {
            if (HttpContext.Current.Session["ChatHistory"] == null)
            {
                HttpContext.Current.Session["ChatHistory"] = new List<ChatMessage>();
            }
            return (List<ChatMessage>)HttpContext.Current.Session["ChatHistory"];
        }

        public static void AddMessageToHistory(string sender, string message)
        {
            var history = GetChatHistory();
            history.Add(new ChatMessage { Sender = sender, Message = message });
            HttpContext.Current.Session["ChatHistory"] = history;
        }
    }

    public class ChatMessage
    {
        public string Sender { get; set; }
        public string Message { get; set; }
    }
}
