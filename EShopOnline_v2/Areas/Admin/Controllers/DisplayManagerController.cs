using System.Web;
using System.Web.Mvc;
using EShopOnline_v2.Areas.Admin.DAL;
using EShopOnline_v2.DAL;

namespace EShopOnline_v2.Areas.Admin.Controllers
{
    public class DisplayManagerController : Controller
    {
        // GET: Admin/DisplayManager
        public ActionResult Index()
        {
            ViewData["data"] = DisplayDataAccessLayer.GetTable();
            ViewBag.UserName = Session["Username"];
            return View();
        }

        // GET: Admin/DisplayManager/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.UserName = Session["Username"];
            return View(DisplayDataAccessLayer.GetItem(id));
        }

        // GET: Admin/DisplayManager/Create
        public ActionResult Create()
        {
            ViewBag.UserName = Session["username"];
            return View();
        }

        // POST: Admin/DisplayManager/Create
        [HttpPost]
        public ActionResult Create(ManHinh newProduct, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            try
            {
                if (DisplayDataAccessLayer.Insert(newProduct, file1, file2, file3, Server.MapPath("/")))
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

        // GET: Admin/DisplayManager/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.UserName = Session["username"];
            return View(DisplayDataAccessLayer.GetItem(id));
        }

        // POST: Admin/DisplayManager/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ManHinh newProduct, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            try
            {
                if (DisplayDataAccessLayer.Update(id, newProduct, file1, file2, file3, Server.MapPath("/")))
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

        // GET: Admin/DisplayManager/Delete/5
        public ActionResult Delete(int id)
        {
            if (DisplayDataAccessLayer.Delete(id, Server.MapPath("/")))
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Không thể xóa dữ liệu!");
            return RedirectToAction("Index");
        }
        
    }
}
