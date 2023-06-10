using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineArtGallery.Controllers
{
    public class BEArtworkController : Controller
    {
        // GET: BEArtwork
        public ActionResult Index()
        {
            return View();
        }
    }
}