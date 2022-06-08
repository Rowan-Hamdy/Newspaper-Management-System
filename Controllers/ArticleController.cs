using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewspaperCMS.Data;
using NewspaperCMS.Models;

namespace NewspaperCMS.Controllers
{
   [Authorize(Roles = "Writer,Admin")]
    public class ArticleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArticleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Article
        public async Task<IActionResult> Index()
        {
            var userId = _context.Users.Where(user => user.UserName == User.Identity.Name).FirstOrDefault();
            var role = _context.UserRoles.Where(role => role.UserId == userId.Id).FirstOrDefault();
            
            if (role.RoleId == "1")
            {

                return _context.article != null ?
              View(await _context.article.ToListAsync()) :
              Problem("Entity set 'ApplicationDbContext.category'  is null.");
            }
            

            var articles = _context.article.Where(a=>a.writer_id == userId.Id).ToListAsync();
            return _context.article != null ?
                          //View(await _context.article.ToListAsync()) :
                          View(await articles) :
                          Problem("Entity set 'ApplicationDbContext.article'  is null.");

        }

        // GET: Article/Details/5
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

        // GET: Article/Create
        public IActionResult Create()
        {
            var userId = _context.Users.Where(user => user.UserName == User.Identity.Name).FirstOrDefault();
            var role = _context.UserRoles.Where(role => role.UserId == userId.Id).FirstOrDefault();
            ViewBag.Message = role.RoleId;

            return View();
        }

        // POST: Article/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,status,article_date,title,content,writer_id")] article article)
        {
            var userId = _context.Users.Where(user => user.UserName == User.Identity.Name).FirstOrDefault();
            ViewBag.UserId = userId.Id;


            if (ModelState.IsValid)
            {
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

          
           
            return View(article);
        }

        // GET: Article/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.article == null)
            {
                return NotFound();
            }

            var article = await _context.article.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        // POST: Article/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,status,article_date,title,content,writer_id")] article article)
        {
            if (id != article.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!articleExists(article.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        // GET: Article/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Article/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.article == null)
            {
                return Problem("Entity set 'ApplicationDbContext.article'  is null.");
            }
            var article = await _context.article.FindAsync(id);
            if (article != null)
            {
                _context.article.Remove(article);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool articleExists(int id)
        {
          return (_context.article?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
