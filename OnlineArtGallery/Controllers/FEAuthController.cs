using OnlineArtGallery.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineArtGallery.Controllers
{
    public class FEAuthController : Controller
    {
        private GalleryArtEntities db = new GalleryArtEntities();
        public string SignIn(User user)
        {
            var auth = db.Users.FirstOrDefault(a => a.user_email.Equals(user.user_email) && a.user_password.Equals(user.user_password));

            if(auth == null)
            {
                return "Wrong username or password";
            }

            if (auth.user_is_active == true)
            {
                Session["UserId"] = auth.user_id;
                Session["UserEmail"] = auth.user_email;
                Session["UserFName"] = auth.user_fname;
                Session["UserLevel"] = auth.user_level;
                Session["UserImage"] = auth.user_image;
                if(auth.user_level == 2)
                {
                    return "User";

                } else
                {
                    return "Admin";
                }
            }
            return "Account is blocked !!!";
        }

        [HttpPost]
        public string SignUp(User user)
        {
            bool check = db.Users.Any(e => e.user_email.Equals(user.user_email));
            if (check)
            {
                return "Email is already exists";
            }
            var auth = new User();
            auth.user_lname = user.user_lname;
            auth.user_fname = user.user_fname;
            auth.user_email = user.user_email;
            auth.user_password = user.user_password;
            auth.user_is_active = true;
            auth.user_level = 2;
            db.Users.Add(auth);
            db.SaveChanges();
            return "Successfully";
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "FEHome");
        }

        public ActionResult UserProfile()
        {
            if (Session["UserId"]  == null)
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

            
            if(user_image != null && user_image.ContentLength > 0)
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

        public string EditPassword(string new_password , string current_password, string reset_password)
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