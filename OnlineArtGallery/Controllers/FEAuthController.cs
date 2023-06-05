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
        public ActionResult SignIn(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            
            if(user.user_email == null && user.user_password == null)
            {
                return View(user);
            }

            var auth = db.Users.Where(a => a.user_email == user.user_email && a.user_password == user.user_password).FirstOrDefault();

            if(auth == null)
                return Redirect(Request.UrlReferrer.ToString());

            if (auth.user_is_active == true)
            {
                Session["UserId"] = auth.user_id;
                Session["UserEmail"] = auth.user_email;
                Session["UserFName"] = auth.user_fname;
                Session["UserLevel"] = auth.user_level;
                Session["UserImage"] = auth.user_image;
                if(auth.user_level == 2)
                {
                    return Redirect(Request.UrlReferrer.ToString());

                } else
                {
                    return RedirectToAction("Index", "BEIndex");
                }
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult SignUp(User user)
        {
            var auth = new User();
            auth.user_email = user.user_email;
            auth.user_password = user.user_password;
            auth.user_level = 2;
            db.Users.Add(auth);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return Redirect(Request.UrlReferrer.ToString());
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

        public ActionResult UserEdit(User user, string password, HttpPostedFileBase user_image)
        {
            var auth = db.Users.Find(Session["UserId"]);
            if (auth == null)
            {
                return RedirectToAction("Index", "FEHome");
            }
            
            auth.user_fname = user.user_fname;
            auth.user_lname = user.user_lname;
            auth.user_phone = user.user_phone;

            if(user.user_password != null)
            {
                if(password == auth.user_password)
                {
                    auth.user_password = user.user_password;
                    TempData["SuccessMessage"] = "Change password successfully.";
                }
                else
                {
                    TempData["FailedMessage"] = "Wrong password !!!!!!";
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
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

        
    }
}