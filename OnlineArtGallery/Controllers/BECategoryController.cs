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

        // POST: Category/Create
        [HttpPost]
        public ActionResult NewCategory(Category ncat, HttpPostedFileBase category_image)
        {
            var ncategory = new Category();
            ncategory.category_name = ncat.category_name;
            ncategory.category_created_date = DateTime.Now.ToString("dd/MM/yyyy");
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
    }

}
