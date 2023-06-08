using OnlineArtGallery.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineArtGallery.Controllers
{
    public class FEUserController : Controller
    {
        private GalleryArtEntities db = new GalleryArtEntities();
        public ActionResult UserProfile()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "FEHome");
            }
            var auth = db.Users.Find(Session["UserId"]);
            if (auth == null)
            {
                return RedirectToAction("Index", "FEHome");
            }
            ViewBag.User = auth;
            return View();
        }

        public ActionResult UserEdit(User user, HttpPostedFileBase user_image)
        {
            var auth = db.Users.Find(Session["UserId"]);
            if (auth == null)
            {
                return RedirectToAction("Index", "FEHome");
            }

            auth.user_fname = user.user_fname;
            auth.user_lname = user.user_lname;
            auth.user_phone = user.user_phone;


            if (user_image != null && user_image.ContentLength > 0)
            {
                if (auth.user_image != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Content/Assets/Images/User/"), auth.user_image);
                    if (System.IO.File.Exists(path))
                    {
                        // Delete the file
                        System.IO.File.Delete(path);

                        // Perform any other necessary actions
                    }
                }
                string fileExtension = Path.GetExtension(user_image.FileName);
                string uniqueFileName = auth.user_id.ToString() + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                string filePath = Path.Combine(Server.MapPath("~/Content/Assets/Images/User/"), uniqueFileName);
                user_image.SaveAs(filePath);

                auth.user_image = uniqueFileName;
            }
            TempData["SuccessMessage"] = "Change profile successfully.";
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public string EditPassword(string new_password, string current_password, string reset_password)
        {
            var auth = db.Users.Find(Session["UserId"]);

            if (current_password == null)
                return "Enter your password first !";

            if (new_password != reset_password)
                return "Your password doesen't match !";

            auth.user_password = new_password;
            return "Change password successfully !";
        } 
    }
}