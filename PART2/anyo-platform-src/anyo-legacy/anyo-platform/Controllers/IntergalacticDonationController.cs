using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using anyo_platform.Models;
using anyo_platform.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace anyo_platform.Controllers
{
    [Authorize]
    public class IntergalacticDonationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var applicationDbContext = db.IntergalacticDonation;
            var indexViewModel = new DonationsViewModel();

            foreach (var donation in applicationDbContext.ToList())
            {
                var model = new DonationViewModel()
                {
                    Message = donation.Message,
                    Id = donation.Id,
                    Quantity = donation.Quantity,
                    Mission = db.IntergalacticMissions.FirstOrDefault(m => m.Id == donation.MissionId),
                    Package = db.IntergalacticPackages.FirstOrDefault(m => m.Id == donation.PackageId),
                    DonorName = donation.DonorName
                };

                indexViewModel.Donations.Add(model);
            }

            return View(indexViewModel);
        }

        public ActionResult Create(int? id)
        {
            ViewBag.DonorName = User.Identity.Name;
            ViewBag.MissionId = id;
            ViewBag.PackageId = new SelectList(db.IntergalacticPackages, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Message,Quantity,MissionId,DonorName,PackageId")] IntergalacticDonation intergalacticDonation)
        {
            if (ModelState.IsValid)
            {
                var mission = db.IntergalacticMissions.Find(intergalacticDonation.MissionId);

                if (mission != null)
                {
                    mission.Current += intergalacticDonation.Quantity;

                    db.Entry(mission).State = EntityState.Modified;   
                    db.IntergalacticDonation.Add(intergalacticDonation);
                    db.SaveChanges();
                }
                else
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }

            ViewBag.DonorName = intergalacticDonation.DonorName;
            ViewBag.MissionId = intergalacticDonation.MissionId;
            ViewBag.PackageId = new SelectList(db.IntergalacticPackages, "Id", "Name", intergalacticDonation.PackageId);
            return View(intergalacticDonation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
