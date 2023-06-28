using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineArtGallery.Models.ModelView
{
    public class Wishlist
    {
        public int ArtworkId { get; set; }
        public string ArtworkImage { get; set; }
        public int FavoriteId { get; set; }
        public string ArtworkPrice { get; set; }
        public string ArtworkName { get; set; }
        public int ArtworkStatus { get; set; }
    }
}