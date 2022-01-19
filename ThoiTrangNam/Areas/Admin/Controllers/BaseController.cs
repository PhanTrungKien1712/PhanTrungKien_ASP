using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThoiTrangNam.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            //Kiểm tra chưa đăng nhập
            if (System.Web.HttpContext.Current.Session["UserAdmin"].Equals(""))
            {
                //Chuyển hướng website
                System.Web.HttpContext.Current.Response.Redirect("~/Admin/login");               
            }
        }
    }
}