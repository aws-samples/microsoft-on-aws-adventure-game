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
    public class IntergalacticPackagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: IntergalacticPackages
        public ActionResult Index()
        {
            return View(db.IntergalacticPackages.ToList());
        }

        // GET: IntergalacticPackages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntergalacticPackages intergalacticPackages = db.IntergalacticPackages.Find(id);
            if (intergalacticPackages == null)
            {
                return HttpNotFound();
            }
            return View(intergalacticPackages);
        }

        // GET: IntergalacticPackages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IntergalacticPackages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,PackageContents,PackageArt,PackageType")] IntergalacticPackages intergalacticPackages)
        {
            if (ModelState.IsValid)
            {
                intergalacticPackages.CreateDate = DateTime.Now;
                
                db.IntergalacticPackages.Add(intergalacticPackages);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(intergalacticPackages);
        }

        // GET: IntergalacticPackages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntergalacticPackages intergalacticPackages = db.IntergalacticPackages.Find(id);
            if (intergalacticPackages == null)
            {
                return HttpNotFound();
            }
            return View(intergalacticPackages);
        }

        // POST: IntergalacticPackages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,PackageContents,PackageArt,PackageType,CreateDate")] IntergalacticPackages intergalacticPackages)
        {
            if (ModelState.IsValid)
            {
                db.Entry(intergalacticPackages).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(intergalacticPackages);
        }

        // GET: IntergalacticPackages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntergalacticPackages intergalacticPackages = db.IntergalacticPackages.Find(id);
            if (intergalacticPackages == null)
            {
                return HttpNotFound();
            }
            return View(intergalacticPackages);
        }

        // POST: IntergalacticPackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IntergalacticPackages intergalacticPackages = db.IntergalacticPackages.Find(id);
            db.IntergalacticPackages.Remove(intergalacticPackages);
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
