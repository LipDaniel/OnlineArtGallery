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

namespace OnlineArtGallery.Controllers
{
    public class FEArtworkController : Controller
    {
        private GalleryArtEntities db = new GalleryArtEntities();
        private const int PageSize = 10;
        public ActionResult ArtworkDetail(int id)
        {
            Artwork artwork = db.Artworks.Find(id);
            Artist artist = db.Artists.Find(artwork.artist_id);
            Category category = db.Categories.Find(artwork.category_id);
            var revCount = db.Ratings.Where(a=> a.artwork_id == id).Count();
            if (artwork == null)
            {
                return RedirectToAction("Index","FEHome");
            }
            var artworkList = db.Artworks.Take(4).ToList();
            var rev = db.Ratings.Where(a=>a.artwork_id == id).ToList();
            ViewBag.Review = rev;
            ViewBag.Artwork = artwork;
            ViewBag.Artist = artist;
            ViewBag.Category = category;  
            ViewBag.ArtworkList = artworkList;
            ViewBag.RevCount = revCount;
            return View();
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