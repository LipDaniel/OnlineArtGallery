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
    public class ArtController : Controller
    {
        private GalleryArtEntities db = new GalleryArtEntities();
        public ActionResult Index()
        {
            var artitst = db.Artists.Include(a => a.artist_id).Include(a => a.artist_name);
            return View(artitst.ToList());
        }

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
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "artist_id, artist_name,artist_country,artist_bio,category_id")] Artist art)
        {
            if (ModelState.IsValid)
            {
                db.Artists.Add(art);
                db.SaveChanges();
                Debug.WriteLine(art);
                return RedirectToAction("Create", "ArtController");
            }

            ViewBag.artist_id = new SelectList(db.Artists, "artist_id", "artist_name", art.artist_id);
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name");
            return View(art);
        }

        //Get edit 
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist art = db.Artists.Find(id);
            if (art == null)
            {
                return HttpNotFound();
            }
            //ViewBag.artist_id = new SelectList(db.Artists, "artist_id", "artist_name", "artist_country", "artist_bio");
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name");
            return View(art);
        }

        // POST: Artworks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "artwork_id,artist_id,artwork_name,artwork_image,artwork_description,artwork_price,artwork_dimensions,artwork_is_sold,artwork_date,category_id")] Artwork artwork)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artwork).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.artist_id = new SelectList(db.Artists, "artist_id", "artist_name", artwork.artist_id);
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name", artwork.category_id);
            return View(artwork);
        }
    }
}