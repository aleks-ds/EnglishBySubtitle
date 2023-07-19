using EnglishBySubtitle.Mvc.Services;
using EnglishBySubtitle.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace EnglishBySubtitle.Mvc.Controllers
{
    public class DataProcessingController : Controller
    {
        public Movie model;
        public OpenSubtitlesApi authorization;
        
        public IActionResult GetSubtitle(string InputTitle)
        {
            model = new()
            {
                InputTitle = InputTitle
            };
            
            HandleData.CheckAuthorizationValidity(ref authorization);
            JArray getObjectRequest = HandleData.GetObjectRequest(model, ref authorization);
            var tupleOfListsOfMovies = new Tuple<string[,], string[,]>(HandleData.ShowListOfExactMatchTitleMovies(model, getObjectRequest), HandleData.ShowAuxiliaryListOfMovies(model, getObjectRequest));
            return View("Getsubtitle", tupleOfListsOfMovies);
        }
    }
}
