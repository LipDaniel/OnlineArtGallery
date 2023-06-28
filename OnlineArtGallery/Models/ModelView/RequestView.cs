using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineArtGallery.Models.ModelView
{
    public class RequestView
    {
        public int UserId { get; set; }
        public int ArtworkId { get; set; }  
        public string ArtworkImage { get; set; }
        public int ArtworkStatus { get; set; }
        public string ArtworkName { get; set;}
        public string ArtworkPrice { get; set; }
        public string CategoryName { get; set;}
        public string ArtistName { get; set;}
        public string Date { get; set; }
        public string Dimensions { get; set; }
        public string Description { get; set; }
        public string CreatedDate { get; set; }
    }
}