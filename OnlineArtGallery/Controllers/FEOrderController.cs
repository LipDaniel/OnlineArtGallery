using OnlineArtGallery.Models.Entities;
using OnlineArtGallery.Models.ModelView;
using ShoppingOnline.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace OnlineArtGallery.Controllers
{
    public class FEOrderController : Controller
    {
        private GalleryArtEntities db = new GalleryArtEntities();
        public ActionResult Cart()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Index", "FEHome");
            var id = int.Parse(Session["UserId"].ToString());
            var cartlist = (from cart in db.Carts
                            join artwork in db.Artworks on cart.artwork_id equals artwork.artwork_id
                            where cart.user_id == id
                            orderby cart.cart_id descending
                            select new
                            {
                                Cart = cart,
                                Artwork = artwork
                            }).ToList();
            List<CartView> cartList= new List<CartView>();
            int amount = 0;
            foreach(var item in cartlist)
            {
                
                var cart = new CartView()
                {
                    ArtworkId = item.Artwork.artwork_id,
                    ArtworkName = item.Artwork.artwork_name,
                    ArtworkPrice = item.Artwork.artwork_price,
                    ArtworkStatus = (int)item.Artwork.artwork_status,
                    ArtworkImage = item.Artwork.artwork_image,
                    CartId = item.Cart.cart_id
                };
                if (item.Artwork.artwork_status == 2)
                {
                    var auction = db.Auctions.Where(a => a.artwork_id == item.Artwork.artwork_id).FirstOrDefault();
                    var bid = auction.auction_current_bid;
                    cart.ArtworkPrice = bid;
                }
                amount += int.Parse(cart.ArtworkPrice);
                cartList.Add(cart);
            }
            ViewBag.CartList = cartList;
            ViewBag.Amount = amount;
            return View();
        }

        public ActionResult Create(int id)
        {
            if (Session["UserId"] == null)
            {
                var resul = new
                {
                    noti = "Please login first!",
                };
                return Json(resul);
            }
            var userId = int.Parse(Session["UserId"].ToString());
            
            bool check = db.Carts.Any(a => a.artwork_id == id && a.user_id == userId);
            if (!check)
            {
                var artwork = db.Artworks.Where(a => a.artwork_id == id).FirstOrDefault();
                if (artwork.artwork_status == 6)
                {
                    var auction = db.Auctions.Where(a => a.artwork_id == id).FirstOrDefault();
                    if (auction.user_id == userId)
                    {
                        var cart = new Cart()
                        {
                            user_id = int.Parse(Session["UserId"].ToString()),
                            artwork_id = id
                        };

                        db.Carts.Add(cart);
                        db.SaveChanges();
                        var count = db.Carts.Where(b => b.user_id == userId).Count();
                        var resultt = new
                        {
                            noti = "Added !",
                            count
                        };
                        return Json(resultt);
                    } else
                    {
                        var resul = new
                        {
                            noti = "You can't add auction into cart"
                        };
                        return Json(resul);
                    }
                } else
                {

                    var cart = new Cart()
                    {
                        user_id = int.Parse(Session["UserId"].ToString()),
                        artwork_id = id
                    };

                    db.Carts.Add(cart);
                    db.SaveChanges();
                    var count = db.Carts.Where(b => b.user_id == userId).Count();
                    var resultt = new
                    {
                        noti = "Added !",
                        count
                    };
                    return Json(resultt);
                }
               
            }
            var result = new
            {
                noti = "Already added !",
            };
            return Json(result);
        }
        public ActionResult Delete(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Index", "FEHome");
            var cart = db.Carts.Find(id);
            db.Carts.Remove(cart);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Checkout()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Index", "FEHome");
            string id = Session["UserId"].ToString();
            var userId = int.Parse(id);
            var cartList = db.Carts.Where(a => a.user_id == userId).ToList();
            var order = (from cart in db.Carts
                         join artwork in db.Artworks on cart.artwork_id equals artwork.artwork_id
                         where cart.user_id == userId
                         orderby cart.cart_id descending
                         select new
                         {
                             Cart = cart,
                             Artwork = artwork
                         }).ToList();
            CartView cartView;
            List<CartView> lstCart = new List<CartView>();
            foreach (var item in order)
            {
                cartView = new CartView
                {
                    ArtworkId = item.Artwork.artwork_id,
                    ArtworkName = item.Artwork.artwork_name,
                    ArtworkPrice = item.Artwork.artwork_price
                };
                
                lstCart.Add(cartView);
            }
            if (cartList != null)
            {
                var auth = db.Users.Find(userId);
                ViewBag.User = auth;
                ViewData["lstCart"] = lstCart;
                return View();

            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult PayNow(Order obj, int artwork_id, string price)
        {
            string id = Session["UserId"].ToString();
            var userId = int.Parse(id);

            var orders = new Order();
            orders.user_id = userId;
            orders.order_total = obj.order_total;
            orders.order_created_date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            orders.order_phone = obj.order_phone;
            orders.order_address = obj.order_address;
            orders.order_email = obj.order_email;
            orders.order_fname = obj.order_fname;
            orders.order_lname = obj.order_lname;
            db.Orders.Add(orders);
            db.SaveChanges();

            var items = new Order_Item();
            items.order_id = orders.order_id;
            items.artwork_id = artwork_id;
            items.price = price;
            db.Order_Item.Add(items);
            db.SaveChanges();

            var update = db.Artworks.Find(artwork_id);
            update.artwork_status = 3;
            db.SaveChanges();

            bool check = db.Notifications.Any(a => a.artwork_id == artwork_id);
            if (check)
            {
                var request = db.Notifications.Where(a=> a.artwork_id == artwork_id && a.notification_recipient_id != null).FirstOrDefault();
                var art = db.Artworks.Find(artwork_id);
                var noti = new Notification()
                {
                    artwork_id = artwork_id,
                    notification_sender_id = request.notification_recipient_id,
                    notification_title = "Your artwork was sold",
                    notification_message = art.artwork_name + "was sold!",
                    notification_created_date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    notification_is_read = false,
                };
                db.Notifications.Add(noti);
                db.SaveChanges();
                
            }
            string payment = (int.Parse(obj.order_total) * 100).ToString();
            return JavaScript("window.location = '" + Url.Action("Payment", "FEOrder", new { payment = payment }) + "'");

        }

        [HttpPost]
        public ActionResult CreateBill(Order obj)
        {
            string id = Session["UserId"].ToString();
            var userId = int.Parse(id);
            var cartList = db.Carts.Where(a => a.user_id == userId).ToList();

            var orders = new Order();
            orders.user_id = userId;
            orders.order_total = obj.order_total;
            orders.order_created_date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            orders.order_phone = obj.order_phone;
            orders.order_address = obj.order_address;
            orders.order_email = obj.order_email;
            orders.order_fname = obj.order_fname;
            orders.order_lname = obj.order_lname;
            db.Orders.Add(orders);
            db.SaveChanges();

            var data = db.Carts.Where(a => a.user_id == userId).ToList();

            foreach (var item in data)
            {
                var artwork = db.Artworks.Where(b => b.artwork_id == item.artwork_id).FirstOrDefault();

                var items = new Order_Item();
                items.order_id = orders.order_id;
                items.artwork_id = item.artwork_id;
                items.price = artwork.artwork_price;
                db.Order_Item.Add(items);
                db.SaveChanges();

                var update = db.Artworks.Find(item.artwork_id);
                update.artwork_status = 3;
                db.SaveChanges();

                Cart book = db.Carts.Find(item.cart_id);
                db.Carts.Remove(book);
                db.SaveChanges();

                bool check = db.Notifications.Any(a => a.artwork_id == artwork.artwork_id);
                if (check)
                {
                    var request = db.Notifications.Where(a => a.artwork_id == artwork.artwork_id && a.notification_recipient_id != null).FirstOrDefault();
                    var noti = new Notification()
                    {
                        artwork_id = artwork.artwork_id,
                        notification_sender_id = request.notification_recipient_id,
                        notification_title = "Your artwork was sold",
                        notification_message = artwork.artwork_name + "was sold!",
                        notification_created_date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                        notification_is_read = false,
                    };
                    db.Notifications.Add(noti);
                    db.SaveChanges();

                }
            }

            string payment = (int.Parse(obj.order_total) * 100).ToString();
            return JavaScript("window.location = '" + Url.Action("Payment", "FEOrder", new { payment = payment }) + "'");
          
        }

        public ActionResult Payment(string payment)
        {
            string url = ConfigurationManager.AppSettings["Url"];
            string returnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
            string tmnCode = ConfigurationManager.AppSettings["TmnCode"];
            string hashSecret = ConfigurationManager.AppSettings["HashSecret"];

            PayLib pay = new PayLib();
            pay.AddRequestData("vnp_Version", "2.1.0");
            pay.AddRequestData("vnp_Command", "pay");
            pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_Amount", (int.Parse(payment) * 23000).ToString()); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", Util.GetIpAddress()); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang"); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);
            return Redirect(paymentUrl);

        }
    }
}