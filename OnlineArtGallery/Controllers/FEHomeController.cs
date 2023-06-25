using OnlineArtGallery.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineArtGallery.Controllers
{
    public class FEHomeController : Controller
    {
        GalleryArtEntities db = new GalleryArtEntities();
        public ActionResult Index(){
            var galleryTop = db.Galleries.Where(a => a.gellery_is_active == true).OrderByDescending(a => a.gallery_id).ToList().Take(3);
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

      
   
    }
}