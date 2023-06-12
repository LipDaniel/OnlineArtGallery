using OnlineArtGallery.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineArtGallery.Controllers
{

    public class BEProfileController : Controller
    {
        private GalleryArtEntities db = new GalleryArtEntities();

        // Post: BEProfile
        [HttpPost]
        public ActionResult NewEdit(User Admin, HttpPostedFileBase user_image)
        {
            var admin = db.Users.Find(Admin.user_id);


            admin.user_fname = Admin.user_fname;
            admin.user_lname = Admin.user_lname;
            admin.user_email = Admin.user_email;
            admin.user_password = Admin.user_password;
            admin.user_address = Admin.user_address;
            admin.user_phone = Admin.user_phone;
            admin.user_is_active = true;
            if (user_image != null)
            {
                if (admin.user_image != null)
                {
                    string artpath = Path.Combine(Server.MapPath("~/Content/Assets/Images/User/"), admin.user_image);
                    if (System.IO.File.Exists(artpath))
                    {
                        // Delete the file
                        System.IO.File.Delete(artpath);

                        // Perform any other necessary actions
                    }
                }
                var fileName = Path.GetFileName(user_image.FileName);
                string fileExtension = Path.GetExtension(user_image.FileName);
                string file = fileName + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                var path = Path.Combine(Server.MapPath("~/Content/Assets/Images/user/"), file);



                user_image.SaveAs(path);
                admin.user_image = file;
            }
            if (admin != null)
            {
                TempData["msg"] = "change success";
                return RedirectToAction("Index", "BEIndex");
            }
            db.SaveChanges();
            return RedirectToAction("Index", "BEIndex");



        }
    }
}