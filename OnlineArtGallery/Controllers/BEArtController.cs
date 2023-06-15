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
            Session["Message"] = "Seccessful";
            return RedirectToAction("ArtistList", "BEIndex");

        }


        //get edit 
        public ActionResult NewEdit(Artist artist, HttpPostedFileBase artist_image)
        {
            var art = db.Artists.Find(artist.artist_id);
            if (art == null)
            {
                return RedirectToAction("ArtistList", "BEIndex");
            }
            art.artist_name = artist.artist_name;
            art.artist_country = artist.artist_country;
            art.artist_bio = artist.artist_bio;
            art.artist_created_date = DateTime.Now.ToString("yyyy/MM/dd");
            art.artist_is_status = true;
            if (artist_image != null)
            {
                if (art.artist_image != null)
                {
                    string gallpath = Path.Combine(Server.MapPath("~/Content/Assets/Images/User/"), art.artist_image);
                    if (System.IO.File.Exists(gallpath))
                    {
                        // Delete the file
                        System.IO.File.Delete(gallpath);

                        // Perform any other necessary actions
                    }
                }
                var fileName = Path.GetFileName(artist_image.FileName);
                string fileExtension = Path.GetExtension(artist_image.FileName);
                string file = fileName + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                var path = Path.Combine(Server.MapPath("~/Content/Assets/Images/artistimg/"), file);


                
                artist_image.SaveAs(path);
                art.artist_image = file;
            }
            db.SaveChanges();
            Session["Message"] = "Seccessful";
            return RedirectToAction("ArtistList", "BEIndex");




        }



    }
}