using Antlr.Runtime.Tree;
using OnlineArtGallery.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using OnlineArtGallery.Models.ModelView;
namespace OnlineArtGallery.Controllers
{
    public class BEExhibitionController : Controller
    {
        // GET: BEExhibition
        private GalleryArtEntities db = new GalleryArtEntities();
        
        public ActionResult AddGallery(Exhibition_Gallery model)
        {
            
            var add = new Exhibition_Gallery()
            {
                exhibition_id = model.exhibition_id,
                gallery_id = model.gallery_id,
                exhibition_gallery_created_date = model.exhibition_gallery_created_date,
                exhibition_gallery_end_date = model.exhibition_gallery_end_date,
                exhibition_gallery_is_active = true
            };
            var gal = db.Galleries.Find(model.gallery_id);
            gal.gellery_is_active = false;
            db.Exhibition_Gallery.Add(add);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());

        }


        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult NewExhibition(Exhibition exhibis, HttpPostedFileBase exhibition_image)
        {
            try
            {
                var exhibi = new Exhibition();
                exhibi.exhibition_name = exhibis.exhibition_name;
                exhibi.exhibition_location = exhibis.exhibition_location;
                exhibi.exhibition_description = exhibis.exhibition_description;
                exhibi.exhibition_is_status = true;

                if (exhibition_image != null && exhibition_image.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(exhibition_image.FileName);
                    string fileExtension = Path.GetExtension(exhibition_image.FileName);
                    string file = fileName + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                    var path = Path.Combine(Server.MapPath("~/Content/Assets/Images/exhibition/"), file);

                    exhibition_image.SaveAs(path);
                    exhibi.exhibition_image = file;
                }

                db.Exhibitions.Add(exhibi);
                db.SaveChanges();

                Session["Message"] = "Successful";
                return RedirectToAction("ExhibitionList", "BEIndex");
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(validationErrors => validationErrors.ValidationErrors)
                    .Select(validationError => $"{validationError.PropertyName}: {validationError.ErrorMessage}")
                    .ToList();

                // Log or handle the error messages as per your application's requirements

                Session["Message"] = "Validation failed: " + string.Join(", ", errorMessages);
                return RedirectToAction("ExhibitionList", "BEIndex");
            }
        }


       
        //get edit 
        public ActionResult NewEdit(Exhibition ex, HttpPostedFileBase exhibition_image)
        {
            var exhibi = db.Exhibitions.Find(ex.exhibition_id);
            if (exhibi == null)
            {
                return RedirectToAction("ExhibitionList", "BEIndex");
            }
            exhibi.exhibition_name = ex.exhibition_name;
            exhibi.exhibition_location = ex.exhibition_location;
            exhibi.exhibition_description = ex.exhibition_description;
           

            exhibi.exhibition_is_status = true;
            if (exhibition_image != null)
            {
                if (exhibi.exhibition_image != null)
                {
                    string gallpath = Path.Combine(Server.MapPath("~/Content/Assets/Images/User/"), exhibi.exhibition_image);
                    if (System.IO.File.Exists(gallpath))
                    {
                        // Delete the file
                        System.IO.File.Delete(gallpath);

                        // Perform any other necessary actions
                    }
                }
                var fileName = Path.GetFileName(exhibition_image.FileName);
                string fileExtension = Path.GetExtension(exhibition_image.FileName);
                string file = fileName + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                var path = Path.Combine(Server.MapPath("~/Content/Assets/Images/exhibition/"), file);



                exhibition_image.SaveAs(path);
                exhibi.exhibition_image = file;
            }
            db.SaveChanges();
            Session["Message"] = "Seccessful";
            return RedirectToAction("ExhibitionList", "BEIndex");




        }

    }
}