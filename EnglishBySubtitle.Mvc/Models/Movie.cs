namespace EnglishBySubtitle.Mvc.Models
{
    public class Movie
    {
        public string InputTitle { get; set; }
        public string OutputSubtitle { get; set; }
        public List<string> UniqueWords { get; set; }
    }
}