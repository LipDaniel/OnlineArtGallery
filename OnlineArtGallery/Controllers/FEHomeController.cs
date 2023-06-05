using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineArtGallery.Controllers
{
    public class FEHomeController : Controller
    {
       
        public ActionResult Index(){
            return View();
        }

        public ActionResult About(){
            return View();
        }

        public ActionResult Contact() { 
            return View();
        }

   
    }
}