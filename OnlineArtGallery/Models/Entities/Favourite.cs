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
    
    public partial class Favourite
    {
        public int favourite_id { get; set; }
        public Nullable<int> user_id { get; set; }
        public Nullable<int> artwork_id { get; set; }
    
        public virtual Artwork Artwork { get; set; }
        public virtual User User { get; set; }
    }
}