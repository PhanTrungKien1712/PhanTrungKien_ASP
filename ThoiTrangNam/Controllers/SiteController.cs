using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;
using PagedList;

namespace ThoiTrangNam.Controllers
{
    
    public class SiteController : Controller
    {
        
        LinkDAO linkDAO = new LinkDAO();
        ProductDAO productDAO = new ProductDAO();
        PostDAO postDAO = new PostDAO();
        CategoryDAO categoryDAO = new CategoryDAO();
        TopicDAO topicDAO = new TopicDAO();

        // GET: Site
        public ActionResult Index(string slug = null, int? page = null)
        {
            MyDBContext db = new MyDBContext();
            //int count = db.Products.Count();
            //ViewBag.SoMauTin = count.ToString();

            if (slug == null)
            {
                return this.Home();
            }
            else
            {
                Link link = linkDAO.getRow(slug);
                if (link != null)
                {
                    string typelink = link.Type;
                    switch (typelink)
                    {
                        case "category":
                            {
                                return this.ProductCategory(slug, page);
                            }
                        case "topic":
                            {
                                return this.PostTopic(slug, page);
                            }
                        case "page":
                            {
                                return this.PostPage(slug);
                            }
                        default:
                            {
                                return this.Error404(slug);
                            }
                    }
                }
                else
                {
                    Product product = productDAO.getRow(slug);
                    if (product != null)
                    {
                        return this.ProductDetail(product/*, id*/);
                    }
                    else
                    {
                        Post post = postDAO.getRow(slug);
                        if (post != null)
                        {
                            return this.PostDetail(post);
                        }
                        else
                        {

                            return this.Error404(slug);
                        }

                    }
                }
            }
        }
        //Trang chủ
        public ActionResult Home()
        {
            List<ProductInfo> productNew = productDAO.getList(0);
            ViewBag.ProductNew = productNew;
            List<Category> list = categoryDAO.getListByParentId(0);
            return View("Home", list);
        }
        public ActionResult HomeProduct(int id)
        {
            Category category = categoryDAO.getRow(id); //Chi tiết id
            ViewBag.Category = category;
            //Sản phẩm theo danh mục theo 3 cấp
            List<int> listcatid = new List<int>();
            listcatid.Add(id);//Cấp 1
            List<Category> liscategory2 = categoryDAO.getListByParentId(id);
            if (liscategory2.Count() != 0)
            {
                foreach(var category2 in liscategory2)
                {
                    listcatid.Add(category2.Id);//Cấp 2
                    List<Category> liscategory3 = categoryDAO.getListByParentId(category2.Id);
                    if (liscategory3.Count() != 0)
                    {
                        foreach(var category3 in liscategory3)
                        {
                            listcatid.Add(category3.Id); //Cấp 3
                        }                           
                    }    
                }    
            }    
            List<ProductInfo> list = productDAO.getListByListCatId(listcatid, 4);
            return View("HomeProduct", list);
        }
        //Nhóm Action Product
        public ActionResult Product(int? page)
        {
            int pageNumber = page ?? 1; //Trang thứ ?
            int pageSize = 9; //Số mẫu tin xuất hiện trên trang
            IPagedList<ProductInfo> list = productDAO.getList(pageSize, pageNumber);
            return View("Product", list);
        }
        private ActionResult ProductCategory(string slug, int? page)
        {
            int pageNumber = page ?? 1; //Trang thứ ?
            int pageSize = 9; //Số mẫu tin xuất hiện trên trang
            Category category = categoryDAO.getRow(slug);
            ViewBag.Category = category;
            //Sản phẩm theo danh mục theo 3 cấp
            List<int> listcatid = new List<int>();
            listcatid.Add(category.Id);//Cấp 1
            List<Category> liscategory2 = categoryDAO.getListByParentId(category.Id);
            if (liscategory2.Count() != 0)
            {
                foreach (var category2 in liscategory2)
                {
                    listcatid.Add(category2.Id);//Cấp 2
                    List<Category> liscategory3 = categoryDAO.getListByParentId(category2.Id);
                    if (liscategory3.Count() != 0)
                    {
                        foreach (var category3 in liscategory3)
                        {
                            listcatid.Add(category3.Id); //Cấp 3
                        }
                    }
                }
            }
            IPagedList<ProductInfo> list = productDAO.getListByListCatId(listcatid, pageSize, pageNumber); 
            return View("ProductCategory", list);
        }
        public ActionResult ProductDetail(Product product /*int id*/)
        {
            //Category category = categoryDAO.getRow(id);
            //ViewBag.Category = category;
            ViewBag.ListOther = productDAO.getListByCatId(4, product.CatId, product.Id);
            return View("ProductDetail", product);
        }
        //Nhóm Post
        public ActionResult HomePost(int id)
        {
            Topic topic = topicDAO.getRow(id);
            ViewBag.Topic = topic;
            List<int> listtopicid = new List<int>();
            listtopicid.Add(id);//Cấp 1
            List<Topic> listopic2 = topicDAO.getListByParentId(id);
            if (listopic2.Count() != 0)
            {
                foreach (var topic2 in listopic2)
                {
                    listtopicid.Add(topic2.Id);//Cấp 2
                    List<Category> listtopic3 = categoryDAO.getListByParentId(topic2.Id);
                    if (listtopic3.Count() != 0)
                    {
                        foreach (var topic3 in listtopic3)
                        {
                            listtopicid.Add(topic3.Id); //Cấp 3
                        }
                    }
                }
            }
            List<PostInfo> list = postDAO.getListByListTopicId(listtopicid, 3);
            return View("HomePost", list);
        }
        public ActionResult Post()
        {
            List<PostInfo> list = postDAO.getList("Post");
            return View("Post", list);
        }
        public ActionResult PostTopic(string slug, int? page)
        {
            Topic topic = topicDAO.getRow(slug);
            ViewBag.Topic = topic;
            List<PostInfo> list = postDAO.getListByTopicId(topic.Id, "Post", null);
            return View("PostTopic", list);
        }
        public ActionResult PostPage(string slug)
        {
            Post post = postDAO.getRow(slug,"page");
            return View("PostPage", post);
        }
        public ActionResult PostDetail(Post post)
        {
            ViewBag.ListOther = postDAO.getListByTopicId(post.TopicId, "Post", post.Id);
            return View("PostDetail", post);
        }
        //Hàm lỗi
        public ActionResult Error404(string slug)
        {
            return View("Error404");
        }
    }
}