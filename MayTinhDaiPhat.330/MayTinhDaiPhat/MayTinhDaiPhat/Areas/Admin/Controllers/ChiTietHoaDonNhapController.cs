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
    public class ChiTietHoaDonNhapController : Controller
    {
        private DataContext db = new DataContext();
        private readonly ChiTietHoaDonNhapDAO dao = new ChiTietHoaDonNhapDAO();
        // GET: /Admin/ChiTietHoaDonNhap/
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var list = dao.DanhSachChiTietHoaDonNhap(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(list);
        }

        // GET: /Admin/ChiTietHoaDonNhap/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDonNhap chitiethoadonnhap = db.ChiTietHoaDonNhap.Find(id);
            if (chitiethoadonnhap == null)
            {
                return HttpNotFound();
            }
            return View(chitiethoadonnhap);
        }

        // GET: /Admin/ChiTietHoaDonNhap/Create
        public ActionResult Create()
        {
            SetViewBagMaHDN();
            SetViewBagSanPham();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( ChiTietHoaDonNhap chitiethoadonnhap)
        {
            if (ModelState.IsValid)
            {
                var result = dao.ThemChiTietHoaDonNhap(chitiethoadonnhap);
                if(result>0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Lỗi! vui lòng kiểm tra lại");
                }
            }

            SetViewBagMaHDN();
            SetViewBagSanPham();
            return View(chitiethoadonnhap);
        }

        // GET: /Admin/ChiTietHoaDonNhap/Edit/5
        public ActionResult Edit(int id)
        {
            SetViewBagMaHDN(id);
            SetViewBagSanPham(id);
            return View(dao.XemChiTiet(id));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ChiTietHoaDonNhap chitiethoadonnhap)
        {
            if (ModelState.IsValid)
            {
                var result = dao.CapNhatChiTietHoaDon(chitiethoadonnhap);
                if(result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            SetViewBagMaHDN();
            SetViewBagSanPham();
            
            return View(chitiethoadonnhap);
        }


        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var delete = new ChiTietHoaDonNhapDAO().XoaChiTietHoaDonNhap(id);
            return RedirectToAction("Index");
        }

        public void SetViewBagMaHDN(int? selectedId = null)
        {
            var hdn = new HoaDonNhapDAO();
            ViewBag.HDN = new SelectList(hdn.DanhSach(), "ID", "Name", selectedId);
        }

        public void SetViewBagSanPham(int? selectedId = null)
        {
            var sp = new SanPhamDAO();
            ViewBag.SP = new SelectList(sp.DanhSach(), "ID", "TenSP", selectedId);
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
