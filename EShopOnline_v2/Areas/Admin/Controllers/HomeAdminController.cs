using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShopOnline_v2.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            if (Session["isLogon"] == null) return RedirectToAction("Login", "Home", new { area = "" });
            var isLogon = (Int32)Session["isLogon"];
            @ViewBag.UserName = Session["Username"];
            return isLogon == 1 ? (ActionResult)View() : RedirectToAction("Login", "Home", new { area = "" });
        }

        // GET: Admin/HomeAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/HomeAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/HomeAdmin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/HomeAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/HomeAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/HomeAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/HomeAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
