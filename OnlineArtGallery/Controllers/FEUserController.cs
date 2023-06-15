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
            var art = db.Artists.ToList();
            var cate = db.Categories.ToList();
            var auth = db.Users.Find(Session["UserId"]);
            if (auth == null)
            {
                return RedirectToAction("Index", "FEHome");
            }
            ViewBag.User = auth;
            ViewBag.Artists = art;
            ViewBag.Category = cate;
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
            db.SaveChanges();

            return "Change password successfully !";
        }

        public ActionResult RequestArtwork(Artwork model, string height,string width, HttpPostedFileBase artwork_image)
        {
            var artwork = new Artwork()
            {
                artist_id = model.artist_id,
                category_id = model.category_id,
                artwork_name = model.artwork_name,
                artwork_description = model.artwork_description,
                artwork_dimensions = width + " x " + height,
                artwork_price = model.artwork_price,
                artwork_status = 4,
                artwork_date = model.artwork_date
            };
            db.Artworks.Add(artwork);

            var auth = db.Users.Find(Session["UserId"]);

            if (artwork_image != null && artwork_image.ContentLength > 0)
            {
                string fileExtension = Path.GetExtension(artwork_image.FileName);
                string uniqueFileName = auth.user_id.ToString() + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                string filePath = Path.Combine(Server.MapPath("~/Content/Assets/Images/Artwork/"), uniqueFileName);
                artwork_image.SaveAs(filePath);
                artwork.artwork_image = uniqueFileName;
            }
            db.SaveChanges();
            var noti = new Notification
            {
                notification_recipient_id = int.Parse(auth.user_id.ToString()),
                notification_title = "New request for artwork!",
                notification_is_read = false,
                notification_message = auth.user_fname + "" + auth.user_lname + " upload new artwork.",
                notificaiton_click_url = "/BERequestArtwork/" + artwork.artwork_id

            };
            db.Notifications.Add(noti);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Request artwork successfully.";
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}