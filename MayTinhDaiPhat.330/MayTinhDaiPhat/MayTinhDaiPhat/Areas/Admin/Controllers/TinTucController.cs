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
    public class TinTucController : Controller
    {
        private DataContext db = new DataContext();
        private readonly TinTucDAO dao = new TinTucDAO();

        // GET: /Admin/TinTuc/
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var list = dao.DanhSachTinTuc(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(list);
        }

        // GET: /Admin/TinTuc/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tintuc = dao.XemTinTuc(id);
            if (tintuc == null)
            {
                return HttpNotFound();
            }
            return View(tintuc);
        }

        // GET: /Admin/TinTuc/Create
        public ActionResult Create()
        {
            ViewBag.MaDM = new SelectList(db.DanhMuc, "MaDM", "TenDM");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( TinTuc tintuc)
        {
            if (ModelState.IsValid)
            {
                var result = dao.ThemTinTuc(tintuc);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Lỗi! vui lòng kiểm tra lại");
                }
            }

            ViewBag.MaDM = new SelectList(db.DanhMuc, "MaDM", "TenDM", tintuc.MaDM);
            return View(tintuc);
        }

        // GET: /Admin/TinTuc/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tintuc = dao.XemTinTuc(id);
            if (tintuc == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDM = new SelectList(db.DanhMuc, "MaDM", "TenDM", tintuc.MaDM);
            return View(tintuc);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( TinTuc tintuc)
        {
            if (ModelState.IsValid)
            {
                var result = dao.SuaTinTuc(tintuc);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            ViewBag.MaDM = new SelectList(db.DanhMuc, "MaDM", "TenDM", tintuc.MaDM);
            return View(tintuc);
        }

        // GET: /Admin/TinTuc/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = dao.XoaTinTuc(id);
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
