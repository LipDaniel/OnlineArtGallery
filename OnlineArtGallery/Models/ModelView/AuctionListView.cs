using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineArtGallery.Models.ModelView
{
    public class AuctionListView
    {
        public int auction_id { get; set; }
        public string artist_name { get; set; }
        public int artist_id { get; set; }
        public int artwork_id { get; set; }
        public string artwork_image { get; set; }
        public string artwork_name { get; set; }
        public string reserve_price { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string current_bid { get; set; }
        public string your_bid { get; set; }
    }
}