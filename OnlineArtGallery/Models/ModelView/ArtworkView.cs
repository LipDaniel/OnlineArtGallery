using System;

namespace OnlineArtGallery.Models.ModelView
{
    public class ArtworkView
    {
        public int id { get; set; }
        public Nullable<int> artist_id { get; set; }
        public string artist { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        public string price { get; set; }
        public string dimensions { get; set; }
        public Nullable<int> status { get; set; }
        public string date { get; set; }
        public Nullable<int> category_id { get; set; }
        public string category { get; set; }
    }
}