using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ThoiTrangNam
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //Khai báo url = Cố định
            routes.MapRoute(
               name: "TatCaSanPham",
               url: "tat-ca-san-pham",
               defaults: new { controller = "Site", action = "Product", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "TatCaBaiViet",
              url: "tat-ca-bai-viet",
              defaults: new { controller = "Site", action = "Post", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "DangNhap",
              url: "dang-nhap",
              defaults: new { controller = "Khachhang", action = "DangNhap", id = UrlParameter.Optional }
          );
            routes.MapRoute(
             name: "DangKy",
             url: "dang-ky",
             defaults: new { controller = "Khachhang", action = "DangKy", id = UrlParameter.Optional }
         );
            routes.MapRoute(
             name: "DangKyThanhCong",
             url: "dang-ky-thanh-cong",
             defaults: new { controller = "Khachhang", action = "DangKyThanhCong", id = UrlParameter.Optional }
         );
            routes.MapRoute(
             name: "TaiKhoan",
             url: "tai-khoan",
             defaults: new { controller = "GioHang", action = "TaiKhoan", id = UrlParameter.Optional }
         );
            routes.MapRoute(
              name: "LienHe",
              url: "lien-he",
              defaults: new { controller = "LienHe", action = "LienHe", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "GioHang",
              url: "gio-hang",
              defaults: new { controller = "GioHang", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "ThanhToan",
              url: "thanh-toan",
              defaults: new { controller = "GioHang", action = "ThanhToan", id = UrlParameter.Optional }
          );
            routes.MapRoute(
            name: "DatHangThanhCong",
            url: "dat-hang-thanh-cong",
            defaults: new { controller = "GioHang", action = "DatHangThanhCong", id = UrlParameter.Optional }
        );
            routes.MapRoute(
              name: "TimKiem",
              url: "tim-kiem",
              defaults: new { controller = "TimKiem", action = "Index", id = UrlParameter.Optional }
          );

            //Khai báo URL động
            routes.MapRoute(
              name: "SiteSlug",
              url: "{slug}",
              defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
          );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
