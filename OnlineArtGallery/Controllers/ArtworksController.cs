using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineArtGallery.Models.Entities;

namespace OnlineArtGallery.Controllers
{
    public class ArtworksController : Controller
    {
        private GalleryArtEntities db = new GalleryArtEntities();

        // GET: Artworks
        public ActionResult Index()
        {
            var artworks = db.Artworks.Include(a => a.Artist).Include(a => a.Category);
            return View(artworks.ToList());
        }

        // GET: Artworks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artwork artwork = db.Artworks.Find(id);
            if (artwork == null)
            {
                return HttpNotFound();
            }
            return View(artwork);
        }

        // GET: Artworks/Create
        public ActionResult Create()
        {
            ViewBag.artist_id = new SelectList(db.Artists, "artist_id", "artist_name");
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name");
            return View();
        }

        // POST: Artworks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "artwork_id,artist_id,artwork_name,artwork_image,artwork_description,artwork_price,artwork_dimensions,artwork_is_sold,artwork_date,category_id")] Artwork artwork)
        {
            if (ModelState.IsValid)
            {
                db.Artworks.Add(artwork);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.artist_id = new SelectList(db.Artists, "artist_id", "artist_name", artwork.artist_id);
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name", artwork.category_id);
            return View(artwork);
        }

        // GET: Artworks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artwork artwork = db.Artworks.Find(id);
            if (artwork == null)
            {
                return HttpNotFound();
            }
            ViewBag.artist_id = new SelectList(db.Artists, "artist_id", "artist_name", artwork.artist_id);
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name", artwork.category_id);
            return View(artwork);
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

        // GET: Artworks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artwork artwork = db.Artworks.Find(id);
            if (artwork == null)
            {
                return HttpNotFound();
            }
            return View(artwork);
        }

        // POST: Artworks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artwork artwork = db.Artworks.Find(id);
            db.Artworks.Remove(artwork);
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
