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
    public class MenuController : Controller
    {
        private MyDBContext db = new MyDBContext();
        CategoryDAO categoryDAO = new CategoryDAO();
        TopicDAO topicDAO = new TopicDAO();
        PostDAO postDAO = new PostDAO();
        MenuDAO menuDAO = new MenuDAO();
        SupplierDAO supplierDAO = new SupplierDAO();
        // GET: Admin/Menu
        public ActionResult Index()
        {
            ViewBag.ListCategory = categoryDAO.getList("Index");
            ViewBag.ListTopic = topicDAO.getList("Index");
            ViewBag.ListPage = postDAO.getList("Index", "Page");
            List<Menu> menu = menuDAO.getList("Index");
            return View("Index", menu);
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            if(!string.IsNullOrEmpty(form["ThemCategory"]))
            {
                if (!string.IsNullOrEmpty(form["itemcat"]))
                {
                    var listitem = form["itemcat"];
                    var listarr = listitem.Split(',');
                    foreach(var row in listarr)
                    {
                        int id = int.Parse(row);
                        Category category = categoryDAO.getRow(id);
                        Menu menu = new Menu();
                        menu.Name = category.Name;
                        menu.Link = category.Slug;
                        menu.TableId = category.Id;
                        menu.TypeMenu = "category";
                        menu.Position = form["Position"];
                        menu.ParentId = 0;
                        menu.Orders = 0;
                        menu.CreatedAt = DateTime.Now;
                        menu.CreatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                        menu.Status = 1;
                        menuDAO.Insert(menu);
                    }
                    TempData["message"] = new XMessage("success", "Thêm menu thành công");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "Chưa chọn danh mục sản phẩm");
                }
            }
            //Topic
            if (!string.IsNullOrEmpty(form["ThemTopic"]))
            {
                if (!string.IsNullOrEmpty(form["itemtopic"]))
                {
                    var listitem = form["itemtopic"];
                    var listarr = listitem.Split(',');
                    foreach (var row in listarr)
                    {
                        int id = int.Parse(row);
                        Topic topic = topicDAO.getRow(id);
                        Menu menu = new Menu();
                        menu.Name = topic.Name;
                        menu.Link = topic.Slug;
                        menu.TableId = topic.Id;
                        menu.TypeMenu = "topic";
                        menu.Position = form["Position"];
                        menu.ParentId = 0;
                        menu.Orders = 0;
                        menu.CreatedAt = DateTime.Now;
                        menu.CreatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                        menu.Status = 1;
                        menuDAO.Insert(menu);
                    }
                    TempData["message"] = new XMessage("success", "Thêm menu thành công");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "Chưa chọn chủ đề bài viết");
                }
            }
            //Page
            if (!string.IsNullOrEmpty(form["ThemPage"]))
            {
                if (!string.IsNullOrEmpty(form["itempage"]))
                {
                    var listitem = form["itempage"];
                    var listarr = listitem.Split(',');
                    foreach (var row in listarr)
                    {
                        int id = int.Parse(row);
                        Post post = postDAO.getRow(id);
                        Menu menu = new Menu();
                        menu.Name = post.Title;
                        menu.Link = post.Slug;
                        menu.TableId = post.Id;
                        menu.TypeMenu = "page";
                        menu.Position = form["Position"];
                        menu.ParentId = 0;
                        menu.Orders = 0;
                        menu.CreatedAt = DateTime.Now;
                        menu.CreatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                        menu.Status = 1;
                        menuDAO.Insert(menu);
                    }
                    TempData["message"] = new XMessage("success", "Thêm menu thành công");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "Chưa chọn trang đơn");
                }
            }
            //Topic
            if (!string.IsNullOrEmpty(form["ThemCustom"]))
            { 
                if (!string.IsNullOrEmpty(form["name"]) && !string.IsNullOrEmpty(form["link"]))
                {
                    Menu menu = new Menu();
                    menu.Name = form["name"];
                    menu.Link = form["link"];
                    menu.TypeMenu = "custom";
                    menu.Position = form["Position"];
                    menu.ParentId = 0;
                    menu.Orders = 0;
                    menu.CreatedAt = DateTime.Now;
                    menu.CreatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                    menu.Status = 1;
                    menuDAO.Insert(menu);
                    TempData["message"] = new XMessage("success", "Thêm menu thành công");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "Chưa nhập đủ thông tin");
                }
            }
             return RedirectToAction("Index", "Menu");
        }

        // GET: Admin/Menu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: Admin/Menu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Menu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Link,ParentId,Orders,TableId,Type,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Status")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Menus.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menu);
        }

        // GET: Admin/Menu/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ListMenu = new SelectList(menuDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(menuDAO.getList("Index"), "Orders", "Name");
            Menu menu = menuDAO.getRow(id);
            return View("Edit",menu);
        }

        // POST: Admin/Menu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Menu menu)
        {
            if (ModelState.IsValid)
            {
                if (menu.ParentId == null)
                {
                    menu.ParentId = 0;
                }
                if (menu.Orders == null)
                {
                    menu.Orders = 1;
                }
                else
                {
                    menu.Orders += 1;
                }
                menu.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
                menu.UpdatedAt = DateTime.Now;
                menuDAO.Update(menu);
                TempData["message"] = new XMessage("success", " Cập nhật thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListMenu = new SelectList(menuDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(menuDAO.getList("Index"), "Orders", "Name");
            return View(menu);
        }

        // GET: Admin/Menu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Admin/Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = menuDAO.getRow(id);
            menuDAO.Delete(menu);
            TempData["message"] = new XMessage("success", "Xóa mẫu tin thành công");
            return RedirectToAction("Trash", "Menu");
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã không tồn tại");
                return RedirectToAction("Index", "Menu");
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Menu");
            }
            menu.Status = (menu.Status == 1) ? 2 : 1;
            menu.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            menu.UpdatedAt = DateTime.Now;
            menuDAO.Update(menu);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Menu");
        }
        public ActionResult Trash(int? id)
        {
            List<Menu> menu = menuDAO.getList("Trash");
            return View("Trash", menu);
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã không tồn tại");
                return RedirectToAction("Index");
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index");
            }
            menu.Status = 0; //Trạng thái rác = 0
            menu.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            menu.UpdatedAt = DateTime.Now;
            menuDAO.Update(menu);
            TempData["message"] = new XMessage("success", "Xóa thành công");
            return RedirectToAction("Index");
        }

        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã không tồn tại");
                return RedirectToAction("Trash", "Menu");
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Menu");
            }
            menu.Status = 2;
            menu.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            menu.UpdatedAt = DateTime.Now;
            menuDAO.Update(menu);
            TempData["message"] = new XMessage("success", "Khôi phục thành công");
            return RedirectToAction("Index", "Menu");
        }
    }
}
