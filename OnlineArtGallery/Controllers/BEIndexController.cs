using OnlineArtGallery.Models.Entities;
using OnlineArtGallery.Models.ModelView;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web.Mvc;

namespace OnlineArtGallery.Controllers
{
    public class BEIndexController : Controller
    {
        private GalleryArtEntities db = new GalleryArtEntities();

        // GET: BEIndex
        public ActionResult Index()
        {
            ViewBag.OrderList = db.Orders.OrderByDescending(a => a.order_id).ToList();
            ViewBag.countBill = db.Orders.Count();
            ViewBag.countCustomer = db.Users.Where(e => e.user_level == 2).Count();
            ViewBag.countOnAuction = db.Auctions.Where(e => e.user_id == null).Count();
            var totalEarnings = db.Orders.ToList();
            var total = 0;
            foreach (var item in totalEarnings)
            {
                total += int.Parse(item.order_total);
            }
            ViewBag.totalEarnings = total;
            return View();
        }
        public ActionResult UserList(User serlist)
        {
            var activeUsers = db.Users.Where(p =>p.user_level==2).ToList();

           

            ViewBag.UserList = activeUsers;

            return View();
        }
        public ActionResult ArtistList()
        {
            ViewBag.Artists = db.Artists.OrderByDescending(a => a.artist_id).ToList();
            return View();
        }
        public ActionResult ArtworkList()
        {
            ViewBag.Artists = db.Artists.OrderByDescending(a => a.artist_id).ToList();
            ViewBag.Gallery = db.Galleries.OrderByDescending(a => a.gallery_id).ToList();
            ViewBag.Category = db.Categories.OrderByDescending(a => a.category_id).ToList();
            ViewBag.Artwork = db.Artworks.Select(a => new ArtworkView
            {
                id = a.artwork_id,
                artist_id = a.artist_id,
                artist = a.Artist.artist_name,
                name = a.artwork_name,
                image = a.artwork_image,
                description = a.artwork_description,
                price = a.artwork_price,
                dimensions = a.artwork_dimensions,
                status = a.artwork_status,
                date = a.artwork_date,
                category_id = a.category_id,
                category = a.Category.category_name
            }).OrderByDescending(e => e.id).ToList();
            return View();
        }
        public ActionResult AuctionList()
        {
            ViewBag.Artwork = db.Artworks.Where(a => a.artwork_status == 0).OrderByDescending(e => e.artwork_id).ToList();
            ViewBag.Auction = db.Auctions.OrderByDescending(e => e.auction_id).ToList();
            return View();
        }
        public ActionResult ProfileAdmin()
        {
            ViewBag.ProfileAdmin = db.Users.ToList();
            return View();
        }
        public ActionResult GalleryList()
        {
            var list = db.Artworks.Where(a => a.artwork_status == 1 || a.artwork_status == 0).ToList();
           
            List<Artwork> artworkList = new List<Artwork>();
            foreach( var item in list)
            {
                var check = db.Artwork_Gallery.Any(a => a.artwork_id == item.artwork_id);
                if (!check)
                {
                    artworkList.Add(item);
                }
            }
            ViewBag.ArtworkList = artworkList;
            ViewBag.GalleryList = db.Galleries.OrderByDescending(a => a.gallery_id).ToList();
            
            return View();
        }
        public ActionResult CategoryList()
        {
            ViewBag.Category = db.Categories.OrderByDescending(a => a.category_id).ToList();
            return View();
        }
        public ActionResult Favourite()
        {
            return View();
        }
        public ActionResult Rating()
        {
            return View();
        }
        public ActionResult Infor()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contactus()
        {
            ViewBag.Contact = db.Contacts.OrderByDescending(a => a.contact_id).ToList();
            return View();
        }
        public ActionResult TagList()
        {
            ViewBag.Tag = db.Tags.OrderByDescending(a => a.tag_id).ToList();

            return View();
        }
        public ActionResult ExhibitionList()
        {
            ViewBag.Exhibition = db.Exhibitions.OrderByDescending(a => a.exhibition_id).ToList();
            ViewBag.Gallery = db.Galleries.Where(a => a.gellery_is_active == true).OrderByDescending(b => b.gallery_id).ToList();
            
            return View();
        }

        [HttpGet]
        public string Search()
        {
            var artwork = db.Artworks.OrderByDescending(e => e.artwork_id).ToList();
                
            string output = "";

            foreach (var item in artwork) {
                string href = "/FEArtwork/ArtworkDetail/"+ item.artwork_id;
                output += 
                    "<li class='dropdown-item p-0'>"
                        +"<a class='text-dark d-flex align-items-center py-2' style='cursor: pointer' href='" + href + "'>"
                            +"<div class='flex-shrink-0 mr-2 ml-2'>"
                                + "<img class='rounded-3' style='width: 50px; height:50px' src='/Content/assets/images/artwork/"+item.artwork_image+"'>"
                            +"</div>"
                            +"<div class='flex-grow-1 pr-2'>"
                                +"<div class='font-weight-bold text-uppercase'>" + item.artwork_name +"</div>"
                                +"<span class='fw-medium text-muted'>"+ item.Artist.artist_name +"</span>"
                            +"</div>"
                        +"</a>"
                    +"</li>";
            }

            return output;
        }
    }
}