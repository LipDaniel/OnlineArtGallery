using OnlineArtGallery.Models.Entities;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineArtGallery.Controllers
{
    public class BECategoryController : Controller
    {
        //Connect Database 
        private GalleryArtEntities db = new GalleryArtEntities();


        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: BECategory/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name");
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult NewCategory(Category categorytable, HttpPostedFileBase category_image)
        {
            var categorytype = new Category();
            categorytype.category_name = categorytable.category_name;
            categorytype.category_created_date = DateTime.Now.ToString("yyyy/MM/dd");
            categorytype.category_is_status = categorytype.category_created_date = true;
 = true;
            if (category_image != null)
            {
                var fileName = Path.GetFileName(category_image.FileName);
                string fileExtension = Path.GetExtension(category_image.FileName);
                string file = fileName + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                var path = Path.Combine(Server.MapPath("~/Content/Assets/Images/categoryimg/"), file);



                category_name.SaveAs(path);
                categorytable.category_image = file;
            }

            db.Artists.Add(artis);
            db.SaveChanges();
            return RedirectToAction("ArtistList", "BEIndex");
        }
    }

}
