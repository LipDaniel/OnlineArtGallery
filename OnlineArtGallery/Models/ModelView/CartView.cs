using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineArtGallery.Models.ModelView
{
    public class CartView
    {
        public int ArtworkId { get; set; }
        public string ArtworkName { get; set; }
        public string ArtworkPrice { get; set; }
    }
}