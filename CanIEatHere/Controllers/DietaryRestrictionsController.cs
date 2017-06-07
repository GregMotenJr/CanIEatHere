using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CanIEatHere.Models;

namespace CanIEatHere.Controllers
{
    public class DietaryRestrictionsController : Controller
    {
        private CanIEatHereEntities db = new CanIEatHereEntities();

        // GET: DietaryRestrictions
        public ActionResult Index()
        {
            return View(db.DietaryRestrictions.ToList());
        }

        // GET: DietaryRestrictions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DietaryRestriction dietaryRestriction = db.DietaryRestrictions.Find(id);
            if (dietaryRestriction == null)
            {
                return HttpNotFound();
            }
            return View(dietaryRestriction);
        }

        // GET: DietaryRestrictions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DietaryRestrictions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DietaryRestrictionID,DietType")] DietaryRestriction dietaryRestriction)
        {
            if (ModelState.IsValid)
            {
                db.DietaryRestrictions.Add(dietaryRestriction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dietaryRestriction);
        }

        // GET: DietaryRestrictions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DietaryRestriction dietaryRestriction = db.DietaryRestrictions.Find(id);
            if (dietaryRestriction == null)
            {
                return HttpNotFound();
            }
            return View(dietaryRestriction);
        }

        // POST: DietaryRestrictions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DietaryRestrictionID,DietType")] DietaryRestriction dietaryRestriction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dietaryRestriction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dietaryRestriction);
        }

        // GET: DietaryRestrictions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DietaryRestriction dietaryRestriction = db.DietaryRestrictions.Find(id);
            if (dietaryRestriction == null)
            {
                return HttpNotFound();
            }
            return View(dietaryRestriction);
        }

        // POST: DietaryRestrictions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DietaryRestriction dietaryRestriction = db.DietaryRestrictions.Find(id);
            db.DietaryRestrictions.Remove(dietaryRestriction);
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
