using OnlineArtGallery.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineArtGallery.Controllers
{
    public class BETagController : Controller
    {
        private GalleryArtEntities db = new GalleryArtEntities();


        // GET: Art
        // GET: Art/Create
        public ActionResult Create()
        {
            ViewBag.tag.id = new SelectList(db.Tags, "tag_id", "tag_name");
            ViewBag.category = new SelectList(db.Categories, "category_id", "category_name");

            return View();
        }

        // POST: Artworks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult NewTag(Tag tag, HttpPostedFileBase tag_image)
        {
            var tager = new Tag();
            tager.tag_name = tag.tag_name;
            tager.tag_created_date = DateTime.Now.ToString("yyyy/MM/dd");
            tager.tag_is_status = true;
            if (tag_image != null)
            {
                var fileName = Path.GetFileName(tag_image.FileName);
                string fileExtension = Path.GetExtension(tag_image.FileName);
                string file = fileName + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                var path = Path.Combine(Server.MapPath("~/Content/Assets/Images/artistimg/"), file);



                tag_image.SaveAs(path);
                tag.tag_image = file;
            }

            db.Tags.Add(tag);
            db.SaveChanges();
            return RedirectToAction("TagList", "BEIndex");

        }


        //get edit 
        public ActionResult Edit(int id)
        {
            var arr = db.Artists.Find(id);

            return RedirectToAction("TagList", "BEIndex", arr);



        }



        //post edit 
        [HttpPost]
        public ActionResult NewEdit(Tag tag, HttpPostedFileBase tag_image)
        {
            var tager = db.Tags.Find(tag.tag_id);
            if (tager == null)
            {
                return RedirectToAction("TagList", "BEIndex");
            }
            tager.tag_name = tag.tag_name;
            tager.tag_created_date = tag.tag_created_date;
            if (tag_image != null)
            {
                if (tager.tag_image != null)
                {
                    string artpath = Path.Combine(Server.MapPath("~/Content/Assets/Images/User/"), tag.tag_image);
                    if (System.IO.File.Exists(artpath))
                    {
                        // Delete the file
                        System.IO.File.Delete(artpath);

                        // Perform any other necessary actions
                    }
                }
                var fileName = Path.GetFileName(tag_image.FileName);
                string fileExtension = Path.GetExtension(tag_image.FileName);
                string file = fileName + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                var path = Path.Combine(Server.MapPath("~/Content/Assets/Images/artistimg/"), file);



                tag_image.SaveAs(path);
                tager.tag_image = file;
            }
            db.SaveChanges();
            return RedirectToAction("TagList", "BEIndex");

        }
    }
}