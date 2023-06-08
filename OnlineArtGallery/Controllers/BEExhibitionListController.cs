using OnlineArtGallery.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineArtGallery.Controllers
{
    public class BEExhibitionListController : Controller
    {
        private GalleryArtEntities db = new GalleryArtEntities();
        public ActionResult CreateBE()
        {
            ViewBag.exhibition_id = new SelectList(db.Exhibitions, "exhibition_name", "exhibition_location", "exhibition_description");
            ViewBag.category = new SelectList(db.Categories, "category_id", "category_name");

            return View();
        }


        [HttpPost]
        // POST: Artworks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        public ActionResult NewExhibition(Exhibition Exhibition, HttpPostedFileBase exhibition_image)
        {
            var exhibition = new Exhibition();
            exhibition.exhibition_name = Exhibition.exhibition_name;
            exhibition.exhibition_location = Exhibition.exhibition_location;
            exhibition.exhibition_description = Exhibition.exhibition_description;
            exhibition.exhibition_start_date = DateTime.Now.ToString("yyyyMMddHHmmss");
            exhibition.exhibition_end_date = DateTime.Now.ToString("yyyyMMddHHmmss");
            exhibition.exhibition_is_status = true;
            if (exhibition_image != null)
            {
                var fileName = Path.GetFileName(exhibition_image.FileName);
                string fileExtension = Path.GetExtension(exhibition_image.FileName);
                string file = fileName + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                var path = Path.Combine(Server.MapPath("~/Content/Assets/Images/exhibition/"), file);



                exhibition_image.SaveAs(path);
                exhibition.exhibition_image = file;
            }

            db.Exhibitions.Add(exhibition);
            db.SaveChanges();


            return RedirectToAction("ExhibitionList", "BEIndex");

        }
        public ActionResult Edit(int id)
        {
            var galler = db.Galleries.Find(id);

            return RedirectToAction("ExhibitionList", "BEIndex", galler);



        }



        //post edit 
        [HttpPost]
        public ActionResult NewEdit(Gallery gallery, HttpPostedFileBase gallery_image)
        {
            var galle = db.Galleries.Find(gallery.gallery_id);
            if (galle == null)
            {
                return RedirectToAction("ExhibitionList", "BEIndex");
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
                var path = Path.Combine(Server.MapPath("~/Content/Assets/Images/exhibition/"), file);



                gallery_image.SaveAs(path);
                galle.gallery_image = file;
            }
            db.SaveChanges();
            return RedirectToAction("ExhibitionList", "BEIndex");


        }
    }
}