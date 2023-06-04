using Microsoft.Ajax.Utilities;
using OnlineArtGallery.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineArtGallery.Controllers
{
    public class FEArtworkController : Controller
    {

        private GalleryArtEntities db = new GalleryArtEntities();
        public ActionResult ArtworkDetail(int id)
        {
            Artwork artwork = db.Artworks.Find(id);
            Artist artist = db.Artists.Find(artwork.artist_id);
            Category category = db.Categories.Find(artwork.category_id);
            if (artwork == null)
            {
                return RedirectToAction("Index","FEHome");
            }
            var artworkList = db.Artworks.Take(4).ToList();
            ViewBag.Artwork = artwork;
            ViewBag.Artist = artist;
            ViewBag.Category = category;  
            ViewBag.ArtworkList = artworkList;
            return View();
        }
    }
}