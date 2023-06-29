using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineArtGallery.Models.ModelView
{
    public class GalleryView
    {
        public int GalleryId { get; set; }
        public string GalleryName { get; set; }
        public string GalleryImage { get; set; }
        public string GalleryDescription { get; set;}
        public int Products { get; set; }
    }
}