using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;
using System.IO;
using System.Net;

namespace ThoiTrangNam.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private MyDBContext db = new MyDBContext();
        private LinkDAO linkDAO = new LinkDAO();
        private ProductDAO productDAO = new ProductDAO();
        private CategoryDAO categoryDAO = new CategoryDAO();
        // GET: Admin/Product
        public ActionResult Index()
        {
            List<ProductInfo> list = productDAO.getListJoin("Index");
            return View("Index", list);
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                //Xử lý thêm thông tin
                product.Slug = XString.Str_Slug(product.Name);
                product.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                product.CreatedAt = DateTime.Now;
                //Upload file
                var FileImg = Request.Files["Img"];
                if (FileImg.ContentLength != 0)
                {
                    string[] FileExtension = new string[] { ".jpg", ".png", ".gif", ".jepg" };
                    if (FileExtension.Contains(FileImg.FileName.Substring(FileImg.FileName.LastIndexOf("."))))
                    {
                        string imgName = product.Slug + FileImg.FileName.Substring(FileImg.FileName.LastIndexOf("."));
                        string pathDir = "~/Public/images/products/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
                        // upload file
                        FileImg.SaveAs(pathImg);
                        //luu hinh vào cơ sỡ
                        product.Img = imgName;
                    }
                }
                //end Upoad file
                if (productDAO.Insert(product) == 1)
                {
                    Link link = new Link();
                    link.Slug = product.Slug;
                    link.TableId = product.Id;
                    link.Type = "product";
                    linkDAO.Insert(link);
                }
                TempData["message"] = new XMessage("success", "Thêm thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name");
            return View(product);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name");
            return View(product);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Slug = XString.Str_Slug(product.Name);
                product.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
                product.UpdatedAt = DateTime.Now;
                //Upload file
                var FileImg = Request.Files["Img"];
                if (FileImg.ContentLength != 0)
                {
                    string[] FileExtension = new string[] { ".jpg", ".png", ".gif", ".jepg" };
                    if (FileExtension.Contains(FileImg.FileName.Substring(FileImg.FileName.LastIndexOf("."))))
                    {
                        string imgName = product.Slug + FileImg.FileName.Substring(FileImg.FileName.LastIndexOf("."));
                        string pathDir = "~/Public/images/products/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
                        //xóa hình cũ
                        if (product.Img != null)
                        {
                            string pathImgDelete = Path.Combine(Server.MapPath(pathDir), product.Img);
                            System.IO.File.Delete(pathImgDelete);
                        }
                        // upload file
                        FileImg.SaveAs(pathImg);
                        //luu hinh vào cơ sỡ
                        product.Img = imgName;
                    }
                }
                productDAO.Update(product);
                TempData["message"] = new XMessage("success ", "Cập nhật thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name");
            return View(product);
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = productDAO.getRow(id);
            productDAO.Delete(product);
            TempData["message"] = new XMessage("success ", "Xóa mãu tin thành công");
            return RedirectToAction("Trash", "Product");
        }
        public ActionResult Trash(int? id)
        {
            List<ProductInfo> list = productDAO.getListJoin("Trash");
            return View("Trash", list);
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã sản phẩm không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            product.Status = (product.Status == 1) ? 2 : 1;
            product.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            product.UpdatedAt = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success ", "Thay đổi trạng thái thàng công");
            return RedirectToAction("Index", "Product");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã sản phẩm không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            product.Status = 0;//Trạng thái rác =0
            product.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            product.UpdatedAt = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success ", "Xóa thành công");
            return RedirectToAction("Index", "Product");
        }
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Product");
            }
            product.Status = 2;//Trạng thái rác =0
            product.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            product.UpdatedAt = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success ", "Khôi phục thành công");
            return RedirectToAction("Index", "Product");
        }
        public String ProductImg(int? productid)
        {
            Product product = productDAO.getRow(productid);
            if (product == null)
            {
                return "";
            }
            else
            {
                return product.Img;
            }
        }
        public String ProductName(int? productid)
        {
            Product product = productDAO.getRow(productid);
            if (product == null)
            {
                return "";
            }
            else
            {
                return product.Name;
            }
        }
    }
}