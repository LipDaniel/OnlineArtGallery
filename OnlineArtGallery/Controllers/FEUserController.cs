using OnlineArtGallery.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using OnlineArtGallery.Models.ModelView;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Policy;

namespace OnlineArtGallery.Controllers
{
    public class FEUserController : Controller
    {
        private GalleryArtEntities db = new GalleryArtEntities();
        public ActionResult UserProfile()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "FEHome");
            }
            var art = db.Artists.ToList();
            var cate = db.Categories.ToList();
            var auth = db.Users.Find(Session["UserId"]);
            if (auth == null)
            {
                return RedirectToAction("Index", "FEHome");
            }
            var orderList = db.Orders.Where(a=> a.user_id == auth.user_id).OrderByDescending(a => a.order_id).ToList();
            List<OrderView> orders = new List<OrderView>(); 
            foreach(var item in orderList)
            {
                var order = new OrderView()
                {
                    OrderId = item.order_id,
                    OrderPrice = item.order_total,
                    CreatedDate = item.order_created_date,
                    Phone = item.order_phone,
                    Address = item.order_address,
                    Email = item.order_email,
                    Lname = item.order_lname,
                    Fname = item.order_fname,
                };
                List<OrderItemView> orderItem = new List<OrderItemView>();
                var orderItemList = db.Order_Item.Where(a => a.order_id == order.OrderId).ToList();
                foreach(var i in orderItemList)
                {
                    var itemOrder = new OrderItemView()
                    {
                        Name = i.Artwork.artwork_name,
                        Price = i.Artwork.artwork_price,
                        Image = i.Artwork.artwork_image
                    };
                    orderItem.Add(itemOrder);
                }
                order.OrderItem = orderItem;
                orders.Add(order);
            }

            var auctionList = (from auction in db.Auctions
                    join artwork in db.Artworks on auction.artwork_id equals artwork.artwork_id
                    join auction_user in db.Auction_User on auction.auction_id equals auction_user.auction_id
                    where auction_user.user_id == auth.user_id
                    orderby auction_user.auction_user_id descending
                    select new
                    {
                        AuctionUser = auction_user,
                        Auction = auction,
                        Artwork = artwork
                    }).ToList();
            var wonAuction = db.Auctions.Where(a => a.user_id == auth.user_id).ToList();
            List<Tuple<string, int>> payment = new List<Tuple<string, int>>();
            foreach (var item in wonAuction)
            {
                var check = db.Order_Item.Any(a => a.artwork_id == item.artwork_id);
                if (check)
                {
                    payment.Add(new Tuple<string, int>("Payment", int.Parse(item.artwork_id.ToString())));

                } else
                {
                    payment.Add(new Tuple<string, int>("Not payment yet", int.Parse(item.artwork_id.ToString())));
                }
            }

            List<AuctionListView> aucList = new List<AuctionListView>();
            foreach(var item in auctionList)
            {
                var auc = new AuctionListView()
                {
                    auction_id = item.Auction.auction_id,
                    artist_name = item.Artwork.Artist.artist_name,
                    artist_id = item.Artwork.Artist.artist_id,
                    artwork_id = item.Artwork.artwork_id,
                    artwork_image = item.Artwork.artwork_image,
                    artwork_name = item.Artwork.artwork_name,
                    reserve_price = item.Auction.auction_reserve_price,
                    start_date = item.Auction.auction_start_date,
                    end_date = item.Auction.auction_end_date,
                    your_bid = item.AuctionUser.auction_amount,
                    current_bid = item.Auction.auction_current_bid,
                    
                };
                aucList.Add(auc);
            }

            var requestList = (from noti in db.Notifications 
                               join artwork in db.Artworks on noti.artwork_id equals artwork.artwork_id
                               where noti.notification_recipient_id == auth.user_id && noti.artwork_id != null
                               orderby noti.notification_id descending
                               select new
                               {
                                   UserId = auth.user_id,
                                   ArtworkId = artwork.artwork_id,
                                   ArtworkImage = artwork.artwork_image,
                                   CategoryName = artwork.Category.category_name,
                                   ArtistName = artwork.Artist.artist_name,
                                   ArtworkStatus = artwork.artwork_status,
                                   ArtworkPrice = artwork.artwork_price,
                                   ArtworkName = artwork.artwork_name,
                                   Date = artwork.artwork_date,
                                   Dimensions = artwork.artwork_dimensions,
                                   Description = artwork.artwork_description,
                                   CreatedDate = noti.notification_created_date
                               }).ToList();
            var reqList = new List<RequestView>();
            int revenue = 0;
            foreach (var item in requestList)
            {
                var request = new RequestView()
                {
                    UserId = auth.user_id,
                    ArtworkId = item.ArtworkId,
                    ArtworkImage = item.ArtworkImage,
                    CategoryName = item.CategoryName,
                    ArtistName = item.ArtistName,
                    ArtworkStatus = int.Parse((item.ArtworkStatus).ToString()),
                    ArtworkPrice = item.ArtworkPrice,
                    ArtworkName = item.ArtworkName,
                    Date = item.Date,
                    Dimensions = item.Dimensions,
                    Description = item.Description,
                    CreatedDate = item.CreatedDate
                };
                reqList.Add(request);
                if (item.ArtworkStatus == 3)
                {
                    revenue += int.Parse(item.ArtworkPrice);
                }
            }
            ViewBag.Request = reqList;
            ViewBag.Order = orders;
            ViewBag.Auction = aucList;
            ViewBag.User = auth;
            ViewBag.Artists = art;
            ViewBag.Category = cate;
            ViewBag.WonAuction = wonAuction;
            ViewBag.Status = payment;
            ViewBag.OrderCount = orderList.Count();
            ViewBag.WonAuctionCount = wonAuction.Count();
            ViewBag.AuctionCount = auctionList.Count();
            ViewBag.RequestCount = requestList.Count();
            ViewBag.Revenue = revenue;
            return View();
        }

        public ActionResult UserEdit(User user, HttpPostedFileBase user_image)
        {
            var auth = db.Users.Find(Session["UserId"]);
            if (auth == null)
            {
                return RedirectToAction("Index", "FEHome");
            }

            auth.user_fname = user.user_fname;
            auth.user_lname = user.user_lname;
            auth.user_phone = user.user_phone;


            if (user_image != null && user_image.ContentLength > 0)
            {
                if (auth.user_image != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Content/Assets/Images/User/"), auth.user_image);
                    if (System.IO.File.Exists(path))
                    {
                        // Delete the file
                        System.IO.File.Delete(path);

                        // Perform any other necessary actions
                    }
                }
                string fileExtension = Path.GetExtension(user_image.FileName);
                string uniqueFileName = auth.user_id.ToString() + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                string filePath = Path.Combine(Server.MapPath("~/Content/Assets/Images/User/"), uniqueFileName);
                user_image.SaveAs(filePath);

                auth.user_image = uniqueFileName;
            }
            TempData["SuccessMessage"] = "Change profile successfully.";
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public string EditPassword(string new_password, string current_password, string reset_password)
        {
            var auth = db.Users.Find(Session["UserId"]);

            

            if (new_password != reset_password)
                return "Your passwords don't match!";

            // Kiểm tra khớp với mật khẩu hiện tại
            if (current_password == auth.user_password && new_password == reset_password)
            {
                // Kiểm tra yêu cầu về độ dài và ký tự
                if (IsPasswordValid(new_password))
                {
                    // Lưu mật khẩu mới vào cơ sở dữ liệu
                    auth.user_password = new_password;
                    db.SaveChanges();
                    return "Password changed successfully!";
                }
                else
                {
                    return "Your new password must be at least 8 characters!";
                }
            }
            else
            {
                return "Password changed Fail!";
            }
        }
        private bool IsPasswordValid(string password)
        {
            // Kiểm tra yêu cầu về độ dài và ký tự
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$!%*?&#^()])[A-Za-z\d$!%*?&#^()]{8,}$");
            return regex.IsMatch(password);
        }
        // Hàm băm mật khẩu

        public ActionResult RequestArtwork(Artwork model, string height, string width, HttpPostedFileBase artwork_image)
        {
            var artwork = new Artwork()
            {
                artist_id = model.artist_id,
                category_id = model.category_id,
                artwork_name = model.artwork_name,
                artwork_description = model.artwork_description,
                artwork_dimensions = width + " x " + height,
                artwork_price = model.artwork_price,
                artwork_status = 4,
                artwork_date = model.artwork_date
            };
            db.Artworks.Add(artwork);

            var auth = db.Users.Find(Session["UserId"]);

            if (artwork_image != null && artwork_image.ContentLength > 0)
            {
                string fileExtension = Path.GetExtension(artwork_image.FileName);
                string uniqueFileName = auth.user_id.ToString() + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                string filePath = Path.Combine(Server.MapPath("~/Content/Assets/Images/Artwork/"), uniqueFileName);
                artwork_image.SaveAs(filePath);
                artwork.artwork_image = uniqueFileName;
            }
            db.SaveChanges();
            var noti = new Notification
            {
                notification_recipient_id = int.Parse(auth.user_id.ToString()),
                notification_title = "New request for artwork!",
                notification_is_read = false,
                notification_message = auth.user_fname + "" + auth.user_lname + " request new artwork.",
                notificaiton_click_url = "/BERequestArtwork/" + artwork.artwork_id,
                notification_created_date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                artwork_id = artwork.artwork_id
            };
            db.Notifications.Add(noti);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Request artwork successfully.";
            return Redirect(Request.UrlReferrer.ToString());
        }
        [HttpPost]
        public ActionResult EditStatus(int user_id)
        {
           

            var userId = int.Parse(Session["UserId"].ToString());
            bool check = db.Users.Any(a => a.user_id == user_id);
            if (check)
            {
                // Lấy thông tin người dùng từ database
                var user = db.Users.Where(u => u.user_id == user_id ).FirstOrDefault();
                if (user.user_is_active == true)
                {
                    user.user_is_active = false;
                    db.SaveChanges();
                    return RedirectToAction("UserList", "BEIndex");
                }
                else
                {
                    user.user_is_active = true;
                    db.SaveChanges();
                    return RedirectToAction("UserList", "BEIndex");
                }
              
            }
            else
            {
                return RedirectToAction("UserList", "BEIndex");
            }
        }

    }

}

