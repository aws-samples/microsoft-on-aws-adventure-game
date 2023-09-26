using System;
using Microsoft.AspNetCore.Mvc;
using anyo_platform.Models;
using anyo_platform_core.Data;
using anyo_platform_core.Data.ViewModels;

namespace anyo_platform_core.Controllers
{
    [Route("api/Donations")]
    [ApiController]
    public class IntergalacticDonationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IntergalacticDonationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/Donations/Get/")]
        public async Task<ActionResult> Get()
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

            return Ok(indexViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Donations/Create/")]
        public async Task<IActionResult> Create([FromBody] IntergalacticDonation intergalacticDonation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(intergalacticDonation);
                await _context.SaveChangesAsync();
                return Ok();
            }

            return BadRequest(ModelState);
        }
    }
}