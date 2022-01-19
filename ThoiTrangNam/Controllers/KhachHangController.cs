using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;
using System.Net;

namespace ThoiTrangNam.Controllers
{
    public class KhachHangController : Controller
    {
        UserDAO userDAO = new UserDAO();
        // GET: KhachHang
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection filed)
        {
            String username = filed["username"];
            String password = filed["password"];
            User row_user = userDAO.getRow(username, "Customer");
            String strError = "";
            if (row_user == null)
            {
                strError = "Tên đăng nhập không tồn tại";
            }
            else
            {
                if(password.Equals(row_user.Password))
                {
                    Session["UserCustomer"] = username;
                    Session["CustomerId"] = row_user.Id;
                    return Redirect("~/");
                }
                else
                {
                    strError = password;
                }    
            }    
            ViewBag.Error = "<span class ='text-danger'>"+strError+"</div>";
            return View("DangNhap");
        }
        //Đăng ký
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection filed, User user)
        {
            var fullname = filed["fullname"];
            var username = filed["username"];
            var password = filed["password"];
            var email = filed["email"];
            var phone = filed["phone"];
            var address = filed["address"];
            if (String.IsNullOrEmpty(fullname))
            {
                ViewData["Loi1"] = "Họ tên khách hàng không dược để trống";
            }
            else if (String.IsNullOrEmpty(username))
            {
                ViewData["Loi2"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(password))
            {
                ViewData["Loi3"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi4"] = "Email không được bỏ trống";
            }
            else if (String.IsNullOrEmpty(phone))
            {
                ViewData["Loi5"] = "Phải nhập số điện thoại";
            }
            else if (String.IsNullOrEmpty(address))
            {
                ViewData["Loi6"] = "Không được bỏ trống địa chỉ";
            }
            else
            {
                user.FullName = fullname;
                user.UserName = username;
                user.Password = password;
                user.Email = email;
                user.Phone = phone;
                user.Address = address;
                user.Roles = "Customer";
                user.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                user.CreatedAt = DateTime.Now;
                user.Status = 1;
                userDAO.Insert(user);
                return RedirectToAction("DangKyThanhCong", "KhachHang");
            }
            return View("DangKy");
        }
        public ActionResult DangXuat()
        {
            Session["UserCustomer"] = null;
            Session.Clear();
            Session.Abandon();
            return Redirect("~/");
        }
        public ActionResult DangKyThanhCong()
        {
            return View();
        }
    }
}