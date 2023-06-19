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

        [HttpPost]
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
        [HttpPost]
        public string LoginFB(User user)
        {
            bool check = db.Users.Any(e => e.facebook_id.Equals(user.facebook_id));
            if (check == true)
            {
                var data = db.Users.FirstOrDefault(e => e.facebook_id.Equals(user.facebook_id));

                Session["UserId"] = data.user_id;
                Session["UserFName"] = data.user_fname;
                Session["UserLevel"] = data.user_level;
                Session["UserImage"] = data.user_image;
                return "Successfully";

            }
            else
            {
                var auth = new User();
                auth.user_fname = user.user_fname;
                auth.facebook_id = user.facebook_id;
                auth.user_password = "123456";
                auth.user_is_active = true;
                auth.user_level = 2;
                db.Users.Add(auth);
                db.SaveChanges();

                var data = db.Users.FirstOrDefault(e => e.facebook_id.Equals(user.facebook_id));

                Session["UserId"] = data.user_id;
                Session["UserFName"] = data.user_fname;
                Session["UserLevel"] = data.user_level;
                Session["UserImage"] = data.user_image;
                return "Successfully";
            }
        }

        [HttpPost]
        public string LoginGG(User user)
        {
            bool check = db.Users.Any(e => e.google_id.Equals(user.google_id));
            if (check == true)
            {
                var data = db.Users.FirstOrDefault(e => e.google_id.Equals(user.google_id));

                Session["UserId"] = data.user_id;
                Session["UserFName"] = data.user_fname;
                Session["UserLevel"] = data.user_level;
                Session["UserImage"] = data.user_image;
                return "Successfully";

            }
            else
            {
                var auth = new User();
                auth.user_fname = user.user_fname;
                auth.user_email = "";
                auth.google_id = user.google_id;
                auth.user_password = "123456";
                auth.user_is_active = true;
                auth.user_level = 2;
                db.Users.Add(auth);
                db.SaveChanges();

                var data = db.Users.FirstOrDefault(e => e.facebook_id.Equals(user.facebook_id));

                Session["UserId"] = data.user_id;
                Session["UserFName"] = data.user_fname;
                Session["UserLevel"] = data.user_level;
                Session["UserImage"] = data.user_image;
                return "Successfully";
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "FEHome");
        }

        
    }
}