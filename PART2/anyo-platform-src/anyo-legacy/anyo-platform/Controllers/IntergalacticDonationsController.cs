using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using anyo_platform.Models;
using Microsoft.AspNet.Identity;

namespace anyo_platform.Controllers
{
    public class IntergalacticDonationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: IntergalacticDonations
        public ActionResult Index()
        {
            var intergalacticDonations = db.IntergalacticDonations.Include(i => i.Donor).Include(i => i.Package);
            return View(intergalacticDonations.ToList());
        }

        // GET: IntergalacticDonations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntergalacticDonation intergalacticDonation = db.IntergalacticDonations.Find(id);
            if (intergalacticDonation == null)
            {
                return HttpNotFound();
            }
            return View(intergalacticDonation);
        }

        // GET: IntergalacticDonations/Create
        public ActionResult Create(int? id)
        {
            ViewBag.DonorId = User.Identity.GetUserId();
            ViewBag.MissionId = id;
            ViewBag.PackageId = new SelectList(db.IntergalacticPackages, "Id", "Name");
            return View();
        }

        // POST: IntergalacticDonations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Message,Quantity,MissionId,DonorId,PackageId")] IntergalacticDonation intergalacticDonation)
        {
            if (ModelState.IsValid)
            {
                intergalacticDonation.CreateDate = DateTime.Now;

                var mission = db.IntergalaticMissions.Find(intergalacticDonation.MissionId);

                if (mission != null)
                {
                    mission.Current += intergalacticDonation.Quantity;

                    db.Entry(mission).State = EntityState.Modified;
                    
                    db.IntergalacticDonations.Add(intergalacticDonation);
                    db.SaveChanges();
                }
                else
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }

            ViewBag.DonorId = User.Identity.GetUserId();
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
