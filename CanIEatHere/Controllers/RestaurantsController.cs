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
    public class RestaurantsController : Controller
    {
        private CanIEatHereEntities db = new CanIEatHereEntities();

        // GET: Restaurants
        public ActionResult Index()
        {
            return View(db.Restaurants.ToList());
        }

        // GET: Restaurants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }

            ViewBag.Image1 = db.Reviews
                .Where(r => r.RestaurantID == id)
                .Select(r => r.Img1).ToString();

            ViewBag.Image2 = db.Reviews
                .Where(r => r.RestaurantID == id)
                .Select(r => r.Img2).ToString();

            ViewBag.Image3 = db.Reviews
                .Where(r => r.RestaurantID == id)
                .Select(r => r.Img3).ToString();

            var loggedInUser = User.Identity.GetUserId();

            //get a list of the reviews that are connected with the current logged in user
            //then can use the list to filter edit/delete permissions on reviews
            var userReviewIDs = db.Reviews
                 .Where(r => r.UserID == loggedInUser)
                 .Select(r => r.ReviewID);

            ViewBag.LoggedInUsersReviews = userReviewIDs.ToList();

            //get food items to display on the reviews index
            var reviewFoodItems = db.FoodItems
            .Select(f => f);

            var dietaryRestrictions = db.DietaryRestrictions
                .Select(d => d);

            ViewBag.FoodItems = reviewFoodItems;
            ViewBag.DietaryRestrictions = dietaryRestrictions;

            return View(restaurant);
        }

        // GET: Restaurants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RestaurantID,PlaceID")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Restaurants.Add(restaurant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(restaurant);
        }

        public JsonResult GetPlaceId(string searchString)
        {
            PlacesAPI placesAPI = new PlacesAPI();

            return Json(placesAPI.getPlaceID(searchString), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDetailsFromPlaceID (string placeIdString)
        {
            PlacesAPI placesAPI = new PlacesAPI();
            return Json(placesAPI.getDetailsFromPlaceID(placeIdString), JsonRequestBehavior.AllowGet);
        }

        // GET: Restaurants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RestaurantID,PlaceID")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            db.Restaurants.Remove(restaurant);
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
