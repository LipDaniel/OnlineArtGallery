using OnlineArtGallery.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace OnlineArtGallery.Controllers
{
    public class BEContactController : Controller
    {
        GalleryArtEntities db = new GalleryArtEntities();

        // GET: BEContact
      

        // POST: Artworks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult NewContact(Contact contact)
        {
            var cont = new Contact();
            cont.contact_name = contact.contact_name;
            cont.contact_phone = contact.contact_phone;
            cont.contact_email = contact.contact_email;
            cont.contact_subject = contact.contact_subject;
            cont.contact_message = contact.contact_message;
            

            if (cont != null)
            {
                Session["Message"] = "Message successfully.";

            }
            
            

            //cont.artist_created_date = DateTime.Now.ToString("yyyy/MM/dd");
            //cont.artist_is_status = true;

            db.Contacts.Add(cont);
            db.SaveChanges();
            return RedirectToAction("Contact","FEHome");

        }
    }
}