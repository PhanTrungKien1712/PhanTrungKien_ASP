using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;


namespace ThoiTrangNam.Controllers
{
    public class TimKiemController : Controller
    {
        ProductDAO productDAO = new ProductDAO();
        // GET: TimKiem
        public ActionResult Index(string search)
        {
            search = search.ToLower();
            List<Product> list = productDAO.SearchByKey(search); //lọc theo chuỗi tìm kiếm
            return View("Index", list); //trả về kết quả
        }
    }
}