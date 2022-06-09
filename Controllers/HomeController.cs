using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewspaperCMS.Data;
using NewspaperCMS.Models;
using System.Diagnostics;

namespace NewspaperCMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context )
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index( string selectedArticle)
        {
            var articles_list = _context.article.ToList().Where(r=>r.status==1).Select(a=>a.title).ToList();
           

            ArticleSearchViewModel model = new ArticleSearchViewModel();

            model.articleSelectList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(articles_list.Distinct().ToList());
            
            model.articles = _context.article.ToList();
            if (selectedArticle != null  )
            {
                model.articles = model.articles.Where(a => a.title == selectedArticle).ToList();
            }
            return View(model);

        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.article == null)
            {
                return NotFound();
            }

            var article = await _context.article
                .FirstOrDefaultAsync(m => m.id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}