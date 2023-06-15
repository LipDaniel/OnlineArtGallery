using OnlineArtGallery.Models.Entities;
using OnlineArtGallery.Models.ModelView;
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
            return View();
        }
        public ActionResult UserList()
        {
            return View();
        }
        public ActionResult ArtistList()
        {
            ViewBag.Artists = db.Artists.OrderByDescending(a => a.artist_id).ToList();
            return View();
        }
        public ActionResult ArtworkRequest()
        {
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
            }).ToList();
            return View();
        }
        public ActionResult AuctionList()
        {
            ViewBag.Artwork = db.Artworks.Where(a => a.artwork_status == 0).ToList();
            ViewBag.Auction = db.Auctions.ToList();

            return View();
        }
        public ActionResult ProfileAdmin()
        {
            ViewBag.ProfileAdmin = db.Users.ToList();
            return View();
        }
        public ActionResult GalleryList()
        {
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
        public ActionResult OtherList()
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
            ViewBag.Exhibition = db.Exhibitions.ToList();

            return View();
        }
    }
}