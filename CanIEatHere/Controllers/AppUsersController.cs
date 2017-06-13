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
    public class AppUsersController : Controller
    {
        private CanIEatHereEntities db = new CanIEatHereEntities();

        // GET: AppUsers
        public ActionResult Index()
        {
            var appUsers = db.AppUsers.Include(a => a.AspNetUser).Include(a => a.DietaryRestriction).Include(a => a.DietaryRestriction1).Include(a => a.DietaryRestriction2);
            return View(appUsers.ToList());
        }

        // GET: AppUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = db.AppUsers.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        // GET: AppUsers/Create
        public ActionResult Create()
        {
            ViewBag.ASPUserID = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.DietaryRestrictionID1 = new SelectList(db.DietaryRestrictions, "DietaryRestrictionID", "DietType");
            ViewBag.DietaryRestrictionID2 = new SelectList(db.DietaryRestrictions, "DietaryRestrictionID", "DietType");
            ViewBag.DietaryRestrictionID3 = new SelectList(db.DietaryRestrictions, "DietaryRestrictionID", "DietType");
            return View();
        }

        // POST: AppUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppUserID,ASPUserID,AppUserName,FirstName,LastName,City,State,ZIP,DietaryRestrictionID1,DietaryRestrictionID2,DietaryRestrictionID3")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                db.AppUsers.Add(appUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ASPUserID = new SelectList(db.AspNetUsers, "Id", "Email", appUser.ASPUserID);
            ViewBag.DietaryRestrictionID1 = new SelectList(db.DietaryRestrictions, "DietaryRestrictionID", "DietType", appUser.DietaryRestrictionID1);
            ViewBag.DietaryRestrictionID2 = new SelectList(db.DietaryRestrictions, "DietaryRestrictionID", "DietType", appUser.DietaryRestrictionID2);
            ViewBag.DietaryRestrictionID3 = new SelectList(db.DietaryRestrictions, "DietaryRestrictionID", "DietType", appUser.DietaryRestrictionID3);
            return View(appUser);
        }

        // GET: AppUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = db.AppUsers.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.ASPUserID = new SelectList(db.AspNetUsers, "Id", "Email", appUser.ASPUserID);
            ViewBag.DietaryRestrictionID1 = new SelectList(db.DietaryRestrictions, "DietaryRestrictionID", "DietType", appUser.DietaryRestrictionID1);
            ViewBag.DietaryRestrictionID2 = new SelectList(db.DietaryRestrictions, "DietaryRestrictionID", "DietType", appUser.DietaryRestrictionID2);
            ViewBag.DietaryRestrictionID3 = new SelectList(db.DietaryRestrictions, "DietaryRestrictionID", "DietType", appUser.DietaryRestrictionID3);
            return View(appUser);
        }

        // POST: AppUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppUserID,ASPUserID,AppUserName,FirstName,LastName,City,State,ZIP,DietaryRestrictionID1,DietaryRestrictionID2,DietaryRestrictionID3")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ASPUserID = new SelectList(db.AspNetUsers, "Id", "Email", appUser.ASPUserID);
            ViewBag.DietaryRestrictionID1 = new SelectList(db.DietaryRestrictions, "DietaryRestrictionID", "DietType", appUser.DietaryRestrictionID1);
            ViewBag.DietaryRestrictionID2 = new SelectList(db.DietaryRestrictions, "DietaryRestrictionID", "DietType", appUser.DietaryRestrictionID2);
            ViewBag.DietaryRestrictionID3 = new SelectList(db.DietaryRestrictions, "DietaryRestrictionID", "DietType", appUser.DietaryRestrictionID3);
            return View(appUser);
        }

        // GET: AppUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = db.AppUsers.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        // POST: AppUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AppUser appUser = db.AppUsers.Find(id);
            db.AppUsers.Remove(appUser);
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
