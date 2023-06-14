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
            item.auction_start_date = obj.auction_start_date;
            item.auction_end_date = obj.auction_end_date;
            item.auction_created_date = DateTime.Now.ToString("yyyy-MM-dd");
            db.Auctions.Add(item);
            db.SaveChanges();

            var artwork = db.Artworks.Find(obj.artwork_id);
            artwork.artwork_status = 2;
            db.SaveChanges();

            return "Add auction successfully";
        }
    }
}