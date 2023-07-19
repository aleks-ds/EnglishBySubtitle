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
            JObject subtitlesJson = JObject.Parse(subtitlesResponse.Content);
            //Console.WriteLine(subtitlesJson);
            return (JArray)subtitlesJson["data"];
        }
        public static string[,] ShowListOfExactMatchTitleMovies(Movie model, JArray subtitleData)
        {
            int amountMovies = 0;
            string title, language;

            foreach (JObject subtitle in subtitleData.Cast<JObject>())
            {
                title = (string)subtitle["attributes"]["feature_details"]["title"];
                language = (string)subtitle["attributes"]["language"];
                if (title.IndexOf(model.InputTitle) != -1 && language == "en" && title.Length == model.InputTitle.Length)
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

                if (title.IndexOf(model.InputTitle) != -1 && language == "en" && title.Length == model.InputTitle.Length)
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

            // put the object movie to two-dimensional array
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
        public static string GetTextFromSubtitle(string contentFromSubtitle)
        {
            string[] subtitleLines = contentFromSubtitle.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            string patternNum = @"^\D.*\D$";
            Regex regexNum = new(patternNum);
            string patternTeg = @"^(?:(?!<.*>).)*$";
            Regex regexTeg = new(patternTeg);
            string text = "";

            List<string> listLine = new();
            foreach (string lineWithoutNumber in subtitleLines)
            {
                if (regexNum.IsMatch(lineWithoutNumber) && regexTeg.IsMatch(lineWithoutNumber))
                {
                    listLine.Add(lineWithoutNumber);
                    text += lineWithoutNumber;
                }
            }
            return text;
        }
        public static List<string> ShowListOfUniqueWordsOfMovie(string text)
        {
            string[] words = text.Split(new[] { ' ', '.', ',', '!', '?', '-', ':', '"', '(', ')', '<', '>', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].Trim('\'').ToLower();
            }

            // Создание списка слов, которые встречаются только один раз
            List<string> uniqueWords = words
                .GroupBy(w => w)                     // Группировка слов по их значению
                .Where(g => g.Count() == 1)          // Отбор групп, содержащих только одно слово
                .Select(g => g.Key)                  // Выбор уникальных слов
                .OrderBy(w => w)                     // сортировка слов по алфавиту
                .ToList();

            /*  Console.WriteLine(uniqueWords.Count);

              foreach (string word in uniqueWords)
              {
                  Console.WriteLine(word);
              }*/
            return uniqueWords;
        }
    }
}
