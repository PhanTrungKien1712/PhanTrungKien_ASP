using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace ThoiTrangNam.Controllers
{
    public class ModuleController : Controller
    {
        private TopicDAO topicDAO = new TopicDAO();
        public MenuDAO menuDAO = new MenuDAO();
        public SliderDAO sliderDAO = new SliderDAO();
        private CategoryDAO categoryDAO = new CategoryDAO();
        // GET: Module
        public ActionResult MainMenu()
        {
            List<Menu> list = menuDAO.getListByParentId("mainmenu",0);
            return View("MainMenu", list);
        }
        public ActionResult MainMenuSub(int id)
        {
            Menu menu = menuDAO.getRow(id);
            List<Menu> list = menuDAO.getListByParentId("mainmenu",id);
            if (list.Count == 0)
            {
                return View("MainMenuSub1", menu); //không có cấp con
            }   
            else
            {
                ViewBag.Menu = menu;
                return View("MainMenuSub2", list); // có cấp con
            }    
        }
        //SliderShow
        public ActionResult Slideshow()
        {
            List<Slider> list = sliderDAO.getListByPosition("Slideshow");
            return View("Slideshow", list);
        }
        //ListCategory
        public ActionResult ListCategory()
        {
            List<Category> list = categoryDAO.getListByParentId(0);
            return View("ListCategory", list);
        }
        //PostLastNews
        public ActionResult PostLastNews()
        {
            List<Topic> list = topicDAO.getListByParentId(0);
            return View("PostLastNews", list);
        }
        //MenuFooter
        public ActionResult MenuFooter()
        {
            List<Menu> list = menuDAO.getListByParentId("footermenu",0);
            return View("MenuFooter", list);
        }
    }
}