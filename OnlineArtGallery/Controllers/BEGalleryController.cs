
using OnlineArtGallery.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using OnlineArtGallery.Models.ModelView;
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
                string file = fileName + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                var path = Path.Combine(Server.MapPath("~/Content/Assets/Images/galleryimg/"), file);



                gallery_image.SaveAs(path);
                gal.gallery_image = file;
            }

            db.Galleries.Add(gal);
            db.SaveChanges();

            Session["Message"] = "Seccessful";
            return RedirectToAction("GalleryList", "BEIndex");
        }
        public ActionResult Edit(int id)
        {
            var galler = db.Galleries.Find(id);

            return RedirectToAction("GalleryList", "BEIndex", galler);



        }



        //post edit 
        [HttpPost]
        public ActionResult NewEdit(Gallery gallery, HttpPostedFileBase gallery_image)
        {
            var galle = db.Galleries.Find(gallery.gallery_id);
            if (galle == null)
            {
                return RedirectToAction("GalleryList", "BEIndex");
            }
            galle.gallery_name = gallery.gallery_name;
            galle.gallery_description = gallery.gallery_description;

            if (gallery_image != null)
            {
                if (galle.gallery_image != null)
                {
                    string gallpath = Path.Combine(Server.MapPath("~/Content/Assets/Images/User/"), galle.gallery_image);
                    if (System.IO.File.Exists(gallpath))
                    {
                        // Delete the file
                        System.IO.File.Delete(gallpath);

                        // Perform any other necessary actions
                    }
                }
                var fileName = Path.GetFileName(gallery_image.FileName);
                string fileExtension = Path.GetExtension(gallery_image.FileName);
                string file = fileName + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                var path = Path.Combine(Server.MapPath("~/Content/Assets/Images/galleryimg/"), file);



                gallery_image.SaveAs(path);
                galle.gallery_image = file;
            }
            db.SaveChanges();
            Session["Message"] = "Seccessful";
            return RedirectToAction("GalleryList", "BEIndex");


        }
        
        public string AddArtwork(int id, List<int> arr)
        {

            foreach(var item in arr)
            {
                var galArt = new Artwork_Gallery()
                {
                    gallery_id = id,
                    artwork_id = int.Parse(item.ToString())
                };
                db.Artwork_Gallery.Add(galArt);
                db.SaveChanges();

            }
            return "Success";
            
        }
    }
}