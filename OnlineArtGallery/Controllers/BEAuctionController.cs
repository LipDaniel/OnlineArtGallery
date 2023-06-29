using OnlineArtGallery.Models;
using OnlineArtGallery.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineArtGallery.Controllers
{
    public class BEAuctionController : Controller
    {
        private GalleryArtEntities db = new GalleryArtEntities();
        // GET: BEAuction
        [HttpPost]
        public string Create(Auction obj)
        {
            var item = new Auction();
            item.artwork_id = obj.artwork_id;
            item.auction_reserve_price = obj.auction_reserve_price;
            item.auction_start_date = obj.auction_start_date+":00";
            item.auction_end_date = obj.auction_end_date + ":00";
            item.auction_current_bid = obj.auction_reserve_price;
            item.auction_created_date = DateTime.Now.ToString("yyyy-MM-dd");
            db.Auctions.Add(item);
            db.SaveChanges();

            var artwork = db.Artworks.Find(obj.artwork_id);
            artwork.artwork_status = 2;
            db.SaveChanges();

            return "Add auction successfully";
        }

        [HttpPost]
        public ActionResult CreateBid(string auction_amount, int id)
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

        public string GetAuctionSuccess()
        {
            var rnow = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

            DateTime now = DateTime.Parse(rnow);

            var auction = db.Auctions.ToList();
            foreach (var item in auction)
            {
                DateTime end = DateTime.Parse(item.auction_end_date);

                if(end <= now)
                {
                    var data = db.Auction_User.Where(e => e.auction_id == item.auction_id).OrderByDescending(d => d.auction_amount).FirstOrDefault();
                    if(data != null)
                    {
                        if(data.Auction.Artwork.artwork_status != 3)
                        {
                            var artwork = db.Artworks.Find(item.Artwork.artwork_id);
                            artwork.artwork_status = 6;
                            db.SaveChanges();

                            var auctions = db.Auctions.Find(item.auction_id);
                            auctions.user_id = data.user_id;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var auctions = db.Auctions.Find(item.auction_id);
                        db.Auctions.Remove(auctions);
                        db.SaveChanges();

                        var artwork = db.Artworks.Find(item.Artwork.artwork_id);
                        artwork.artwork_status = 0;
                        db.SaveChanges();
                    }
                }
            }
            return "Complete Auction";
        }
    }
}

