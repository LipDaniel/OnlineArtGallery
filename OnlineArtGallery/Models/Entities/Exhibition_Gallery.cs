//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineArtGallery.Models.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Exhibition_Gallery
    {
        public int exhibition_gallery_id { get; set; }
        public Nullable<int> gallery_id { get; set; }
        public Nullable<int> exhibition_id { get; set; }
        public string exhibition_gallery_start_date { get; set; }
        public string exhibition_gallery_end_date { get; set; }
        public string exhibition_gallery_created_date { get; set; }
        public Nullable<bool> exhibition_gallery_is_active { get; set; }
    
        public virtual Exhibition Exhibition { get; set; }
        public virtual Gallery Gallery { get; set; }
    }
}
