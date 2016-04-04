﻿using System;
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
    public class HoaDonXuatController : Controller
    {
        private DataContext db = new DataContext();
        private readonly HoaDonXuatDAO dao = new HoaDonXuatDAO();
        // GET: /Admin/HoaDonXuat/
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var list = dao.DanhSachHoaDonXuat(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(list);
        }

        // GET: /Admin/HoaDonXuat/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonXuat hoadonxuat = dao.XemHoaDonXuat(id);
            if (hoadonxuat == null)
            {
                return HttpNotFound();
            }
            return View(hoadonxuat);
        }

        // GET: /Admin/HoaDonXuat/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KhachHang, "MaKH", "TenKH");
            ViewBag.MaNV = new SelectList(db.NhanVien, "MaNV", "TenNV");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( HoaDonXuat hoadonxuat)
        {
            if (ModelState.IsValid)
            {
                var result = dao.ThemHoaDonXuat(hoadonxuat);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Lỗi! vui lòng kiểm tra lại");
                }
            }

            ViewBag.MaKH = new SelectList(db.KhachHang, "MaKH", "TenKH", hoadonxuat.MaKH);
            ViewBag.MaNV = new SelectList(db.NhanVien, "MaNV", "TenNV", hoadonxuat.MaNV);
            return View(hoadonxuat);
        }

        // GET: /Admin/HoaDonXuat/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonXuat hoadonxuat = dao.XemHoaDonXuat(id);
            if (hoadonxuat == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KhachHang, "MaKH", "TenKH", hoadonxuat.MaKH);
            ViewBag.MaNV = new SelectList(db.NhanVien, "MaNV", "TenNV", hoadonxuat.MaNV);
            return View(hoadonxuat);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( HoaDonXuat hoadonxuat)
        {
            if (ModelState.IsValid)
            {
                var result = dao.SuaHoaDonXuat(hoadonxuat);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            ViewBag.MaKH = new SelectList(db.KhachHang, "MaKH", "TenKH", hoadonxuat.MaKH);
            ViewBag.MaNV = new SelectList(db.NhanVien, "MaNV", "TenNV", hoadonxuat.MaNV);
            return View(hoadonxuat);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = dao.XemHoaDonXuat(id);
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