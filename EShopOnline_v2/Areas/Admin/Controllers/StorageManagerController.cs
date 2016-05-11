using System.Web;
using System.Web.Mvc;
using EShopOnline_v2.Areas.Admin.DAL;
using EShopOnline_v2.DAL;

namespace EShopOnline_v2.Areas.Admin.Controllers
{
    public class StorageManagerController : Controller
    {
        // GET: Admin/StorageManager
        public ActionResult Index()
        {
            ViewData["data"] = StorageDataAccessLayer.GetTable();
            ViewBag.UserName = Session["Username"];
            return View();
        }

        // GET: Admin/StorageManager/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.UserName = Session["Username"];
            return View(StorageDataAccessLayer.GetItem(id));
        }

        // GET: Admin/StorageManager/Create
        public ActionResult Create()
        {
            ViewBag.UserName = Session["username"];
            return View();
        }

        // POST: Admin/StorageManager/Create
        [HttpPost]
        public ActionResult Create(OCung newProduct, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            try
            {
                if (StorageDataAccessLayer.Insert(newProduct, file1, file2, file3, Server.MapPath("/")))
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

        // GET: Admin/StorageManager/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.UserName = Session["username"];
            return View(StorageDataAccessLayer.GetItem(id));
        }

        // POST: Admin/StorageManager/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, OCung newProduct, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            try
            {
                if (StorageDataAccessLayer.Update(id, newProduct, file1, file2, file3, Server.MapPath("/")))
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

        // GET: Admin/StorageManager/Delete/5
        public ActionResult Delete(int id)
        {
            if (StorageDataAccessLayer.Delete(id, Server.MapPath("/")))
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Không thể xóa dữ liệu!");
            return RedirectToAction("Index");
        }

    }
}
