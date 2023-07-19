using Newtonsoft.Json.Linq;
using RestSharp;

namespace EnglishBySubtitle.Mvc.Services
{
    public class OpenSubtitlesApi
    {
        public readonly string login = "infovirus";
        public readonly string password = "A6d1AlLh&g0W";
        public readonly string apiKey = "7MqL9KE0eWiN8SWr3MXdWreqDPSwDoep";
        public readonly string url = "https://api.opensubtitles.com/api/v1";
        public readonly RestClient restClient;
        public string Token { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public OpenSubtitlesApi()
        {
            restClient = new RestClient(url);
            var tokenRequest = new RestRequest("login", Method.Post);
            tokenRequest.AddHeader("Content-Type", "application/json");
            tokenRequest.AddHeader("Api-Key", apiKey);
            tokenRequest.AddJsonBody(new { username = login, password });
            var tokenResponse = restClient.Execute(tokenRequest, Method.Post);
            CreatedAt = DateTime.Now;
            Token = JObject.Parse(tokenResponse.Content)["token"].ToString();
        }
    }
}
