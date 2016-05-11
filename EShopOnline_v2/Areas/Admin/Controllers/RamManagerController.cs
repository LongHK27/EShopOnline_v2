using System.Web;
using System.Web.Mvc;
using EShopOnline_v2.Areas.Admin.DAL;
using EShopOnline_v2.DAL;

namespace EShopOnline_v2.Areas.Admin.Controllers
{
    public class RamManagerController : Controller
    {
        // GET: Admin/RamAdmin
        public ActionResult Index()
        {
            ViewData["data"] = RamDataAccessLayer.GetTable();
            ViewBag.UserName = Session["Username"];
            return View();
        }

        // GET: Admin/RamAdmin/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.UserName = Session["Username"];
            return View(RamDataAccessLayer.GetItem(id));
        }

        // GET: Admin/RamAdmin/Create
        public ActionResult Create()
        {
            ViewBag.UserName = Session["username"];
            return View();
        }

        // POST: Admin/RamAdmin/Create
        [HttpPost]
        public ActionResult Create(RAM newProduct, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            try
            {
                if (RamDataAccessLayer.Insert(newProduct, file1, file2, file3, Server.MapPath("/")))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Không thể insert dữ liệu!");
                    return View();
                }
            }
            catch
            {
                ModelState.AddModelError("", "Không thể insert dữ liệu!");
                return View();
            }
        }

        // GET: Admin/RamAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.UserName = Session["username"];
            return View(RamDataAccessLayer.GetItem(id));
        }

        // POST: Admin/RamAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, RAM newProduct, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            try
            {
                if (RamDataAccessLayer.Update(id, newProduct, file1, file2, file3, Server.MapPath("/")))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Không thể thay đổi dữ liệu!");
                    return View(newProduct);
                }
            }
            catch
            {
                ModelState.AddModelError("", "Không thể thay đổi dữ liệu!");
                return View(newProduct);
            }
        }

        // GET: Admin/RamAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            if (RamDataAccessLayer.Delete(id, Server.MapPath("/")))
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Không thể xóa dữ liệu!");
            return RedirectToAction("Index");
        }
    }
}
