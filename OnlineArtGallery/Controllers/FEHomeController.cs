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
            var artworkTop = db.Artworks.OrderByDescending(b => b.artwork_id).ToList().Take(6);
            ViewBag.GalleryTop = galleryTop;
            ViewBag.ArtworkTop = artworkTop;
            return View();
        }

        public ActionResult About(){
            return View();
        }

        public ActionResult Contact() { 
            return View();
        }

   
    }
}