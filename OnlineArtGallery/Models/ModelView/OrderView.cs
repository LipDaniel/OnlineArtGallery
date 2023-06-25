using OnlineArtGallery.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineArtGallery.Models.ModelView
{
    public class OrderView
    {
        public int OrderId { get; set; }
        public string CreatedDate { get; set; }
        public string OrderPrice { get; set;}
        public string Phone { get; set;}
        public string Address { get; set;}
        public string Email { get; set;}
        public string Lname { get; set;}
        public string Fname { get; set;}
   
        public List<OrderItemView> OrderItem { get; set; }
    }
}