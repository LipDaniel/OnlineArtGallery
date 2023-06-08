using OnlineArtGallery.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineArtGallery.Controllers
{
    public class FEWishlistController : Controller
    {
        private GalleryArtEntities db = new GalleryArtEntities();
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Index", "FEHome");

            string id = Session["UserId"].ToString();
            var userId = int.Parse(id);
            var wishlist = db.Favourites.Where(a => a.user_id == userId).OrderByDescending(a => a.favourite_id).ToList();
            ViewBag.Wishlist = wishlist;
            return View();
        }
        public ActionResult Create(int id)
        {
            if (Session["UserId"] == null)
                return Json("Please login first");

            var userId = int.Parse(Session["UserId"].ToString());
            bool check = db.Favourites.Any(a => a.artwork_id == id);
            if (!check)
            {
                var wishlist = new Favourite()
                {
                    user_id = int.Parse(Session["UserId"].ToString()),
                    artwork_id = id
                };
                db.Favourites.Add(wishlist);
                db.SaveChanges();
                
                var count = db.Favourites.Where(b => b.user_id == userId).Count();
                var resultt = new
                {
                    noti = "Added !",
                    count
                };
                return Json(resultt); 

            }
            var result = new
            {
                noti = "Already added !",
            };
            return Json(result);
        }
        public ActionResult Delete(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Index", "FEHome");
            var wishlist = db.Favourites.Find(id);
            db.Favourites.Remove(wishlist);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}