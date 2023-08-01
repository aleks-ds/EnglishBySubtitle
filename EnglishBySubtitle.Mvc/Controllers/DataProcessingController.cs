using EnglishBySubtitle.Mvc.Services;
using EnglishBySubtitle.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace EnglishBySubtitle.Mvc.Controllers
{
    public class DataProcessingController : Controller
    {
        public Movie model = new ();
        public OpenSubtitlesApi authorization = new ();
        
        public IActionResult GetSubtitle(string InputTitle)
        {
            model.InputTitle = InputTitle;
            HandleData.CheckAuthorizationValidity(ref authorization);
            JArray getObjectRequest;
            try 
            {
                getObjectRequest = HandleData.GetObjectRequest(model, ref authorization);
            }
            catch (Exception ex) {
                // Saving the URL of the previous page in TempData.
                TempData["PreviousUrl"] = Request.Headers["Referer"].ToString();

                ErrorViewModel errorViewModel = new ErrorViewModel
                {
                    ErrorMessage = ex.Message,
                    RequestId = HttpContext?.Items["RequestId"]?.ToString()
                };
                return View("Error", errorViewModel);
            }           

            var tupleOfListsOfMovies = new Tuple<string[,], string[,]>(
                HandleData.ShowListOfExactMatchTitleMovies(model, getObjectRequest), 
                HandleData.ShowAuxiliaryListOfMovies(model, getObjectRequest));
            return View("Getsubtitle", tupleOfListsOfMovies);
        }

        public IActionResult ReadSubtitle(string idSubtitle, string titleOfMovie)
         {
            model.InputTitle = titleOfMovie;
            JObject objectRequest = HandleData.PostObjectRequest(ref authorization, idSubtitle); 
            model.OutputSubtitle = HandleData.GetTextFromSubtitle(objectRequest).Result;
            model.UniqueWords = HandleData.ShowListOfUniqueWordsOfMovie(model.OutputSubtitle);

            return View("~/Views/DataProcessing/Subtitle.cshtml", model);
        }       
    }
}
