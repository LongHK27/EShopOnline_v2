using System.Web;
using System.Web.Mvc;
using EShopOnline_v2.Areas.Admin.DAL;
using EShopOnline_v2.Models;

namespace EShopOnline_v2.Areas.Admin.Controllers
{
    public class ComputerManagerController : Controller
    {
        // GET: Admin/ComputerManager
        public ActionResult Index()
        {
            ViewBag.computer = ComputerDataAccessLayer.GetTable();
            ViewBag.UserName = Session["Username"];
            return View();
        }

        // GET: Admin/ComputerManager/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.UserName = Session["Username"];
            return View(ComputerDataAccessLayer.GetItem(id));
        }

        // GET: Admin/ComputerManager/Create
        public ActionResult Create()
        {
            ViewBag.UserName = Session["username"];
            ViewBag.cpu = CpuDataAccessLayer.GetTable();
            ViewBag.ram = RamDataAccessLayer.GetTable();
            ViewBag.stroage = StorageDataAccessLayer.GetTable();
            ViewBag.vga = CardVgaDataAccessLayer.GetTable();
            return View();
        }

        // POST: Admin/ComputerManager/Create
        [HttpPost]
        public ActionResult Create(ComputerModel newProduct, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            try
            {
                if (ComputerDataAccessLayer.Insert(newProduct, file1, file2, file3, Server.MapPath("/")))
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

        // GET: Admin/ComputerManager/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.UserName = Session["username"];
            ViewBag.cpu = CpuDataAccessLayer.GetTable();
            ViewBag.ram = RamDataAccessLayer.GetTable();
            ViewBag.stroage = StorageDataAccessLayer.GetTable();
            ViewBag.vga = CardVgaDataAccessLayer.GetTable();
            return View(ComputerDataAccessLayer.GetItem(id));
        }

        // POST: Admin/ComputerManager/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ComputerModel newProduct, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            try
            {
                if (ComputerDataAccessLayer.Update(id, newProduct, file1, file2, file3, Server.MapPath("/")))
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

        // GET: Admin/ComputerManager/Delete/5
        public ActionResult Delete(int id)
        {
            if (ComputerDataAccessLayer.Delete(id, Server.MapPath("/")))
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Không thể xóa dữ liệu!");
            return RedirectToAction("Index");
        }

    }
}
