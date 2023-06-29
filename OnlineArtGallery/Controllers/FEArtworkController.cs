using Microsoft.Ajax.Utilities;
using OnlineArtGallery.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using OnlineArtGallery.Models.ModelView;
namespace OnlineArtGallery.Controllers
{
    public class FEArtworkController : Controller
    {
        private GalleryArtEntities db = new GalleryArtEntities();
        private const int PageSize = 10;

        public ActionResult GalleryDetail(int id)
        {
            var gallery = db.Galleries.Find(id);
            var artwork = db.Artwork_Gallery.Where(a => a.gallery_id == id).ToList();
            
            var exhi = db.Exhibition_Gallery.Where(b => b.gallery_id == id).FirstOrDefault();
            ViewBag.Gallery = gallery;
            ViewBag.Artwork = artwork;
            ViewBag.Exhibition = exhi;
            return View();
        }
        public ActionResult Gallery()
        {
            List<GalleryView> list = new List<GalleryView>();
            var gallery = db.Galleries.OrderByDescending(a => a.gallery_id).ToList();
            foreach (var item in gallery)
            {
                var product = db.Artwork_Gallery.Where(b => b.gallery_id == item.gallery_id).Count();
                var gal = new GalleryView()
                {
                    GalleryDescription = item.gallery_description,
                    GalleryId = item.gallery_id,
                    GalleryImage = item.gallery_image,
                    GalleryName = item.gallery_name,
                    Products = product,
                };
                list.Add(gal);
            }
            
            ViewBag.Gallery = list;
            return View();
        }
        public ActionResult ArtworkDetail(int id)
        {
            Artwork artwork = db.Artworks.Find(id);
            if (artwork == null)
            {
                return RedirectToAction("Index", "FEHome");
            }
            Artist artist = db.Artists.Find(artwork.artist_id);
            Category category = db.Categories.Find(artwork.category_id);
            Auction auction = null;
            if(artwork.artwork_status == 2)
            {
                auction = db.Auctions.Where(a => a.artwork_id == id).FirstOrDefault();
            }
            var revCount = db.Ratings.Where(a=> a.artwork_id == id).Count();
            var artworkList = db.Artworks.Where(b => b.category_id == artwork.category_id).Take(5).ToList();
            var rev = db.Ratings.Where(a=>a.artwork_id == id).ToList();
            var star = db.Ratings.Where(a => a.artwork_id == id).ToList();
            double percent = 0;
            if(star.Count() != 0)
            {
                percent = (star.Average(a => a.rating_star)) / 5 * 100;
            }
            if(Session["UserId"] != null)
            {
                var userId = int.Parse(Session["UserId"].ToString());
                var auth = db.Users.Find(userId);
                ViewBag.User = auth;

            }
            ViewBag.Review = rev;
            ViewBag.Artwork = artwork;
            ViewBag.Artist = artist;
            ViewBag.Category = category;  
            ViewBag.ArtworkList = artworkList;
            ViewBag.RevCount = revCount;
            ViewBag.Percent = percent;
            ViewBag.Auction = auction;

            return View();
        }

        public ActionResult Rating(Rating rate, int artwork_id)
        {
            if (Session["UserId"] != null)
            {
                var userId = int.Parse(Session["UserId"].ToString());
                var rating = new Rating()
                {
                    user_id = userId,
                    artwork_id = artwork_id,
                    rating_star = rate.rating_star,
                    rating_comment = rate.rating_comment,
                    rating_title = rate.rating_title,
                };
                db.Ratings.Add(rating);
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            };
            return Redirect(Request.UrlReferrer.ToString());
        }
        public int RatingCount(int id)
        {
            GalleryArtEntities db = new GalleryArtEntities();
            var count = db.Ratings.Where(a => a.artwork_id == id).ToList().Count();
            return count;
        }
        public double Star(int id)
        {
            GalleryArtEntities db = new GalleryArtEntities();
            var star = db.Ratings.Where(a => a.artwork_id == id).ToList();
            double percent = 0;
            if (star.Count() != 0)
            {
                percent = (star.Average(a => a.rating_star)) / 5 * 100;
                return percent;
            }

            return percent;
        }
        public ActionResult ArtworkList(int page = 1)
        {
            var artwork = db.Artworks.Where(a => a.artwork_status == 1 || a.artwork_status == 2 || a.artwork_status == 3).ToList();
            var paginatedData = artwork.Skip((page - 1) * PageSize).Take(PageSize).ToList();
            var totalItem = artwork.Count();
            var totalPages = (int)Math.Ceiling(totalItem / (double)PageSize);
            var category = db.Categories.ToList();
            var tag = db.Tags.ToList();
            var pageSize = paginatedData.Count() + (page - 1) * 5;
            var cateCount = db.Categories.Count();
            var artist = db.Artists.Where(a => a.artist_is_status == true).ToList();
            ViewBag.Category = category;
            ViewBag.Artwork = paginatedData;
            ViewBag.Tag = tag;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalItem = totalItem;
            ViewBag.PageSize = pageSize;
            ViewBag.Artist = artist;
            return View();
        }

        public ActionResult Category(int id, int page = 1)
        {
            var artwork = db.Artworks.Where(a => a.category_id == id &&( a.artwork_status == 1 || a.artwork_status == 2 || a.artwork_status == 3)).ToList();
            var paginatedData = artwork.Skip((page - 1)*PageSize).Take(PageSize).ToList();
            var totalItem = artwork.Count();
            var totalPages = (int)Math.Ceiling(totalItem / (double)PageSize);
            var tag = db.Tags.ToList();
            var cate = db.Categories.Find(id);
            var header = cate.category_name;
            var pageSize = paginatedData.Count() + (page - 1) * 5;
            var artist = db.Artists.Where(a => a.artist_is_status == true).ToList();
            ViewBag.Artwork = paginatedData;
            ViewBag.Tag = tag;
            ViewBag.Header = header;
            ViewBag.Id = id;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItem = totalItem;
            ViewBag.Artist = artist;
            return View();
        }

        public ActionResult Artist(int id, int page = 1)
        {
            var artwork = db.Artworks.Where(a => a.artist_id == id && (a.artwork_status == 1 || a.artwork_status == 2 || a.artwork_status == 3)).ToList();
            var paginatedData = artwork.Skip((page - 1) * PageSize).Take(PageSize).ToList();
            var totalItem = artwork.Count();
            var totalPages = (int)Math.Ceiling(totalItem / (double)PageSize);
            var category = db.Categories.Where(a => a.category_is_status == true).ToList();
            var tag = db.Tags.ToList();
            var artist = db.Artists.Find(id);
            var pageSize = paginatedData.Count() + +(page - 1) * 5;
            ViewBag.Category = category;
            ViewBag.Artwork = paginatedData;
            ViewBag.Tag = tag;
            ViewBag.Artist = artist;
            ViewBag.TotalPages = totalPages;
            ViewBag.Id = id;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItem = totalItem;
          
            return View();
        }

        public ActionResult GetCategory()
        {
            var category = db.Categories.ToList();
            ViewBag.Category = category;
            return View();
        }

        
    }
}