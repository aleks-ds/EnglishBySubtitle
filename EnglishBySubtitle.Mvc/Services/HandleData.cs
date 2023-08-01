using EnglishBySubtitle.Mvc.Models;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Text.RegularExpressions;

namespace EnglishBySubtitle.Mvc.Services
{
    public static class HandleData
    {
        public static void CheckAuthorizationValidity(ref OpenSubtitlesApi authorization)
        {
            // Checking for null and whether 24 hours have passed since the authorization and token acquisition.
            if (authorization == null || DateTime.Now.Subtract(authorization.CreatedAt).TotalSeconds >= 86400)
            {
                // Creating an authorization object.
                authorization = new OpenSubtitlesApi();
            }
        }
        public static JArray GetObjectRequest(Movie model, ref OpenSubtitlesApi authorization)
        {
            var subtitlesRequest = new RestRequest("subtitles", Method.Get);
            subtitlesRequest.AddHeader("Authorization", $"Bearer {authorization.Token}");
            subtitlesRequest.AddHeader("Api-Key", authorization.apiKey);
            subtitlesRequest.AddParameter("query", model.InputTitle);
            subtitlesRequest.AddParameter("language", "en");
            var subtitlesResponse = authorization.restClient.Execute(subtitlesRequest, Method.Get);
            // Check on null
            if (subtitlesResponse?.Content != null)
            {
                JObject subtitlesJson = JObject.Parse(subtitlesResponse.Content);
                return (JArray)(subtitlesJson["data"] ?? new JArray());
            }
            throw new Exception();
        }
        public static JObject PostObjectRequest(ref OpenSubtitlesApi authorization, string idSubtitle)
        {
            var request = new RestRequest("download", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Api-Key", authorization.apiKey);
            request.AddParameter("application/json", $"{{\n  \"file_id\": {idSubtitle}\n}}", ParameterType.RequestBody);
            RestResponse response = authorization.restClient.Execute(request);
            if(response?.Content != null)
            {
                JObject objectRequest = JObject.Parse(response.Content);
                return (JObject)(objectRequest ?? new JObject());
            }
            else
            {
                throw new Exception();
            }
        }
        public static string[,] ShowListOfExactMatchTitleMovies(Movie model, JArray subtitleData)
        {
            int amountMovies = 0;
            string title, language;
            if (subtitleData != null && subtitleData.Count > 0)
            {
                foreach (JObject subtitle in subtitleData.Cast<JObject>())
                {
                    title = (string)subtitle["attributes"]["feature_details"]["title"];
                    language = (string)subtitle["attributes"]["language"];
                    if (title.ToLower().IndexOf(model.InputTitle.ToLower()) != -1 && language == "en" && title.Length == model.InputTitle.Length)
                        amountMovies++;
                }

                string[,] exactMatchTitle = new string[amountMovies, 4];
                string movieName, url, idMovie;
                int counter = 0;

                foreach (JObject subtitle in subtitleData.Cast<JObject>())
                {
                    title = (string)subtitle["attributes"]["feature_details"]["title"];
                    movieName = (string)subtitle["attributes"]["feature_details"]["movie_name"];
                    language = (string)subtitle["attributes"]["language"];
                    url = (string)subtitle["attributes"]["url"];
                    idMovie = (string)subtitle["attributes"]["files"][0]["file_id"];

                    if (title.ToLower().IndexOf(model.InputTitle.ToLower()) != -1 && language == "en" && title.Length == model.InputTitle.Length)
                    {
                        if (counter != amountMovies)
                        {
                            exactMatchTitle[counter, 0] = title;
                            exactMatchTitle[counter, 1] = movieName;
                            exactMatchTitle[counter, 2] = url;
                            exactMatchTitle[counter, 3] = idMovie;
                            counter++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                return exactMatchTitle;
            }
            else
            {
                return new string[,] { };
            }           
        }
        public static string[,] ShowAuxiliaryListOfMovies(Movie model, JArray subtitleData)
        {
            int amountMovies = 0;
            string title, language;

            // give me amount of movies at list.
            foreach (JObject subtitle in subtitleData.Cast<JObject>())
            {
                title = (string)subtitle["attributes"]["feature_details"]["title"];
                language = (string)subtitle["attributes"]["language"];

                if (title.ToLower().IndexOf(model.InputTitle.ToLower()) != -1 && language == "en" && title.Length != model.InputTitle.Length)
                    amountMovies++;
            }

            string movieName, url, idMovie;
            string[,] exactMatchTitle = new string[amountMovies, 4];
            int counter = 0;

            // Put the object movie to two-dimensional array
            foreach (JObject subtitle in subtitleData.Cast<JObject>())
            {
                title = (string)subtitle["attributes"]["feature_details"]["title"];
                movieName = (string)subtitle["attributes"]["feature_details"]["movie_name"];
                language = (string)subtitle["attributes"]["language"];
                url = (string)subtitle["attributes"]["url"];
                idMovie = (string)subtitle["attributes"]["files"][0]["file_id"];

                if (title.ToLower().IndexOf(model.InputTitle.ToLower()) != -1 && language == "en" && title.Length != model.InputTitle.Length)
                {
                    if (counter != amountMovies)
                    {
                        exactMatchTitle[counter, 0] = title;
                        exactMatchTitle[counter, 1] = movieName;
                        exactMatchTitle[counter, 2] = url;
                        exactMatchTitle[counter, 3] = idMovie;
                        counter++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return exactMatchTitle;
        }
        public static async Task<string> GetTextFromSubtitle(JObject objectRequest)
        {
            string content;
            using (HttpClient client = new())
            {
                content = await client.GetStringAsync((string)objectRequest["link"]);
            }
            string[] subtitleLines = content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            string patternNum = @"^\D.*\D$";
            Regex regexNum = new(patternNum);
            string patternTeg = @"^(?:(?!<.*>).)*$";
            Regex regexTeg = new(patternTeg);
            string text = "";

            List<string> listLine = new();
            int indexStartSentence, indexEndSentence;
            foreach (string lineWithoutNumber in subtitleLines)
            {
                if (regexNum.IsMatch(lineWithoutNumber) && regexTeg.IsMatch(lineWithoutNumber))
                {                    
                    indexStartSentence = 0;
                    indexEndSentence = 1;
                    while (indexStartSentence < lineWithoutNumber.Length && !char.IsLetter(lineWithoutNumber[indexStartSentence]))
                    {
                        indexStartSentence++;
                    }
                    while (indexEndSentence < lineWithoutNumber.Length - 1 && !char.IsLetter(lineWithoutNumber[^indexEndSentence]))
                    {
                        indexEndSentence++;
                    }
                    if (indexEndSentence > 1)
                        indexEndSentence--;
                    int lastIndex = (lineWithoutNumber.Length - indexEndSentence + 1);


                    string clearLine;
                    if (text.Length != 0 && text[^2] == '.')
                        clearLine = char.ToUpper(lineWithoutNumber[indexStartSentence..lastIndex][0]) + lineWithoutNumber[indexStartSentence..lastIndex][1..];
                    else
                        clearLine = lineWithoutNumber[indexStartSentence..lastIndex];

                    Console.WriteLine("Оригинал: " + lineWithoutNumber);
                    Console.WriteLine("Финальная: " + clearLine);
                    Console.WriteLine();
                    
                    listLine.Add(clearLine);
                    text += clearLine + " ";
                }
            }
            return text;
        }
        public static List<string> ShowListOfUniqueWordsOfMovie(string textFromSubtitle)
        {
            string[] uncleanedWords = textFromSubtitle.Split(' ', StringSplitOptions.RemoveEmptyEntries);            
            for (int i = 0; i < uncleanedWords.Length; i++)
            {
                uncleanedWords[i] = uncleanedWords[i].Trim(' ', '.', ',', '!', '?', '-', ':', ';', '"',
                    '\'', '(', ')', '<', '>', '[', ']').ToLower();
            }
            string[] words = uncleanedWords.Distinct().ToArray();
            Array.Sort(words);
            List<string> uniqueWords = new();
            List<string> uniqueNumbers = new();
            foreach (string word in words) 
            {
                if(word.Length != 1 && char.IsLetter(word[0]))
                    uniqueWords.Add(word);
                else
                    uniqueNumbers.Add(word);
            }
            return uniqueWords;
        }
    }
}
