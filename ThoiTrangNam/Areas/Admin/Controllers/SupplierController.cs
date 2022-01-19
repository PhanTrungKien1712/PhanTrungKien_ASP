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
    public class SupplierController : Controller
    {
        private MyDBContext db = new MyDBContext();
        SupplierDAO supplierDAO = new SupplierDAO();
        LinkDAO linkDAO = new LinkDAO();
        // GET: Admin/supplier
        public ActionResult Index()
        {
            List<Supplier> list = supplierDAO.getList("Index");
            return View("Index", list);
        }

        // GET: Admin/supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // GET: Admin/supplier/Create
        public ActionResult Create()
        {
            ViewBag.ListOrder = new SelectList(supplierDAO.getList("Index"), "Order", "Name", 0);
            return View();
        }

        // POST: Admin/supplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                //Xử lý thêm thông tin
                supplier.Slug = XString.Str_Slug(supplier.Name);
                if (supplier.Orders == null)
                {
                    supplier.Orders = 1;
                }
                else
                {
                    supplier.Orders += 1;
                }
                supplier.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                supplier.CreatedAt = DateTime.Now;
                if (supplierDAO.Insert(supplier) == 1)
                {
                    Link link = new Link();
                    link.Slug = supplier.Slug;
                    link.TableId = supplier.Id;
                    link.Type = "supplier";
                    linkDAO.Insert(link);
                }
                TempData["message"] = new XMessage("success", "Thêm thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListOrder = new SelectList(supplierDAO.getList("Index"), "Order", "Name", 0);
            return View(supplier);
        }

        // GET: Admin/supplier/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListOrder = new SelectList(supplierDAO.getList("Index"), "Order", "Name", 0);
            return View(supplier);
        }

        // POST: Admin/supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                supplier.Slug = XString.Str_Slug(supplier.Name);
                if (supplier.Orders == null)
                {
                    supplier.Orders = 1;
                }
                else
                {
                    supplier.Orders += 1;
                }
                supplier.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
                supplier.UpdatedAt = DateTime.Now;
                if (supplierDAO.Update(supplier) == 1)
                {
                    Link link = new Link();
                    link.Slug = supplier.Slug;
                    link.TableId = supplier.Id;
                    link.Type = "supplier";
                    linkDAO.Update(link);
                }
                TempData["message"] = new XMessage("success", " Cập nhật thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListOrder = new SelectList(supplierDAO.getList("Index"), "Order", "Name", 0);
            return View(supplier);
        }

        // GET: Admin/supplier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Admin/supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier = supplierDAO.getRow(id);
            supplierDAO.Delete(supplier);
            TempData["message"] = new XMessage("success", "Xóa mẫu tin thành công");
            return RedirectToAction("Trash", "supplier");
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "supplier");
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "supplier");
            }
            supplier.Status = (supplier.Status == 1) ? 2 : 1;
            supplier.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            supplier.UpdatedAt = DateTime.Now;
            supplierDAO.Update(supplier);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "supplier");
        }
        public ActionResult Trash(int? id)
        {
            return View(supplierDAO.getList("Trash"));
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "supplier");
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "supplier");
            }
            supplier.Status = 0; //Trạng thái rác = 0
            supplier.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            supplier.UpdatedAt = DateTime.Now;
            supplierDAO.Update(supplier);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "supplier");
        }

        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "supplier");
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "supplier");
            }
            supplier.Status = 2;
            supplier.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            supplier.UpdatedAt = DateTime.Now;
            supplierDAO.Update(supplier);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Trash", "supplier");
        }
    }
}
