using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;

namespace ThoiTrangNam.Controllers
{
    public class LienHeController : Controller
    {
        ContactDAO contactDAO = new ContactDAO();
        // GET: LienHe
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult LienHe()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LienHe(FormCollection filed, Contact contact)
        {
            var fullname = filed["fullname"];
            var phone = filed["phone"];
            var email = filed["email"];
            var title = filed["title"];
            var detail = filed["detail"];

                contact.FullName = fullname;
                contact.Phone = phone;
                contact.Email = email;
                contact.Title = title;
                contact.Detail = detail;
                contact.CreatedAt = DateTime.Now;
                contact.Status = 1;
                contactDAO.Insert(contact);
            return RedirectToAction("LienHeThanhCong", "LienHe");
        }
        public ActionResult LienHeThanhCong()
        {
            return View();
        }
    }
}