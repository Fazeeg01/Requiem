using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameStore.Data;
using GameStore.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace GameStore.Controllers
{
    public class ScreenshotsController : Controller
    {
        private readonly GameStoreContext _context;
        IHostingEnvironment Environment;

        public ScreenshotsController(GameStoreContext context, IHostingEnvironment _environment)
        {
            _context = context;
            Environment = _environment; 
        }

        // GET: Screenshots
        public async Task<IActionResult> Index()
        {
            var gameStoreContext = _context.Screenshots.Include(s => s.Game);
            return View(await gameStoreContext.ToListAsync());
        }

        // GET: Screenshots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var screenshot = await _context.Screenshots
                .Include(s => s.Game)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (screenshot == null)
            {
                return NotFound();
            }

            return View(screenshot);
        }

        // GET: Screenshots/Create
        public IActionResult Create()
        {
            ViewData["GameID"] = new SelectList(_context.Games, "ID", "ID");
            return View();
        }


        // POST: Screenshots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int GameID, IFormFile file)
        {
            Screenshot screenshot = new Screenshot { GameID = GameID };
            
            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;


            string path = Path.Combine(this.Environment.WebRootPath, "Images/Games/" + GameID + "/Screenshots");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = Path.GetFileName(file.FileName);
            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }

            screenshot.Image = "/Images/Games/" + GameID+ "/Screenshots/" + fileName;

            _context.Add(screenshot);
            _context.SaveChanges();
            return Redirect("~/Games/Details/"+GameID);
        }


        // POST: Screenshots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,GameID,Image")] Screenshot screenshot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(screenshot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameID"] = new SelectList(_context.Games, "ID", "ID", screenshot.GameID);
            return View(screenshot);
        }

        // GET: Screenshots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var screenshot = await _context.Screenshots.FindAsync(id);
            if (screenshot == null)
            {
                return NotFound();
            }
            ViewData["GameID"] = new SelectList(_context.Games, "ID", "ID", screenshot.GameID);
            return View(screenshot);
        }

        // POST: Screenshots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,GameID,Image")] Screenshot screenshot)
        {
            if (id != screenshot.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(screenshot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScreenshotExists(screenshot.ID))
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
            ViewData["GameID"] = new SelectList(_context.Games, "ID", "ID", screenshot.GameID);
            return View(screenshot);
        }

        // GET: Screenshots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var screenshot = await _context.Screenshots
                .Include(s => s.Game)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (screenshot == null)
            {
                return NotFound();
            }

            return View(screenshot);
        }

        // POST: Screenshots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var screenshot = await _context.Screenshots.FindAsync(id);
            _context.Screenshots.Remove(screenshot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScreenshotExists(int id)
        {
            return _context.Screenshots.Any(e => e.ID == id);
        }
    }
}
