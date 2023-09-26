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
    public class IntergalacticPackagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IntergalacticPackagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return _context.IntergalacticPackages != null ? 
                          View(await _context.IntergalacticPackages.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.IntergalacticPackages'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.IntergalacticPackages == null)
            {
                return NotFound();
            }

            var intergalacticPackages = await _context.IntergalacticPackages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intergalacticPackages == null)
            {
                return NotFound();
            }

            return View(intergalacticPackages);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PackageContents,PackageArt,PackageType")] IntergalacticPackages intergalacticPackages)
        {
            if (ModelState.IsValid)
            {
                _context.Add(intergalacticPackages);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(intergalacticPackages);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.IntergalacticPackages == null)
            {
                return NotFound();
            }

            var intergalacticPackages = await _context.IntergalacticPackages.FindAsync(id);
            if (intergalacticPackages == null)
            {
                return NotFound();
            }
            return View(intergalacticPackages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PackageContents,PackageArt,PackageType")] IntergalacticPackages intergalacticPackages)
        {
            if (id != intergalacticPackages.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(intergalacticPackages);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IntergalacticPackagesExists(intergalacticPackages.Id))
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
            return View(intergalacticPackages);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.IntergalacticPackages == null)
            {
                return NotFound();
            }

            var intergalacticPackages = await _context.IntergalacticPackages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intergalacticPackages == null)
            {
                return NotFound();
            }

            return View(intergalacticPackages);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IntergalacticPackages == null)
            {
                return Problem("Entity set 'ApplicationDbContext.IntergalacticPackages'  is null.");
            }
            var intergalacticPackages = await _context.IntergalacticPackages.FindAsync(id);
            if (intergalacticPackages != null)
            {
                _context.IntergalacticPackages.Remove(intergalacticPackages);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IntergalacticPackagesExists(int id)
        {
          return (_context.IntergalacticPackages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
