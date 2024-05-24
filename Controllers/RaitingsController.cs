using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameStore.Data;
using GameStore.Models;

namespace GameStore.Controllers
{
    public class RaitingsController : Controller
    {
        private readonly GameStoreContext _context;

        public RaitingsController(GameStoreContext context)
        {
            _context = context;
        }

        // GET: Raitings
        public async Task<IActionResult> Index()
        {
            var gameStoreContext = _context.Raitings.Include(r => r.Game).Include(r => r.User);
            return View(await gameStoreContext.ToListAsync());
        }

        // GET: Raitings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raiting = await _context.Raitings
                .Include(r => r.Game)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (raiting == null)
            {
                return NotFound();
            }

            return View(raiting);
        }

        // GET: Raitings/Create
        public IActionResult Create()
        {
            ViewData["GameID"] = new SelectList(_context.Games, "ID", "ID");
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID");
            return View();
        }

        // GET: Raitings/Rate
        public IActionResult Rate(int? GameID, int? Value)
        {
            var _currentUser = _context.Users.First(x => x.Login.Equals(HttpContext.User.Identity.Name));
            Raiting raiting = new Raiting { GameID = (int)GameID, UserID = _currentUser.ID, Value = (int)Value };
            _context.Add(raiting);
            _context.SaveChanges();

            Instrument game = _context.Games.First(x => x.ID == raiting.GameID);
            game.Raiting = (double)(_context.Raitings.Where(x => x.GameID == raiting.GameID).Sum(x => x.Value) / _context.Raitings.Where(x => x.GameID == raiting.GameID).Count());

            _context.Update(game);
            _context.SaveChanges();

            return Redirect("~/Games/Details/"+raiting.GameID);
        }

        // POST: Raitings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,GameID,UserID,Value")] Raiting raiting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(raiting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameID"] = new SelectList(_context.Games, "ID", "ID", raiting.GameID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", raiting.UserID);
            return View(raiting);
        }

        // GET: Raitings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raiting = await _context.Raitings.FindAsync(id);
            if (raiting == null)
            {
                return NotFound();
            }
            ViewData["GameID"] = new SelectList(_context.Games, "ID", "ID", raiting.GameID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", raiting.UserID);
            return View(raiting);
        }

        // POST: Raitings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,GameID,UserID,Value")] Raiting raiting)
        {
            if (id != raiting.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(raiting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RaitingExists(raiting.ID))
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
            ViewData["GameID"] = new SelectList(_context.Games, "ID", "ID", raiting.GameID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", raiting.UserID);
            return View(raiting);
        }

        // GET: Raitings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raiting = await _context.Raitings
                .Include(r => r.Game)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (raiting == null)
            {
                return NotFound();
            }

            return View(raiting);
        }

        // POST: Raitings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var raiting = await _context.Raitings.FindAsync(id);
            _context.Raitings.Remove(raiting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RaitingExists(int id)
        {
            return _context.Raitings.Any(e => e.ID == id);
        }
    }
}
