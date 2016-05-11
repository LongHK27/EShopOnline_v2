using System.Web.Mvc;
using EShopOnline_v2.Areas.Admin.DAL;
using EShopOnline_v2.DAL;

namespace EShopOnline_v2.Areas.Admin.Controllers
{
    public class AccountManagerController : Controller
    {
        // GET: Admin/AccountManager
        public ActionResult Index()
        {
            ViewData["data"] = AccountDataAccessLayer.GetTable();
            ViewBag.UserName = Session["Username"];
            return View();
        }

        // GET: Admin/AccountManager/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.UserName = Session["Username"];
            return View(AccountDataAccessLayer.GetItem(id));
        }

        // GET: Admin/AccountManager/Create
        public ActionResult Create()
        {
            ViewBag.UserName = Session["Username"];
            return View();
        }

        // POST: Admin/AccountManager/Create
        [HttpPost]
        public ActionResult Create(TaiKhoan newAccount)
        {
            try
            {
                if (AccountDataAccessLayer.Insert(newAccount))
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

        // GET: Admin/AccountManager/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.UserName = Session["username"];
            return View(AccountDataAccessLayer.GetItem(id));
        }

        // POST: Admin/AccountManager/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TaiKhoan account)
        {
            try
            {
                if (AccountDataAccessLayer.Update(id, account))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Không thể thay đổi dữ liệu!");
                    return View(account);
                }
            }
            catch
            {
                ModelState.AddModelError("", "Không thể thay đổi dữ liệu!");
                return View(account);
            }
        }

        // GET: Admin/AccountManager/Delete/5
        public ActionResult Delete(int id)
        {
            if (AccountDataAccessLayer.Delete(id))
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Không thể xóa dữ liệu!");
            return RedirectToAction("Index");
        }

    }
}
