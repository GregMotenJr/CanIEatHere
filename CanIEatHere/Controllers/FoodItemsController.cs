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
    public class FoodItemsController : Controller
    {
        private CanIEatHereEntities db = new CanIEatHereEntities();

        // GET: FoodItems
        public ActionResult Index()
        {
            var foodItems = db.FoodItems.Include(f => f.Course).Include(f => f.DietaryRestriction).Include(f => f.DietaryRestriction1).Include(f => f.DietaryRestriction2).Include(f => f.Review);
            return View(foodItems.ToList());
        }

        // GET: FoodItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodItem foodItem = db.FoodItems.Find(id);
            if (foodItem == null)
            {
                return HttpNotFound();
            }
            return View(foodItem);
        }

        // GET: FoodItems/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseType");
            ViewBag.DietaryRestrictionID = new SelectList(db.DietaryRestrictions, "DietaryRestrictionID", "DietType");
            ViewBag.DietaryRestrictionID2 = new SelectList(db.DietaryRestrictions, "DietaryRestrictionID", "DietType");
            ViewBag.DietaryRestrictionID3 = new SelectList(db.DietaryRestrictions, "DietaryRestrictionID", "DietType");
            ViewBag.ReviewID = new SelectList(db.Reviews, "ReviewID", "UserID");
            return View();
        }

        // POST: FoodItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FoodItemID,FoodItemName,ListIngredients,FoodItemRating,CourseID,ReviewID,DietaryRestrictionID,DietaryRestrictionID2,DietaryRestrictionID3")] FoodItem foodItem)
        {
            if (ModelState.IsValid)
            {
                db.FoodItems.Add(foodItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseType", foodItem.CourseID);
            ViewBag.DietaryRestrictionID = new SelectList(db.DietaryRestrictions, "DietaryRestrictionID", "DietType", foodItem.DietaryRestrictionID);
            ViewBag.DietaryRestrictionID2 = new SelectList(db.DietaryRestrictions, "DietaryRestrictionID", "DietType", foodItem.DietaryRestrictionID2);
            ViewBag.DietaryRestrictionID3 = new SelectList(db.DietaryRestrictions, "DietaryRestrictionID", "DietType", foodItem.DietaryRestrictionID3);
            ViewBag.ReviewID = new SelectList(db.Reviews, "ReviewID", "UserID", foodItem.ReviewID);
            return View(foodItem);
        }

        // GET: FoodItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodItem foodItem = db.FoodItems.Find(id);
            if (foodItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseType", foodItem.CourseID);
            ViewBag.DietaryRestrictionID = new SelectList(db.DietaryRestrictions, "DietaryRestrictionID", "DietType", foodItem.DietaryRestrictionID);
            ViewBag.DietaryRestrictionID2 = new SelectList(db.DietaryRestrictions, "DietaryRestrictionID", "DietType", foodItem.DietaryRestrictionID2);
            ViewBag.DietaryRestrictionID3 = new SelectList(db.DietaryRestrictions, "DietaryRestrictionID", "DietType", foodItem.DietaryRestrictionID3);
            ViewBag.ReviewID = new SelectList(db.Reviews, "ReviewID", "UserID", foodItem.ReviewID);
            return View(foodItem);
        }

        // POST: FoodItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FoodItemID,FoodItemName,ListIngredients,FoodItemRating,CourseID,ReviewID,DietaryRestrictionID,DietaryRestrictionID2,DietaryRestrictionID3")] FoodItem foodItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseType", foodItem.CourseID);
            ViewBag.DietaryRestrictionID = new SelectList(db.DietaryRestrictions, "DietaryRestrictionID", "DietType", foodItem.DietaryRestrictionID);
            ViewBag.DietaryRestrictionID2 = new SelectList(db.DietaryRestrictions, "DietaryRestrictionID", "DietType", foodItem.DietaryRestrictionID2);
            ViewBag.DietaryRestrictionID3 = new SelectList(db.DietaryRestrictions, "DietaryRestrictionID", "DietType", foodItem.DietaryRestrictionID3);
            ViewBag.ReviewID = new SelectList(db.Reviews, "ReviewID", "UserID", foodItem.ReviewID);
            return View(foodItem);
        }

        // GET: FoodItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodItem foodItem = db.FoodItems.Find(id);
            if (foodItem == null)
            {
                return HttpNotFound();
            }
            return View(foodItem);
        }

        // POST: FoodItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodItem foodItem = db.FoodItems.Find(id);
            db.FoodItems.Remove(foodItem);
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
