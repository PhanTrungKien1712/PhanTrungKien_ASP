using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;

namespace ThoiTrangNam.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private MyDBContext db = new MyDBContext();
        private UserDAO userDAO = new UserDAO();
        // GET: Admin/Category
        public ActionResult Index()
        {
            List<User> list = userDAO.getList("Index");
            return View("Index", list);
        }

        // GET: Admin/user/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/user/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/user/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                user.CreatedAt = DateTime.Now;
                userDAO.Insert(user);
                TempData["message"] = new XMessage("success ", "Thêm thành công");
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Admin/user/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/user/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                user.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
                user.UpdatedAt = DateTime.Now;
                userDAO.Update(user);
                TempData["message"] = new XMessage("success", " Cập nhật thành công");
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Admin/user/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/user/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = userDAO.getRow(id);
            userDAO.Delete(user);
            TempData["message"] = new XMessage("success", "Xóa mẫu tin thành công");
            return RedirectToAction("Trash", "User");
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã không tồn tại");
                return RedirectToAction("Index", "User");
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "User");
            }
            user.Status = (user.Status == 1) ? 2 : 1;
            user.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            user.UpdatedAt = DateTime.Now;
            userDAO.Update(user);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "User");
        }
        public ActionResult Trash(int? id)
        {
            return View(userDAO.getList("Trash"));
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã không tồn tại");
                return RedirectToAction("Index", "User");
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "User");
            }
            user.Status = 0; //Trạng thái rác = 0
            user.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            user.UpdatedAt = DateTime.Now;
            userDAO.Update(user);
            TempData["message"] = new XMessage("success", "Xóa thành công");
            return RedirectToAction("Index", "User");
        }

        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã không tồn tại");
                return RedirectToAction("Trash", "User");
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "User");
            }
            user.Status = 2;
            user.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            user.UpdatedAt = DateTime.Now;
            userDAO.Update(user);
            TempData["message"] = new XMessage("success", "Khôi phục thành công");
            return RedirectToAction("Index", "User");
        }
        public String NameCustomer(int userid)
        {
            User user = userDAO.getRow(userid);
            if(user==null)
            {
                return "";
            }
            else
            {
                return user.FullName;
            }
        }
        public String AddressCustomer(int userid)
        {
            User user = userDAO.getRow(userid);
            if (user == null)
            {
                return "";
            }
            else
            {
                return user.Address;
            }
        }
        public String PhoneCustomer(int userid)
        {
            User user = userDAO.getRow(userid);
            if (user == null)
            {
                return "";
            }
            else
            {
                return user.Phone;
            }
        }
        public String EmailCustomer(int userid)
        {
            User user = userDAO.getRow(userid);
            if (user == null)
            {
                return "";
            }
            else
            {
                return user.Email;
            }
        }
    }
}
