using OnlineArtGallery.Models.Entities;
using System;
using System.Collections.Generic;
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

            var auth = db.Users.Where(a => a.user_email == user.user_email && a.user_password== user.user_password).FirstOrDefault();
            if(auth.user_is_active == true)
            {
                Session["UserId"] = auth.user_id;
                Session["UserEmail"] = auth.user_email;
                Session["UserFName"] = auth.user_fname;
                Session["UserLevel"] = auth.user_level;
                Session["UserImage"] = auth.user_image;

                return Redirect(Request.UrlReferrer.ToString());
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

        public ActionResult UserEdit(User user)
        {
            
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}