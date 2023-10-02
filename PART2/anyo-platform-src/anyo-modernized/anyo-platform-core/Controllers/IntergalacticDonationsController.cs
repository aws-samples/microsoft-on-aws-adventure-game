using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using anyo_platform.Models;
using anyo_platform_core.Data;
using System.Security.Claims;
using anyo_platform_core.Data.ViewModels;
using System.Security.Policy;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace anyo_platform_core.Controllers
{
    [Authorize]
    public class IntergalacticDonationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IntergalacticDonationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            string url = "https://localhost:7704/Donations/Get";

            using (HttpClient client = new HttpClient())
            {
                var json = await client.GetStringAsync(url);
                var apiModel = JsonConvert.DeserializeObject<DonationsViewModel>(json);

                if (apiModel == null)
                    return BadRequest();

                return View(apiModel);
            }
        }

        public async Task<IActionResult> OldIndex()
        {
            var applicationDbContext = _context.IntergalacticDonation;
            var indexViewModel = new DonationsViewModel();

            foreach (var donation in applicationDbContext.ToList())
            {
                var model = new DonationViewModel()
                {
                    Message = donation.Message,
                    Id = donation.Id,
                    Quantity = donation.Quantity,
                    Mission = _context.IntergalacticMissions.FirstOrDefault(m => m.Id == donation.MissionId),
                    Package = _context.IntergalacticPackages.FirstOrDefault(m => m.Id == donation.PackageId),
                    DonorName = donation.DonorName
                };

                indexViewModel.Donations.Add(model);
            }

            return View(indexViewModel);
        }

        public IActionResult Create(int? id)
        {
            ViewData["DonorName"] = User.Identity.Name;
            ViewData["MissionId"] = id;
            ViewData["PackageId"] = new SelectList(_context.IntergalacticPackages, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Message,Quantity,MissionId,DonorName,PackageId")] IntergalacticDonation intergalacticDonation)
        {
            if (ModelState.IsValid)
            {
                var mission = _context.IntergalacticMissions.Find(intergalacticDonation.MissionId);

                if (mission != null)
                {
                    mission.Current += intergalacticDonation.Quantity;
                }

                _context.Add(intergalacticDonation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["DonorName"] = intergalacticDonation.DonorName;
            ViewData["MissionId"] = intergalacticDonation.MissionId;
            ViewData["PackageId"] = new SelectList(_context.IntergalacticPackages, "Id", "Name", intergalacticDonation.PackageId);
            return View(intergalacticDonation);
        }


        private bool IntergalacticDonationExists(int id)
        {
          return (_context.IntergalacticDonation?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
