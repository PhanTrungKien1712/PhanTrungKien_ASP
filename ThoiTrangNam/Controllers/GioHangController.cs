using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace ThoiTrangNam.Controllers
{
    public class GioHangController : Controller
    {
        OrderDAO orderDAO = new OrderDAO();
        OrderDetailDAO orderdetailDAO = new OrderDetailDAO();
        UserDAO userDAO = new UserDAO();
        ProductDAO productDAO = new ProductDAO();
        XCart xcart = new XCart();
        // GET: GioHang
        public ActionResult Index()
        {
            List<CartItem> listcart = xcart.getCart();
            return View("Index", listcart);
        }
        public ActionResult CartAdd(int productid)
        {
            Product product = productDAO.getRow(productid);//Chi tiết sản phẩm
            CartItem cartitem = new CartItem(product.Id, product.Name, product.Img, product.PriceSale, 1);
            //Add vào giỏ hàng
            List<CartItem> listcart = xcart.AddCart(cartitem);
            return RedirectToAction("Index", "Giohang");
        }
        public ActionResult CartDel(int productid)
        {
            xcart.DelCart(productid);
            return RedirectToAction("Index", "Giohang");
        }
        public ActionResult CartUpdate(FormCollection form)
        {
            if (!string.IsNullOrEmpty(form["CAPNHAT"]))
            {
                var listqty = form["qty"];
                var listarr = listqty.Split(',');
                xcart.UpdateCart(listarr);    
            }    
            return RedirectToAction("Index", "Giohang");
        }
        public ActionResult CartDelAll()
        {
            xcart.DelCart();
            return RedirectToAction("Index", "Giohang");
        }
        //ThanhToan
        public ActionResult ThanhToan()
        {     
            List<CartItem> listcart = xcart.getCart();
            //Kiểm tra đăng nhập người dùng ==> Khách hàng
            if (Session["UserCustomer"].Equals(""))
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            int userid = int.Parse(Session["CustomerId"].ToString()); //Mã người đăng nhập
            User user = userDAO.getRow(userid);
            ViewBag.user = user;
            return View("ThanhToan", listcart);
        }
        public ActionResult DatMua(FormCollection field)
        {
            //Lưu thông tin vào CSDL Order và OrderDetail
            int userid = int.Parse(Session["CustomerId"].ToString()); //Mã người đăng nhập
            User user = userDAO.getRow(userid);
            //Lấy thông tin
            String ReceriverName = field["ReceriverName"];
            String ReceriverEmail = field["ReceriverEmail"];
            String ReceriverPhone = field["ReceriverPhone"];
            String ReceriverAddress = field["ReceriverAddress"];
            String note = field["Note"];
            //Tạo đối tượng đơn hàng
            Order order = new Order();
            order.UserId = userid;
            order.ReceriverName = ReceriverName;
            order.ReceriverEmail = ReceriverEmail;
            order.ReceriverPhone = ReceriverPhone;
            order.ReceriverAddress = ReceriverAddress;
            order.Note = note;
            order.CreatedAt = DateTime.Now;
            order.Status = 1;
            if (orderDAO.Insert(order) == 1)
            {
                //Thêm vào chi tiết đơn hàng
                List<CartItem> listcart = xcart.getCart();
                foreach(CartItem cartItem in listcart)
                {
                    OrderDetail orderdetail = new OrderDetail();
                    orderdetail.OrderId = order.Id;
                    orderdetail.ProductId = cartItem.ProductId;
                    orderdetail.Price = cartItem.Price;
                    orderdetail.Qty = cartItem.Qty;
                    orderdetail.Amount = cartItem.Amount;
                    orderdetailDAO.Insert(orderdetail);//Lưu
                }    
            }
            return RedirectToAction("DatHangThanhCong", "GioHang");
        }
        public ActionResult TaiKhoan()
        {
            int userid = int.Parse(Session["CustomerId"].ToString()); //Mã người đăng nhập
            User user = userDAO.getRow(userid);
            ViewBag.user = user;
            return View("TaiKhoan");
        }
        public ActionResult DatHangThanhCong()
        {
            return View();
        }
    }
}