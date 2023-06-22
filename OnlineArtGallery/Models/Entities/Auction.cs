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

    public partial class Auction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Auction()
        {
            this.Auction_User = new HashSet<Auction_User>();
        }
    
        public int auction_id { get; set; }
        public Nullable<int> artwork_id { get; set; }
        public string auction_reserve_price { get; set; }
        public string auction_start_date { get; set; }
        public string auction_end_date { get; set; }
        public string auction_current_bid { get; set; }
        public Nullable<int> user_id { get; set; }
        public string auction_created_date { get; set; }
    
        public virtual Artwork Artwork { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Auction_User> Auction_User { get; set; }
    }
}
