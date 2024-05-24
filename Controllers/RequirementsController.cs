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
    public class RequirementsController : Controller
    {
        private readonly GameStoreContext _context;

        public RequirementsController(GameStoreContext context)
        {
            _context = context;
        }

        // GET: Requirements
        public async Task<IActionResult> Index()
        {
            var gameStoreContext = _context.Requirements.Include(r => r.Game);
            return View(await gameStoreContext.ToListAsync());
        }

        // GET: Requirements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requirement = await _context.Requirements
                .Include(r => r.Game)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (requirement == null)
            {
                return NotFound();
            }

            return View(requirement);
        }

        // GET: Requirements/Create
        public IActionResult Create(int? id)
        {
            ViewData["GameID"] = new SelectList(_context.Games, "ID", "ID", (int)id);
            return View();
        }

        // POST: Requirements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OS,CPU,GPU,RAM,ROM,GameID")] Requirement requirement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(requirement);
                await _context.SaveChangesAsync();
                return Redirect("~/Games/Details/" + requirement.GameID);
            }
            ViewData["GameID"] = new SelectList(_context.Games, "ID", "ID", requirement.GameID);
            return View(requirement);
        }

        // GET: Requirements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requirement = await _context.Requirements.FindAsync(id);
            if (requirement == null)
            {
                return NotFound();
            }
            ViewData["GameID"] = new SelectList(_context.Games, "ID", "ID", requirement.GameID);
            return View(requirement);
        }

        // POST: Requirements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,OS,CPU,GPU,RAM,ROM,GameID")] Requirement requirement)
        {
            if (id != requirement.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requirement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequirementExists(requirement.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("~/Games/Details/"+requirement.GameID);
            }
            ViewData["GameID"] = new SelectList(_context.Games, "ID", "ID", requirement.GameID);
            return View(requirement);
        }

        // GET: Requirements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requirement = await _context.Requirements
                .Include(r => r.Game)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (requirement == null)
            {
                return NotFound();
            }

            return View(requirement);
        }

        // POST: Requirements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var requirement = await _context.Requirements.FindAsync(id);
            _context.Requirements.Remove(requirement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequirementExists(int id)
        {
            return _context.Requirements.Any(e => e.ID == id);
        }
    }
}
