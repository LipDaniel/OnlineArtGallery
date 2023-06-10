using OnlineArtGallery.Models.Entities;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace OnlineArtGallery.Controllers
{
    public class BECategoryController : Controller
    {
        private GalleryArtEntities db = new GalleryArtEntities();

        // GET: BECategory/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name");
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult NewCategory(Category newcat, HttpPostedFileBase category_image)
        {
            var ncategory = new Category();
            ncategory.category_name = newcat.category_name;
            ncategory.category_created_date = DateTime.Now.ToString("dd/MM/yyyy");
            ncategory.category_is_active = true;

            if (category_image != null)
            {
                var fileName = Path.GetFileName(category_image.FileName);
                string fileExtension = Path.GetExtension(category_image.FileName);
                string file = fileName + DateTime.Now.ToString("ddMMyyyyHHmmss") + fileExtension;
                var path = Path.Combine(Server.MapPath("~/Content/Assets/Images/categoryList_img/"), file);


                category_image.SaveAs(path);
                ncategory.category_image = file;
            }

            db.Categories.Add(ncategory);
            db.SaveChanges();


            return RedirectToAction("CategoryList", "BEIndex");
        }
        public ActionResult Edit(int id)
        {
            var editval = db.Categories.Find(id);

            return RedirectToAction("CategoryList", "BEIndex", editval);
        }

        //post edit 
        [HttpPost]
        public ActionResult Edit(Category category, HttpPostedFileBase category_image)
        {
            var nobj = db.Categories.Find(category.category_id);
            if (nobj == null)
            {
                return RedirectToAction("CategoryList", "BEIndex");
            }
            nobj.category_name = category.category_name;

            if (category_image != null)
            {
                if (category.category_image != null)
                {
                    string gallpath = Path.Combine(Server.MapPath("~/Content/Assets/Images/User/"), category.category_image);
                    if (System.IO.File.Exists(gallpath))
                    {
                        // Delete the file
                        System.IO.File.Delete(gallpath);

                        // Perform any other necessary actions
                    }
                }
                var fileName = Path.GetFileName(category_image.FileName);
                string fileExtension = Path.GetExtension(category_image.FileName);
                string file = fileName + DateTime.Now.ToString("ddMMyyyyHHmmss") + fileExtension;
                var path = Path.Combine(Server.MapPath("~/Content/Assets/Images/categoryList_img/"), file);

                category_image.SaveAs(path);
                category.category_image = file;
            }
            db.SaveChanges();
            return RedirectToAction("CategoryList", "BEIndex");
        }

    }

}