using OnlineArtGallery.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineArtGallery.Controllers
{
    public class FEAuctionController : Controller
    {
        private GalleryArtEntities db = new GalleryArtEntities();
        // GET: FEAuction
        public ActionResult Create(string auction_amount, int id)
        {
            if (Session["UserId"] == null)
                return Json("Login first!");

            var auc = new Auction_User()
            {
                auction_id = id,
                user_id = int.Parse(Session["UserId"].ToString()),
                auction_user_created_date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                auction_amount = auction_amount
            };
            db.Auction_User.Add(auc);

            var auction = db.Auctions.Find(id);
            auction.auction_current_bid = auction_amount;
            db.SaveChanges();

            var result = new
            {
                noti = "Bid successfully !",
                bid = auction_amount
            };
            return Json(result);
        }
    }
}