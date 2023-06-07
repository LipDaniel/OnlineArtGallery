using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineArtGallery.Controllers
{
    public class BEIndexController : Controller
    {
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
            return View();
        }
        public ActionResult ArtworkRequest()
        {
            return View();
        }
        public ActionResult ArtworkList()
        {
            return View();
        }
        public ActionResult GalleryList()
        {   
            return View();
        }
        public ActionResult CategoryList()
        {
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
    }
}