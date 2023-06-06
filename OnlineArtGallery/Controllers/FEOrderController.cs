using OnlineArtGallery.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace OnlineArtGallery.Controllers
{
    public class FEOrderController : Controller
    {
        private GalleryArtEntities db = new GalleryArtEntities();
        public ActionResult Cart()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Index", "FEHome");

            string id = Session["UserId"].ToString();
            var userId = int.Parse(id);
            var cart = db.Carts.Where(a => a.user_id == userId).OrderByDescending(a => a.cart_id).ToList();
            ViewBag.Cart = cart;
            return View();
        }

        public string Create(int id)
        {
            if (Session["UserId"] == null)
                return "Please login first ! ";

            bool check = db.Favourites.Any(a => a.artwork_id == id);
            if (!check)
            {
                var cart = new Cart()
                {
                    user_id = int.Parse(Session["UserId"].ToString()),
                    artwork_id = id
                };

                db.Carts.Add(cart);
                db.SaveChanges();
                return "Added !";
            }
            return "Already added !";
        }
        public ActionResult Delete(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Index", "FEHome");
            var cart = db.Carts.Find(id);
            db.Carts.Remove(cart);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Checkout()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Index", "FEHome");

            string id = Session["UserId"].ToString();
            var userId = int.Parse(id);
            var auth = db.Users.Find(userId);
           
            ViewBag.User = auth;
            return View();
        }
    }
}