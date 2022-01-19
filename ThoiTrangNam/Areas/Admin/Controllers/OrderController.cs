using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace ThoiTrangNam.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private OrderDAO orderDAO = new OrderDAO();
        private OrderDetailDAO orderdetailDAO = new OrderDetailDAO();
        private MyDBContext db = new MyDBContext();

        // GET: Admin/Order
        public ActionResult Index()
        {
            List<Order> list = orderDAO.getList("Index");
            return View(list);
        }

        // GET: Admin/Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListChiTiet = orderdetailDAO.getList(id);
            return View(order);
        }

        // GET: Admin/Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,DeliveryName,DeliveryEmail,DeliveryPhone,DeliveryAddress,CreatedAt,UpdatedBy,UpdatedAt,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Admin/Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,DeliveryName,DeliveryEmail,DeliveryPhone,DeliveryAddress,CreatedAt,UpdatedBy,UpdatedAt,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Admin/Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = orderDAO.getRow(id);
            orderDAO.Delete(order);
            TempData["message"] = new XMessage("success ", "Xóa mãu tin thành công");
            return RedirectToAction("Trash", "Order");
        }
        public ActionResult Trash(int? id)
        {
            List<OrderInfo> list = orderDAO.getListJoin("Trash");
            return View("Trash", list);
        }
        public ActionResult Destroy(int? id)
        {

            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            if (order.Status == 1 || order.Status == 2)
            {
                order.Status = 0;
                order.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
                order.UpdatedAt = DateTime.Now;
            }
            else
            {
                if(order.Status==3)
                {
                    TempData["message"] = new XMessage("danger", "Đơn hàng đã vận chuyển không thể hủy");
                    return RedirectToAction("Index", "Order");
                }
                if (order.Status == 4)
                {
                    TempData["message"] = new XMessage("danger", "Đơn hàng đã giao không thể hủy");
                    return RedirectToAction("Index", "Order");
                }
                return RedirectToAction("Index");
            }              
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success ", "Hủy thành công");
            return RedirectToAction("Index", "Order");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã sản phẩm không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            order.Status = 0;//Trạng thái rác =0
            order.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            order.UpdatedAt = DateTime.Now;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success ", "Xóa thành công");
            return RedirectToAction("Index", "Order");
        }
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Order");
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Order");
            }
            order.Status = 2;//Trạng thái rác =0
            order.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            order.UpdatedAt = DateTime.Now;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success ", "Khôi phục thành công");
            return RedirectToAction("Index", "Order");
        }
    }
}
