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
    public class HinhAnhController : Controller
    {
        private DataContext db = new DataContext();
        private readonly HinhAnhDAO dao = new HinhAnhDAO();
        // GET: /Admin/HinhAnh/
        //public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        //{
        //    var list = dao.DanhSachHinhAnh(searchString, page, pageSize);
        //    ViewBag.searchString = searchString;
        //    return View(list);
        //}
        public ActionResult Index()
        {
            var list = dao.DanhSach();
            return View(list);
        }
        // GET: /Admin/HinhAnh/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhAnh hinhanh = dao.XemHinhAnh(id);
            if (hinhanh == null)
            {
                return HttpNotFound();
            }
            return View(hinhanh);
        }

        // GET: /Admin/HinhAnh/Create
        public ActionResult Create()
        {
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( HinhAnh hinhanh)
        {
            if (ModelState.IsValid)
            {
                var result = dao.ThemHinhAnh(hinhanh);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Lỗi! vui lòng kiểm tra lại");
                }
            }

            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP", hinhanh.MaSP);
            return View(hinhanh);
        }

        // GET: /Admin/HinhAnh/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhAnh hinhanh = dao.XemHinhAnh(id);
            if (hinhanh == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP", hinhanh.MaSP);
            return View(hinhanh);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( HinhAnh hinhanh)
        {
            if (ModelState.IsValid)
            {
                var result = dao.SuaHinhAnh(hinhanh);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP", hinhanh.MaSP);
            return View(hinhanh);
        }

        public ActionResult Delete(int id)
        {
            var result = dao.XemHinhAnh(id);
            return View(result);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DelSeteAction(int id)
        {
            var result = dao.XoaHinhAnh(id);
            return RedirectToAction("Index");
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
