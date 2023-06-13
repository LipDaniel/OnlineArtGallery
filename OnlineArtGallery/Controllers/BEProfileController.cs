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

    public class BEProfileController : Controller
    {
        private GalleryArtEntities db = new GalleryArtEntities();

        // Post: BEProfile
        [HttpPost]
        public ActionResult NewEdit(User Admin, HttpPostedFileBase user_image)
        {
            var admin = db.Users.Find(Session["UserId"]);
            if (admin == null)
            {
                return RedirectToAction("Index", "FEHome");
            }

            admin.user_fname = Admin.user_fname;
            admin.user_lname = Admin.user_lname;
            admin.user_email = Admin.user_email;
            admin.user_phone = Admin.user_phone;





            if (user_image != null && user_image.ContentLength > 0)
            {
                if (admin.user_image != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Content/Assets/Images/User/"), admin.user_image);
                    if (System.IO.File.Exists(path))
                    {
                        // Delete the file
                        System.IO.File.Delete(path);

                        // Perform any other necessary actions
                    }
                }
                string fileExtension = Path.GetExtension(user_image.FileName);
                string uniqueFileName = admin.user_id.ToString() + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                string filePath = Path.Combine(Server.MapPath("~/Content/Assets/Images/user/"), uniqueFileName);
                user_image.SaveAs(filePath);

                admin.user_image = uniqueFileName;
            }
            TempData["SuccessMessage"] = "Change profile successfully.";
           
                

                db.SaveChanges();
                return RedirectToAction("Index", "BEIndex");
            


        }
    }
}