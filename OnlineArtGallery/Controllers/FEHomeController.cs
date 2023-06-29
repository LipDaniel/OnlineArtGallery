using OnlineArtGallery.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Parser.SyntaxTree;

namespace OnlineArtGallery.Controllers
{
    public class FEHomeController : Controller
    {
        GalleryArtEntities db = new GalleryArtEntities();
        public ActionResult Index(){
            var galleryTop = db.Galleries.OrderByDescending(a => a.gallery_id).ToList().Take(3);
            var artworkTop = db.Artworks.Where(b => b.artwork_status == 1 ).OrderByDescending(b => b.artwork_id).ToList().Take(6);
            var auctionTop = db.Auctions.OrderByDescending(b => b.auction_id).ToList();
            ViewBag.GalleryTop = galleryTop;
            ViewBag.ArtworkTop = artworkTop;
            ViewBag.AuctionTop = auctionTop;
            return View();
        }

        public ActionResult About(){
            return View();
        }

        public ActionResult Contact() { 
            ViewBag.Contact = db.Contacts.OrderByDescending(a=>a.contact_id).ToList();
            return View();
        }

        public ActionResult Count()
        {
            if(Session["UserId"] != null)
            {
                var id = int.Parse(Session["UserId"].ToString());
                var cartCount = db.Carts.Where(a => a.user_id == id).ToList().Count();
                var wishlistCount = db.Favourites.Where(a => a.user_id == id).ToList().Count();
                var notiCount = db.Notifications.Where(a => a.notification_sender_id == id && a.notification_is_read == false).ToList().Count();
                var result = new
                {
                    cart = cartCount,
                    wishlist = wishlistCount,
                    noti = notiCount
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            var resultt = new
            {
                cart = 0,
                wishlist = 0,
            };
            return Json(resultt, JsonRequestBehavior.AllowGet);
        }
      
   
    }
}