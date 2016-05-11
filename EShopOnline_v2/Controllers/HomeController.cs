using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EShopOnline_v2.DAL;

namespace EShopOnline_v2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        //Get: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TaiKhoan user)
        {
            var i = Accounts.CheckLogIn(user);
            if (i != 0)
            {
                Session["isLogon"] = 1;
                Session["Username"] = user.Ten;
                Session["id"] = user.ID;
                Session.Timeout = 30;

                return RedirectToAction("Index", i == 1 ? "Admin/HomeAdmin" : "Home");
            }
            ModelState.AddModelError("", "Tên tài khoản hoặc mật khẩu không đúng!");
            return View(user);
        }
    }
}