using OnlineArtGallery.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineArtGallery.Controllers
{
    public class BEGalleryController : Controller
    {
        // GET: BEGallery
        private GalleryArtEntities db = new GalleryArtEntities();

        public ActionResult CreateBE()
        {
            ViewBag.gallery_id = new SelectList(db.Artists, "gallery_name", "gallery_image", "gallery_description");
            ViewBag.category = new SelectList(db.Categories, "category_id", "category_name");
            return View();
        }


        [HttpPost]
        // POST: Artworks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        public ActionResult NewGallery([Bind(Include = "gallery_name,gallery_image,gallery_description")] Gallery gallery)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Galleries.Add(gallery);
                    db.SaveChanges();
                    return RedirectToAction("GalleryList", "BEIndex");
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var error in ex.EntityValidationErrors)
                {
                    foreach (var validationError in error.ValidationErrors)
                    {
                        string errorMessage = $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
                        // Xử lý thông báo lỗi
                    }
                }
            }


            ViewBag.gallery_id = new SelectList(db.Galleries, "gallery_name", "gallery_image", "gallery_description");
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name");
            return RedirectToAction("GalleryList", "BEIndex");
        }

    }
}