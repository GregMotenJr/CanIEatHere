using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CanIEatHere.Models;
using Microsoft.AspNet.Identity;

namespace CanIEatHere.Controllers
{
    
    public class ReviewsController : Controller
    {
        private CanIEatHereEntities db = new CanIEatHereEntities();

        // GET: Reviews
        public ActionResult Index()
        {
            var reviews = db.Reviews.Include(r => r.Restaurant).Include(r => r.AspNetUser);

            var loggedInUser = User.Identity.GetUserId();

            var userReviewIDs = db.Reviews
                 .Where(r => r.UserID == loggedInUser)
                 .Select(r => r.ReviewID);

            ViewBag.LoggedInUsersReviews = userReviewIDs.ToList();

            return View(reviews.ToList());
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        [Authorize]
        // GET: Reviews/Create
        public ActionResult Create()
        {
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "PlaceID");
            //ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReviewID,UserID,NumFoodOptions,NumFoodOptionsRating,GeneralComments,OverallRating,TimeStamp,RestaurantID,RestaurantPriceRating,Img1,Img2,Img3")] Review review)
        {
            if (ModelState.IsValid)
            {
                review.UserID = User.Identity.GetUserId();
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "PlaceID", review.RestaurantID);
            //ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", review.UserID);
            return View(review);
        }

        public JsonResult GetPlaceId(string searchString)
        {
            PlacesAPI placesAPI = new PlacesAPI();

            return Json(placesAPI.getPlaceID(searchString), JsonRequestBehavior.AllowGet);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id, string userID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "PlaceID", review.RestaurantID);
            //ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", review.UserID);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReviewID,UserID,NumFoodOptions,NumFoodOptionsRating,GeneralComments,OverallRating,TimeStamp,RestaurantID,RestaurantPriceRating,Img1,Img2,Img3")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "PlaceID", review.RestaurantID);
            //ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", review.UserID);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
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
