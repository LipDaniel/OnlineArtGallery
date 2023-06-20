using OnlineArtGallery.Models.Entities;
using OnlineArtGallery.Models.ModelView;
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
            var cart = db.Carts.Where(a => a.user_id == int.Parse(Session["UserId"].ToString())).ToList();
            return View();
        }

        public ActionResult Create(int id)
        {
            if (Session["UserId"] == null)
                return Json("Please login first ! ");
            var userId = int.Parse(Session["UserId"].ToString());
            bool check = db.Carts.Any(a => a.artwork_id == id);
            if (!check)
            {
                var cart = new Cart()
                {
                    user_id = int.Parse(Session["UserId"].ToString()),
                    artwork_id = id
                };

                db.Carts.Add(cart);
                db.SaveChanges();
                var count = db.Carts.Where(b => b.user_id == userId).Count();
                var resultt = new
                {
                    noti = "Added !",
                    count
                };
                return Json(resultt);
               
            }
            var result = new
            {
                noti = "Already added !",
            };
            return Json(result);
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
            var cartList = db.Carts.Where(a => a.user_id == userId).ToList();
            var order = (from cart in db.Carts
                         join artwork in db.Artworks on cart.artwork_id equals artwork.artwork_id
                         where cart.user_id == userId
                         orderby cart.cart_id descending
                         select new
                         {
                             Cart = cart,
                             Artwork = artwork
                         }).ToList();
            CartView cartView;
            List<CartView> lstCart = new List<CartView>();
            foreach (var item in order)
            {
                cartView = new CartView
                {
                    ArtworkId = item.Artwork.artwork_id,
                    ArtworkName = item.Artwork.artwork_name,
                    ArtworkPrice = item.Artwork.artwork_price
                };
                
                lstCart.Add(cartView);
            }
            if (cartList != null)
            {
                var auth = db.Users.Find(userId);
                ViewBag.User = auth;
                ViewData["lstCart"] = lstCart;
                return View();

            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        
    }
}