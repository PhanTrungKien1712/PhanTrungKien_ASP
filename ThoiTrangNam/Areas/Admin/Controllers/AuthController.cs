using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;

namespace ThoiTrangNam.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        private MyDBContext db = new MyDBContext();
        // GET: Admin/Auth
        public ActionResult Login()
        {
            ViewBag.Error = "";
            return View("Login");
        }
        [HttpPost]
        public ActionResult DoLogin(FormCollection field)
        {
            ViewBag.Error = "";
            string username = field["username"];
            string password = field["password"];
            //Kiểm tra xem trong CSDL có Username: admin hoặc Email là phankien209@gmail.com
            User user = db.Users
                .Where(m => m.Roles == "Admin" && m.Status == 1 && (m.UserName == username || m.Email == username))
                .FirstOrDefault();
            if (user != null)
            {
                if (/*user.CountError >= 5&&*/ user.Roles!="Admin") 
                {
                    ViewBag.Error = "<p class='login-box-msg text-danger'>Liên hệ người quản lý!</p>";
                }
                else
                {
                    if (user.Password.Equals(password))
                    {
                        Session["UserAdmin"] = username;
                        Session["UserId"] = user.Id.ToString();
                        Session["FullName"] = user.FullName;
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        //Cập nhật số lần sai
                        if (user.CountError == null)
                        {
                            user.CountError = null;
                        }
                        else
                        {
                            user.CountError += 1;//Tăng lên 1 lần
                        }
                        //Cập nhật lại mẫu tin
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.Error = "<p class='login-box-msg text-danger'>Mật khẩu không chính xác!</p>";
                    }
                }
                
            }
            else
            {
                ViewBag.Error = "<p class='login-box-msg text-danger' > Tài khoản <strong> " + username + "</strong> không tồn tại!</p>";
            }
            return View("Login");
        }
        public ActionResult Logout()
        {
            Session["UserAdmin"] = "";
            Session["UserId"] = "";
            Session["FullName"] = "";
            Session["Img"] = "";
            return Redirect("~/Admin/login");
        }    
    }
}