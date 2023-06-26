using OnlineArtGallery.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineArtGallery.Controllers
{
    public class BEDashboardController : Controller
    {
        private GalleryArtEntities db = new GalleryArtEntities();
        // GET: BEDashboard
        public List<Order> GetRevenueEachYear()
        {
            var orders = db.Orders.SqlQuery("Select month(convert(datetime, order_created_date, 120)) as month, SUM(CAST(order_total as int)) as revenues from orders where year(convert(datetime, order_created_date, 120)) = 2022 group by month(convert(datetime, order_created_date, 120))").ToList();

            return orders;
        }

        public DateTime ConverDate(string date)
        {
            var myDate = DateTime.ParseExact(date, "yyyy-MM-dd HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);
            return myDate;
        }
    }
}