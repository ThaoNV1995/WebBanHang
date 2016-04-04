using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MayTinhDaiPhat.Models;
using NhapXuat.DAO;

namespace MayTinhDaiPhat.Areas.Admin.Controllers
{
    public class DanhMucController : Controller
    {
        private DataContext db = new DataContext();
        private readonly DanhMucDAO dao = new DanhMucDAO();

        // GET: /Admin/DanhMuc/
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var list = dao.DanhSachDanhMuc(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(list);
        }

        // GET: /Admin/DanhMuc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMuc danhmuc = db.DanhMuc.Find(id);
            if (danhmuc == null)
            {
                return HttpNotFound();
            }
            return View(danhmuc);
        }

        // GET: /Admin/DanhMuc/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( DanhMuc danhmuc)
        {
            if (ModelState.IsValid)
            {
                var result = dao.ThemDanhMuc(danhmuc);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Lỗi! vui lòng kiểm tra lại");
                }
            }

            return View(danhmuc);
        }

        // GET: /Admin/DanhMuc/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMuc danhmuc = dao.XemDanhMuc(id);
            if (danhmuc == null)
            {
                return HttpNotFound();
            }
            return View(danhmuc);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( DanhMuc danhmuc)
        {
            if (ModelState.IsValid)
            {
                var result = dao.SuaDanhMuc(danhmuc);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View(danhmuc);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = dao.XoaDanhMuc(id);
            return View("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
