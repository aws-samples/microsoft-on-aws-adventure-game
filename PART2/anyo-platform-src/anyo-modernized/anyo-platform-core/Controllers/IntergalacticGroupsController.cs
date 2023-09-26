using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using anyo_platform.Models;
using anyo_platform_core.Data;

namespace anyo_platform_core.Controllers
{
    public class IntergalacticGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IntergalacticGroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IntergalacticGroups
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.IntergalacticGroups.Include(i => i.Leader);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: IntergalacticGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.IntergalacticGroups == null)
            {
                return NotFound();
            }

            var intergalacticGroup = await _context.IntergalacticGroups
                .Include(i => i.Leader)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intergalacticGroup == null)
            {
                return NotFound();
            }

            return View(intergalacticGroup);
        }

        // GET: IntergalacticGroups/Create
        public IActionResult Create()
        {
            ViewData["LeaderId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: IntergalacticGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,About,HomePlanet,Logo,LeaderId")] IntergalacticGroup intergalacticGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(intergalacticGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LeaderId"] = new SelectList(_context.Users, "Id", "UserName", intergalacticGroup.LeaderId);
            return View(intergalacticGroup);
        }

        // GET: IntergalacticGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.IntergalacticGroups == null)
            {
                return NotFound();
            }

            var intergalacticGroup = await _context.IntergalacticGroups.FindAsync(id);
            if (intergalacticGroup == null)
            {
                return NotFound();
            }
            ViewData["LeaderId"] = new SelectList(_context.Users, "Id", "UserName", intergalacticGroup.LeaderId);
            return View(intergalacticGroup);
        }

        // POST: IntergalacticGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,About,HomePlanet,Logo,LeaderId")] IntergalacticGroup intergalacticGroup)
        {
            if (id != intergalacticGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(intergalacticGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IntergalacticGroupExists(intergalacticGroup.Id))
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
            ViewData["LeaderId"] = new SelectList(_context.Users, "Id", "UserName", intergalacticGroup.LeaderId);
            return View(intergalacticGroup);
        }

        // GET: IntergalacticGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.IntergalacticGroups == null)
            {
                return NotFound();
            }

            var intergalacticGroup = await _context.IntergalacticGroups
                .Include(i => i.Leader)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intergalacticGroup == null)
            {
                return NotFound();
            }

            return View(intergalacticGroup);
        }

        // POST: IntergalacticGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IntergalacticGroups == null)
            {
                return Problem("Entity set 'ApplicationDbContext.IntergalacticGroup'  is null.");
            }

            var intergalacticGroup = await _context.IntergalacticGroups.FindAsync(id);
            if (intergalacticGroup != null)
            {
                _context.IntergalacticGroups.Remove(intergalacticGroup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IntergalacticGroupExists(int id)
        {
            return (_context.IntergalacticGroups?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
