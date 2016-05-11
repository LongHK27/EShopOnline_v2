using System.Web;
using System.Web.Mvc;
using EShopOnline_v2.Areas.Admin.DAL;
using EShopOnline_v2.DAL;

namespace EShopOnline_v2.Areas.Admin.Controllers
{
    public class CpuManagerController : Controller
    {
        // GET: Admin/CpuAdmin
        public ActionResult Index()
        {
            ViewData["data"] = CpuDataAccessLayer.GetTable();
            ViewBag.UserName = Session["Username"];
            return View();
        }

        // GET: Admin/CpuAdmin/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.UserName = Session["Username"];
            return View(CpuDataAccessLayer.GetItem(id));
        }

        // GET: Admin/CpuAdmin/Create
        public ActionResult Create()
        {
            ViewBag.UserName = Session["username"];
            return View();
        }

        // POST: Admin/CpuAdmin/Create
        [HttpPost]
        public ActionResult Create(CPU newProduct, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            try
            {
                if (CpuDataAccessLayer.Insert(newProduct, file1, file2, file3, Server.MapPath("/")))
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

        // GET: Admin/CpuAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.UserName = Session["username"];
            return View(CpuDataAccessLayer.GetItem(id));
        }

        // POST: Admin/CpuAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CPU newProduct, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            try
            {
                if (CpuDataAccessLayer.Update(id, newProduct, file1, file2, file3, Server.MapPath("/")))
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

        // GET: Admin/CpuAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            if (CpuDataAccessLayer.Delete(id, Server.MapPath("/")))
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Không thể xóa dữ liệu!");
            return RedirectToAction("Index");
        }

    }
}
