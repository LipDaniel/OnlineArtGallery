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
        public string Create(int id)
        {
            if (Session["UserId"] == null)
                return "Please login first";

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
                return "Added"; 

            }
            return "Already added !";
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