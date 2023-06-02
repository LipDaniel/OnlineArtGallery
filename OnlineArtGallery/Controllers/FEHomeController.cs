using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineArtGallery.Controllers
{
    public class FEHomeController : Controller
    {
        // GET: FEHome
        public ActionResult Index()
        {
            return View();
        }
    }
}