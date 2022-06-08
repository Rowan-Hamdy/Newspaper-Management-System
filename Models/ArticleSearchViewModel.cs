using Microsoft.AspNetCore.Mvc.Rendering;

namespace NewspaperCMS.Models
{
    public class ArticleSearchViewModel
    {
        public List<article> articles { get; set; }
        public SelectList  articleSelectList { get; set; }
        public string selectedArticle { get; set; }

        public ArticleSearchViewModel()
        {
            articles = new List<article>();
        }
    }
}
