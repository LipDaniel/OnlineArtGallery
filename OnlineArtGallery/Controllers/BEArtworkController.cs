using OnlineArtGallery.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineArtGallery.Controllers
{
    public class BEArtworkController : Controller
    {
        private GalleryArtEntities db = new GalleryArtEntities();

        // GET: BEArtwork
        public string Create(Artwork obj, HttpPostedFileBase artwork_image) 
        {
            var item = new Artwork();
            item.artwork_name = obj.artwork_name;
            item.artwork_price = obj.artwork_price;
            item.artwork_description = obj.artwork_description;
            item.artwork_date = obj.artwork_date;
            item.artwork_dimensions = obj.artwork_dimensions;
            item.category_id = obj.category_id;
            item.artist_id = obj.artist_id;
            item.artwork_status = 0;

            if (artwork_image != null)
            {
                string fileExtension = Path.GetExtension(artwork_image.FileName);
                string file = DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                var path = Path.Combine(Server.MapPath("~/Content/Assets/Images/artwork/"), file);


                artwork_image.SaveAs(path);
                item.artwork_image = file;
            }
            db.Artworks.Add(item);
            db.SaveChanges();
            return "Add artwork successfully";
        }

        public string Edit(Artwork obj, HttpPostedFileBase artwork_image)
        {
            var item = db.Artworks.Find(obj.artwork_id);
            item.artwork_name = obj.artwork_name;
            item.artwork_price = obj.artwork_price;
            item.artwork_description = obj.artwork_description;
            item.artwork_date = obj.artwork_date;
            item.artwork_dimensions = obj.artwork_dimensions;
            item.category_id = obj.category_id;
            item.artist_id = obj.artist_id;
            item.artwork_status = obj.artwork_status;

            if (artwork_image != null)
            {
                string fileExtension = Path.GetExtension(artwork_image.FileName);
                string file = DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                var path = Path.Combine(Server.MapPath("~/Content/Assets/Images/artwork/"), file);


                artwork_image.SaveAs(path);
                item.artwork_image = file;
            }
            db.SaveChanges();
            return "Update artwork successfully";
        }

        public string Update(Artwork obj)
        {
            var item = db.Artworks.Find(obj.artwork_id);
            item.artwork_status = obj.artwork_status;
            
            db.SaveChanges();

            return "Change status successfully";
        }

        public string Approve(Artwork obj)
        {
            var item = db.Artworks.Find(obj.artwork_id);
            item.artwork_status = obj.artwork_status;
            var noti = db.Notifications.Where(a => a.artwork_id ==  obj.artwork_id && a.notification_recipient_id != null).FirstOrDefault();
            var user = db.Users.Find(noti.notification_recipient_id);
            var announce = new Notification()
            {
                notification_sender_id = user.user_id,
                notification_title = "Your artwork was approved!",
                notification_is_read = false,
                notification_message = item.artwork_name + " is on sale.",
                notificaiton_click_url = "/FEArtwork/ArtworkDetail/" + item.artwork_id,
                notification_created_date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                artwork_id = item.artwork_id
            };
            db.Notifications.Add(announce);
            db.SaveChanges();

            return "Confirm successfully";
        }
    }
}