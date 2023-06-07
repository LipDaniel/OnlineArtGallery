using OnlineArtGallery.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineArtGallery.Controllers
{
    public class BEArtController : Controller
    {

        private GalleryArtEntities db = new GalleryArtEntities();


        // GET: Art
        // GET: Art/Create
        public ActionResult Create()
        {
            ViewBag.artist_id = new SelectList(db.Artists, "artist_id", "artist_name", "artist_country");
            ViewBag.category = new SelectList(db.Categories, "category_id", "category_name");

            return View();
        }

        // POST: Artworks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult NewArt(Artist art, HttpPostedFileBase artist_image)
        {
            var artis = new Artist();
            artis.artist_name = art.artist_name;
            artis.artist_country = art.artist_country;
            artis.artist_bio = art.artist_bio;
            artis.artist_created_date = DateTime.Now.ToString("yyyy/MM/dd");
            artis.artist_is_status = true;
            if (artist_image != null)
            {
                var fileName = Path.GetFileName(artist_image.FileName);
                string fileExtension = Path.GetExtension(artist_image.FileName);
                string file = fileName + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                var path = Path.Combine(Server.MapPath("~/Content/Assets/Images/artistimg/"), file);



                artist_image.SaveAs(path);
                artis.artist_image = file;
            }

            db.Artists.Add(artis);
            db.SaveChanges();
            return RedirectToAction("ArtistList", "BEIndex");

        }




    //post edit 

    [HttpPost]
    public ActionResult NewEdit(int id, Artist artist)
    {
        var arr = db.Artists.Find(id);
        if (arr == null)
        {
            return RedirectToAction("ArtistList", "BEIndex");
        }
        return RedirectToAction("ArtistList", "BEIndex");

        arr.artist_name = artist.artist_name;
        arr.artist_country = artist.artist_country;
        arr.artist_country = artist.artist_bio;

    }
}
}