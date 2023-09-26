using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using anyo_platform.Models;

namespace anyo_platform.Controllers
{
    public class IntergalacticMissionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: IntergalaticMissions
        public ActionResult Index()
        {
            return View(db.IntergalaticMissions.ToList());
        }

        // GET: IntergalaticMissions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntergalacticMissions intergalaticMissions = db.IntergalaticMissions.Find(id);
            if (intergalaticMissions == null)
            {
                return HttpNotFound();
            }
            return View(intergalaticMissions);
        }

        // GET: IntergalaticMissions/Create
        public ActionResult Create()
        {
            ViewBag.GroupId = new SelectList(db.IntergalacticGroups, "Id", "Name");
            return View();
        }

        // POST: IntergalaticMissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Target,Banner,Status,GroupId")] IntergalacticMissions intergalaticMissions)
        {
            if (ModelState.IsValid)
            {
                intergalaticMissions.CreateDate = DateTime.Now;
                intergalaticMissions.EndDate = DateTime.Now.AddDays(31);

                db.IntergalaticMissions.Add(intergalaticMissions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupId = new SelectList(db.IntergalacticGroups, "Id", "Name");
            return View(intergalaticMissions);
        }

        // GET: IntergalaticMissions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntergalacticMissions intergalaticMissions = db.IntergalaticMissions.Find(id);
            if (intergalaticMissions == null)
            {
                return HttpNotFound();
            }

            ViewBag.GroupId = db.IntergalacticGroups.Find(intergalaticMissions.GroupId);
            return View(intergalaticMissions);
        }

        // POST: IntergalaticMissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Target,Banner,Status,CreateDate,EndDate,GroupId")] IntergalacticMissions intergalaticMissions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(intergalaticMissions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupId = db.IntergalacticGroups.Find(intergalaticMissions.GroupId);
            return View(intergalaticMissions);
        }

        // GET: IntergalaticMissions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntergalacticMissions intergalaticMissions = db.IntergalaticMissions.Find(id);
            if (intergalaticMissions == null)
            {
                return HttpNotFound();
            }
            return View(intergalaticMissions);
        }

        // POST: IntergalaticMissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IntergalacticMissions intergalaticMissions = db.IntergalaticMissions.Find(id);
            db.IntergalaticMissions.Remove(intergalaticMissions);
            db.SaveChanges();
            return RedirectToAction("Index");
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
