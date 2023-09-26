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
    public class IntergalacticMissionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IntergalacticMissionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.IntergalacticMissions.Include(i => i.Group);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.IntergalacticMissions == null)
            {
                return NotFound();
            }

            var intergalacticMissions = await _context.IntergalacticMissions
                .Include(i => i.Group)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (intergalacticMissions == null)
            {
                return NotFound();
            }

            intergalacticMissions.PackagesDonated = _context.IntergalacticDonation.Where(m => m.MissionId == intergalacticMissions.Id).ToList();

            return View(intergalacticMissions);
        }

        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.IntergalacticGroups, "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Banner,Status,Current,Target,GroupId")] IntergalacticMissions intergalacticMissions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(intergalacticMissions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.IntergalacticGroups, "Id", "Name", intergalacticMissions.GroupId);
            return View(intergalacticMissions);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.IntergalacticMissions == null)
            {
                return NotFound();
            }

            var intergalacticMissions = await _context.IntergalacticMissions.FindAsync(id);
            if (intergalacticMissions == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.IntergalacticGroups, "Id", "Name", intergalacticMissions.GroupId);
            return View(intergalacticMissions);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Banner,Status,Current,Target,GroupId")] IntergalacticMissions intergalacticMissions)
        {
            if (id != intergalacticMissions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(intergalacticMissions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IntergalacticMissionsExists(intergalacticMissions.Id))
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
            ViewData["GroupId"] = new SelectList(_context.IntergalacticGroups, "Id", "Name", intergalacticMissions.GroupId);
            return View(intergalacticMissions);
        }

        // GET: IntergalacticMissions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.IntergalacticMissions == null)
            {
                return NotFound();
            }

            var intergalacticMissions = await _context.IntergalacticMissions
                .Include(i => i.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intergalacticMissions == null)
            {
                return NotFound();
            }

            return View(intergalacticMissions);
        }

        // POST: IntergalacticMissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IntergalacticMissions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.IntergalacticMissions'  is null.");
            }

            var intergalacticMissions = await _context.IntergalacticMissions.FindAsync(id);
            if (intergalacticMissions != null)
            {
                _context.IntergalacticMissions.Remove(intergalacticMissions);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IntergalacticMissionsExists(int id)
        {
          return (_context.IntergalacticMissions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
