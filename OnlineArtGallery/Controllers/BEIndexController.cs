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
        public ActionResult Artist()
        {
            return View();
        }
 

    }
}