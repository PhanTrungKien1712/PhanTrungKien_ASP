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
    public class ContactController : Controller
    {
        private MyDBContext db = new MyDBContext();
        private ContactDAO contactDAO = new ContactDAO();

        // GET: Admin/Contact
        public ActionResult Index()
        {
            return View(db.Contacts.ToList());
        }

        // GET: Admin/Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Admin/Contact/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Contact/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FullName,Phone,Email,Title,Content,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Status")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        // GET: Admin/Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Admin/Contact/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,FullName,Phone,Email,Title,Content,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Status")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Admin/Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Admin/Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "contact");
            }
            Contact contact = contactDAO.getRow(id);
            if (contact == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "contact");
            }
            contact.Status = (contact.Status == 1) ? 2 : 1;
            contact.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            contact.UpdatedAt = DateTime.Now;
            contactDAO.Update(contact);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "contact");
        }
        public ActionResult Trash(int? id)
        {
            return View(contactDAO.getList("Trash"));
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "contact");
            }
            Contact contact = contactDAO.getRow(id);
            if (contact == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "contact");
            }
            contact.Status = 0; //Trạng thái rác = 0
            contact.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            contact.UpdatedAt = DateTime.Now;
            contactDAO.Update(contact);
            TempData["message"] = new XMessage("success", "Xóa thành công");
            return RedirectToAction("Index", "contact");
        }

        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "contact");
            }
            Contact contact = contactDAO.getRow(id);
            if (contact == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "contact");
            }
            contact.Status = 2;
            contact.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            contact.UpdatedAt = DateTime.Now;
            contactDAO.Update(contact);
            TempData["message"] = new XMessage("success", "Khôi phục thành công");
            return RedirectToAction("Index", "contact");
        }
    }
}
