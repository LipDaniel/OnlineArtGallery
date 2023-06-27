using OnlineArtGallery.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineArtGallery.Controllers
{

    public class BEProfileController : Controller
    {
        private GalleryArtEntities db = new GalleryArtEntities();

        // Post: BEProfile
        [HttpPost]
        public ActionResult NewEdit(User Admin, HttpPostedFileBase user_image)
        {
            var admin = db.Users.Find(Session["UserId"]);
            if (admin == null)
            {
                return RedirectToAction("ProfileAdmin", "BEIndex");
            }

            admin.user_fname = Admin.user_fname;
            admin.user_lname = Admin.user_lname;
            admin.user_email = Admin.user_email;
            admin.user_phone = Admin.user_phone;





            if (user_image != null && user_image.ContentLength > 0)
            {
                if (admin.user_image != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Content/Assets/Images/User/"), admin.user_image);
                    if (System.IO.File.Exists(path))
                    {
                        // Delete the file
                        System.IO.File.Delete(path);

                        // Perform any other necessary actions
                    }
                }
                string fileExtension = Path.GetExtension(user_image.FileName);
                string uniqueFileName = admin.user_id.ToString() + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                string filePath = Path.Combine(Server.MapPath("~/Content/Assets/Images/user/"), uniqueFileName);
                user_image.SaveAs(filePath);

                admin.user_image = uniqueFileName;
            }
            //TempData["SuccessMessage"] = "Change profile successfully.";

            Session["Message"] = "Change profile successful";
            db.SaveChanges();
                return RedirectToAction("ProfileAdmin","BEIndex");
            


        }
        //Edit password 
        [HttpPost]
        public ActionResult CheckCurrentPassword(string currentPassword, string new_password, string reset_password)
        {
            // Lấy thông tin người dùng từ cơ sở dữ liệu
            var userId = (int)Session["UserId"];
            var user = db.Users.Find(userId);

            // Kiểm tra mật khẩu hiện tại
            var isValid = currentPassword == user.user_password;

            if (isValid)
            {
                var response = new { isValid = isValid };
                return Json(response);
            }



            // Kiểm tra xem mật khẩu mới và xác nhận mật khẩu có khớp nhau không
            var pattern = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$!%*?&#^()])[A-Za-z\d$!%*?&#^()]{8,}$");
            var isPasswordValid = pattern.IsMatch(new_password) && !new_password.Contains(" ");

            if (!isPasswordValid || new_password != reset_password)
            {
                Session["Message"] = "Change password Fail";
                return RedirectToAction("ProfileAdmin", "BEIndex");
            }

            user.user_password = new_password;
            db.SaveChanges();
            Session["Message"] = " change password sucessfull";
            return RedirectToAction("ProfileAdmin", "BEIndex");
        }
    }
}