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
using System.IO;

namespace ThoiTrangNam.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        private MyDBContext db = new MyDBContext();
        private PostDAO postDAO = new PostDAO();
        private TopicDAO topicDAO = new TopicDAO();
        private LinkDAO linkDAO = new LinkDAO();
        // GET: Admin/Post
        public ActionResult Index()
        {
            return View(postDAO.getList("Index", "Post"));
        }

        // GET: Admin/Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Admin/Post/Create
        public ActionResult Create()
        {
            ViewBag.ListTopic = new SelectList(topicDAO.getList("Index"), "Id", "Name", 0);
            return View();
        }

        // POST: Admin/Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                //Xử lý thêm thông tin
                post.PostType = "Post";
                post.Slug = XString.Str_Slug(post.Title);
                post.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                post.CreatedAt = DateTime.Now;
                //Upload file
                var FileImg = Request.Files["Img"];
                if (FileImg.ContentLength != 0)
                {
                    string[] FileExtension = new string[] { ".jpg", ".png", ".gif", ".jepg" };
                    if (FileExtension.Contains(FileImg.FileName.Substring(FileImg.FileName.LastIndexOf("."))))
                    {
                        string imgName = post.Slug + FileImg.FileName.Substring(FileImg.FileName.LastIndexOf("."));
                        string pathDir = "~/Public/images/posts/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
                        // upload file
                        FileImg.SaveAs(pathImg);
                        //luu hinh vào cơ sỡ
                        post.Img = imgName;
                    }
                }
                //end Upoad file
                if (postDAO.Insert(post) == 1)
                {
                    Link link = new Link();
                    link.Slug = post.Slug;
                    link.TableId = post.Id;
                    link.Type = "post";
                    linkDAO.Insert(link);
                }
                TempData["message"] = new XMessage("success", "Thêm thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListTopic = new SelectList(topicDAO.getList("Index"), "Id", "Name", 0);
            return View(post);
        }

        // GET: Admin/Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListTopic = new SelectList(topicDAO.getList("Index"), "Id", "Name", 0);
            return View(post);
        }

        // POST: Admin/Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                post.PostType = "Post";
                post.Slug = XString.Str_Slug(post.Title);
                post.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
                post.UpdatedAt = DateTime.Now;
                //Upload file
                var FileImg = Request.Files["Img"];
                if (FileImg.ContentLength != 0)
                {
                    string[] FileExtension = new string[] { ".jpg", ".png", ".gif", ".jepg" };
                    if (FileExtension.Contains(FileImg.FileName.Substring(FileImg.FileName.LastIndexOf("."))))
                    {
                        string imgName = post.Slug + FileImg.FileName.Substring(FileImg.FileName.LastIndexOf("."));
                        string pathDir = "~/Public/images/posts/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
                        //xóa hình cũ
                        if (post.Img != null)
                        {
                            string pathImgDelete = Path.Combine(Server.MapPath(pathDir), post.Img);
                            System.IO.File.Delete(pathImgDelete);
                        }
                        // upload file
                        FileImg.SaveAs(pathImg);
                        //luu hinh vào cơ sỡ
                        post.Img = imgName;
                    }
                }
                //end Upoad file
                postDAO.Update(post);
                TempData["message"] = new XMessage("success ", "Cập nhật thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListTopic = new SelectList(topicDAO.getList("Index"), "Id", "Name", 0);
            return View(post);
        }

        // GET: Admin/Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = postDAO.getRow(id);
            postDAO.Delete(post);
            TempData["message"] = new XMessage("success ", "Xóa mãu tin thành công");
            return RedirectToAction("Trash", "post");
        }
        public ActionResult Trash(int? id)
        {
            return View(postDAO.getList("Trash", "Post"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã không tồn tại");
                return RedirectToAction("Index", "Post");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Post");
            }
            post.Status = (post.Status == 1) ? 2 : 1;
            post.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            post.UpdatedAt = DateTime.Now;
            postDAO.Update(post);
            TempData["message"] = new XMessage("success ", "Thay đổi trạng thái thàng công");
            return RedirectToAction("Index", "Post");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã không tồn tại");
                return RedirectToAction("Index", "Post");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Post");
            }
            post.Status = 0;//Trạng thái rác =0
            post.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            post.UpdatedAt = DateTime.Now;
            postDAO.Update(post);
            TempData["message"] = new XMessage("success ", "Xóa thành công");
            return RedirectToAction("Index", "Post");
        }
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã không tồn tại");
                return RedirectToAction("Trash", "Post");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Post");
            }
            post.Status = 2;//Trạng thái rác =0
            post.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            post.UpdatedAt = DateTime.Now;
            postDAO.Update(post);
            TempData["message"] = new XMessage("success ", "Khôi phục thành công");
            return RedirectToAction("Index", "Post");
        }
    }
}
