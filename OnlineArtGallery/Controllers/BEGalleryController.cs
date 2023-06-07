
using OnlineArtGallery.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
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
        public ActionResult NewGallery(Gallery gallery, HttpPostedFileBase gallery_image)
        {
            var gal = new Gallery();
            gal.gallery_name = gallery.gallery_name;
            gal.gallery_description = gallery.gallery_description;  
            gal.gallery_created_date = DateTime.Now.ToString("yyyy/MM/dd");
            gal.gellery_is_active = true;
            if (gallery_image != null)
            {
                var fileName = Path.GetFileName(gallery_image.FileName);
                string fileExtension = Path.GetExtension(gallery_image.FileName);
                string file = fileName + DateTime.Now.ToString("yyyyMMddHHmmss")+ fileExtension;  
                var path = Path.Combine(Server.MapPath("~/Content/Assets/Images/galleryimg/"), file);

                 
                
                gallery_image.SaveAs(path);
                gal.gallery_image = file;
            }

            db.Galleries.Add(gal);
            db.SaveChanges();
           

            return RedirectToAction("GalleryList", "BEIndex");
        }

    }
}