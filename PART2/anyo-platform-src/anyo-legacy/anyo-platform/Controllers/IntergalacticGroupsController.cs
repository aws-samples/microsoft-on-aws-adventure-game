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
    public class IntergalacticGroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: IntergalacticGroups
        public ActionResult Index()
        {
            var intergalacticGroups = db.IntergalacticGroups.Include(i => i.Leader);
            return View(intergalacticGroups.ToList());
        }

        // GET: IntergalacticGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntergalacticGroup intergalacticGroup = db.IntergalacticGroups.Find(id);
            if (intergalacticGroup == null)
            {
                return HttpNotFound();
            }
            return View(intergalacticGroup);
        }

        // GET: IntergalacticGroups/Create
        public ActionResult Create()
        {
            ViewBag.LeaderId = new SelectList(db.Users, "Id", "UserName");
            return View();
        }

        // POST: IntergalacticGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,About,HomePlanet,Logo,LeaderId")] IntergalacticGroup intergalacticGroup)
        {
            if (ModelState.IsValid)
            {
                db.IntergalacticGroups.Add(intergalacticGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LeaderId = new SelectList(db.Users, "Id", "UserName", intergalacticGroup.LeaderId);
            return View(intergalacticGroup);
        }

        // GET: IntergalacticGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntergalacticGroup intergalacticGroup = db.IntergalacticGroups.Find(id);
            if (intergalacticGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.LeaderId = new SelectList(db.Users, "Id", "UserName", intergalacticGroup.LeaderId);
            return View(intergalacticGroup);
        }

        // POST: IntergalacticGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,About,HomePlanet,Logo,LeaderId")] IntergalacticGroup intergalacticGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(intergalacticGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LeaderId = new SelectList(db.Users, "Id", "UserName", intergalacticGroup.LeaderId);
            return View(intergalacticGroup);
        }

        // GET: IntergalacticGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntergalacticGroup intergalacticGroup = db.IntergalacticGroups.Find(id);
            if (intergalacticGroup == null)
            {
                return HttpNotFound();
            }
            return View(intergalacticGroup);
        }

        // POST: IntergalacticGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IntergalacticGroup intergalacticGroup = db.IntergalacticGroups.Find(id);
            db.IntergalacticGroups.Remove(intergalacticGroup);
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
