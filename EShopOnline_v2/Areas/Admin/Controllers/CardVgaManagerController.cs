using System.Web;
using System.Web.Mvc;
using EShopOnline_v2.Areas.Admin.DAL;
using EShopOnline_v2.DAL;

namespace EShopOnline_v2.Areas.Admin.Controllers
{
    public class CardVgaManagerController : Controller
    {
        // GET: Admin/CardVgaManager
        public ActionResult Index()
        {
            ViewData["data"] = CardVgaDataAccessLayer.GetTable();
            ViewBag.UserName = Session["Username"];
            return View();
        }

        // GET: Admin/CardVgaManager/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.UserName = Session["Username"];
            return View(CardVgaDataAccessLayer.GetItem(id));
        }

        // GET: Admin/CardVgaManager/Create
        public ActionResult Create()
        {
            ViewBag.UserName = Session["username"];
            return View();
        }

        // POST: Admin/CardVgaManager/Create
        [HttpPost]
        public ActionResult Create(CardVGA newProduct, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            try
            {
                if (CardVgaDataAccessLayer.Insert(newProduct, file1, file2, file3, Server.MapPath("/")))
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

        // GET: Admin/CardVgaManager/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.UserName = Session["username"];
            return View(CardVgaDataAccessLayer.GetItem(id));
        }

        // POST: Admin/CardVgaManager/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CardVGA newProduct, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            try
            {
                if (CardVgaDataAccessLayer.Update(id, newProduct, file1, file2, file3, Server.MapPath("/")))
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

        // GET: Admin/CardVgaManager/Delete/5
        public ActionResult Delete(int id)
        {
            if (CardVgaDataAccessLayer.Delete(id, Server.MapPath("/")))
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Không thể xóa dữ liệu!");
            return RedirectToAction("Index");
        }

    }
}
