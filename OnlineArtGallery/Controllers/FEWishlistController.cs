using OnlineArtGallery.Models.Entities;
using OnlineArtGallery.Models.ModelView;
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
            var id = int.Parse(Session["UserId"].ToString());
            var list = (from favourite in db.Favourites
                            join artwork in db.Artworks on favourite.artwork_id equals artwork.artwork_id
                            where favourite.user_id == id
                            orderby favourite.favourite_id descending
                            select new
                            {
                                FavouriteId = favourite.favourite_id,
                                ArtworkId = artwork.artwork_id,
                                ArtworkName = artwork.artwork_name,
                                ArtworkPrice = artwork.artwork_price,
                                ArtworkStatus = artwork.artwork_status,
                                ArtworkImage = artwork.artwork_image
                            }).ToList();
            var wishlist = new List<Wishlist>();
            foreach (var item in list)
            {
                var wish = new Wishlist()
                {
                    ArtworkId = item.ArtworkId,
                    ArtworkImage = item.ArtworkImage,
                    ArtworkName = item.ArtworkName,
                    ArtworkPrice = item.ArtworkPrice,
                    ArtworkStatus = int.Parse(item.ArtworkStatus.ToString()),
                    FavoriteId = item.FavouriteId,
                };
                wishlist.Add(wish);

            }
            ViewBag.Wishlist = wishlist;
            
            return View();
        }
        public ActionResult Create(int id)
        {
            if (Session["UserId"] == null)
            {
                var resul = new
                {
                    noti = "Please login first!",
                };
                return Json(resul);
            }

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