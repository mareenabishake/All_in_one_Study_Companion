using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Configuration;

namespace All_in_one_Study_Companion.Classes
{
    public static class LLM
    {
        private static readonly HttpClient client = new HttpClient();
        private const string GroqApiUrl = "https://api.groq.com/openai/v1/chat/completions";

        static LLM()
        {
            string apiKey = ConfigurationManager.AppSettings["GROQ_API_KEY"];
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        }

        public static async Task<string> QueryGroqAsync(string userMessage)
        {
            var requestBody = new
            {
                model = "llama-3.1-70b-versatile",
                messages = new[]
                {
                    new { role = "user", content = userMessage }
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(GroqApiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);
                return responseObject.choices[0].message.content.ToString();
            }
            else
            {
                throw new Exception($"API request failed: {response.StatusCode}");
            }
        }
    }
}
