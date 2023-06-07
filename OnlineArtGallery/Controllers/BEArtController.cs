using OnlineArtGallery.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineArtGallery.Controllers
{
    public class BEArtController : Controller
    {

        private GalleryArtEntities db = new GalleryArtEntities();


        // GET: Art
        // GET: Art/Create
        public ActionResult Create()
        {
            ViewBag.artist_id = new SelectList(db.Artists, "artist_id", "artist_name", "artist_country");
            ViewBag.category = new SelectList(db.Categories, "category_id", "category_name");

            return View();
        }

        // POST: Artworks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult NewArt([Bind(Include = "artist_name,artist_country,artist_bio")] Artist art)
        {
            if (ModelState.IsValid)
            {
                db.Artists.Add(art);
                db.SaveChanges();
                return RedirectToAction("ArtistList", "BEIndex");
            }

            ViewBag.artist_id = new SelectList(db.Artists, "artist_id", "artist_name", art.artist_id);
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name");
            return RedirectToAction("ArtistList", "BEIndex");
        }

        //post edit 

        [HttpPost]
        public ActionResult NewEdit(int id, Artist artist)
        {
            var arr = db.Artists.Find(id);
            if (arr == null)
            {
                return RedirectToAction("ArtistList", "BEIndex");
            }
            return RedirectToAction("ArtistList", "BEIndex");

            arr.artist_name = artist.artist_name;
            arr.artist_country = artist.artist_country;
            arr.artist_country = artist.artist_bio;

        }
    }
}