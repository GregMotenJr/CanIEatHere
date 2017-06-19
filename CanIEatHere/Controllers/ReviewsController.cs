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

            //again check who is logged in to see if have permission to edit from details page
            var loggedInUser = User.Identity.GetUserId();

            var userReviewIDs = db.Reviews
                 .Where(r => r.UserID == loggedInUser)
                 .Select(r => r.ReviewID);

            ViewBag.LoggedInUsersReviews = userReviewIDs.ToList();

            //get food items attached to the particular review
            var reviewFoodItems = db.FoodItems
                .Where(f => f.ReviewID == id)
                .Select(f => f);

            ViewBag.FoodItems = reviewFoodItems;

            //get the three possible dietary restrictions that might be attached to the food item
            var dr1 = (from d in db.DietaryRestrictions
                      join f in db.FoodItems
                      on d.DietaryRestrictionID equals f.DietaryRestrictionID
                      where f.ReviewID == id
                      select d.DietType).FirstOrDefault();

            var dr2 = (from d in db.DietaryRestrictions
                      join f in db.FoodItems
                      on d.DietaryRestrictionID equals f.DietaryRestrictionID2
                      where f.ReviewID == id
                      select d.DietType).FirstOrDefault();

            var dr3 = (from d in db.DietaryRestrictions
                      join f in db.FoodItems
                      on d.DietaryRestrictionID equals f.DietaryRestrictionID3
                      where f.ReviewID == id
                      select d.DietType).FirstOrDefault();

            ViewBag.DietaryRestriction1 = dr1;
            ViewBag.DietaryRestriction2 = dr2;
            ViewBag.DietaryRestriction3 = dr3;

            //get the course
            var courses = (db.Courses
                .Select(c => c));
            
            var foodItemCourse = (db.FoodItems
               .Where(f => f.ReviewID == id)
               .Select(f=> f.CourseID).FirstOrDefault());

            var course = "";
            foreach (var c in courses)
            {
                if (foodItemCourse == c.CourseID)
                {
                    course = c.CourseType;
                }
            }

            ViewBag.Course = course;

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


        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Create")]
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

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Add")]
        public ActionResult CreateFoodItems([Bind(Include = "ReviewID,UserID,NumFoodOptions,NumFoodOptionsRating,GeneralComments,OverallRating,TimeStamp,RestaurantID,RestaurantPriceRating,Img1,Img2,Img3")] Review review)
        {
            if (ModelState.IsValid)
            {
                review.UserID = User.Identity.GetUserId();
                db.Reviews.Add(review);
                db.SaveChanges();
                int Reviewid = review.ReviewID;
                return RedirectToAction("Create", "FoodItems", new { reviewID = Reviewid });

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

            //get food items attached to the particular review
            var reviewFoodItems = db.FoodItems
                .Where(f => f.ReviewID == id)
                .Select(f => f);

            ViewBag.FoodItems = reviewFoodItems;

            //get the three possible dietary restrictions that might be attached to the food item
            var dr1 = (from d in db.DietaryRestrictions
                       join f in db.FoodItems
                       on d.DietaryRestrictionID equals f.DietaryRestrictionID
                       where f.ReviewID == id
                       select d.DietType).FirstOrDefault();

            var dr2 = (from d in db.DietaryRestrictions
                       join f in db.FoodItems
                       on d.DietaryRestrictionID equals f.DietaryRestrictionID2
                       where f.ReviewID == id
                       select d.DietType).FirstOrDefault();

            var dr3 = (from d in db.DietaryRestrictions
                       join f in db.FoodItems
                       on d.DietaryRestrictionID equals f.DietaryRestrictionID3
                       where f.ReviewID == id
                       select d.DietType).FirstOrDefault();

            ViewBag.DietaryRestriction1 = dr1;
            ViewBag.DietaryRestriction2 = dr2;
            ViewBag.DietaryRestriction3 = dr3;

            //get the course
            var courses = (db.Courses
                .Select(c => c));

            var foodItemCourse = (db.FoodItems
               .Where(f => f.ReviewID == id)
               .Select(f => f.CourseID).FirstOrDefault());

            var course = "";
            foreach (var c in courses)
            {
                if (foodItemCourse == c.CourseID)
                {
                    course = c.CourseType;
                }
            }

            ViewBag.Course = course;

            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Save")]
        public ActionResult Edit([Bind(Include = "ReviewID,UserID,NumFoodOptions,NumFoodOptionsRating,GeneralComments,OverallRating,TimeStamp,RestaurantID,RestaurantPriceRating,Img1,Img2,Img3")] Review review)
        {
            if (ModelState.IsValid)
            {
                review.UserID = User.Identity.GetUserId();
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "PlaceID", review.RestaurantID);
            //ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", review.UserID);
            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Continue")]
        public ActionResult SaveAndEditFoodItems([Bind(Include = "ReviewID,UserID,NumFoodOptions,NumFoodOptionsRating,GeneralComments,OverallRating,TimeStamp,RestaurantID,RestaurantPriceRating,Img1,Img2,Img3")] Review review)
        {
            if (ModelState.IsValid)
            {
                review.UserID = User.Identity.GetUserId();
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                int reviewID = review.ReviewID;
                return RedirectToAction("Index","FoodItems", new { ReviewID = reviewID });
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

            //get food items attached to the particular review
            var reviewFoodItems = db.FoodItems
                .Where(f => f.ReviewID == id)
                .Select(f => f);

            ViewBag.FoodItems = reviewFoodItems;

            //get the three possible dietary restrictions that might be attached to the food item
            var dr1 = (from d in db.DietaryRestrictions
                       join f in db.FoodItems
                       on d.DietaryRestrictionID equals f.DietaryRestrictionID
                       where f.ReviewID == id
                       select d.DietType).FirstOrDefault();

            var dr2 = (from d in db.DietaryRestrictions
                       join f in db.FoodItems
                       on d.DietaryRestrictionID equals f.DietaryRestrictionID2
                       where f.ReviewID == id
                       select d.DietType).FirstOrDefault();

            var dr3 = (from d in db.DietaryRestrictions
                       join f in db.FoodItems
                       on d.DietaryRestrictionID equals f.DietaryRestrictionID3
                       where f.ReviewID == id
                       select d.DietType).FirstOrDefault();

            ViewBag.DietaryRestriction1 = dr1;
            ViewBag.DietaryRestriction2 = dr2;
            ViewBag.DietaryRestriction3 = dr3;

            //get the course
            var courses = (db.Courses
                .Select(c => c));

            var foodItemCourse = (db.FoodItems
               .Where(f => f.ReviewID == id)
               .Select(f => f.CourseID).FirstOrDefault());

            var course = "";
            foreach (var c in courses)
            {
                if (foodItemCourse == c.CourseID)
                {
                    course = c.CourseType;
                }
            }

            ViewBag.Course = course;
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
