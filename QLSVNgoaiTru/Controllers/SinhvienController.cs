using QLSVNgoaiTru.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLSVNgoaiTru.Controllers
{
    public class SinhvienController : Controller
    {
        // GET: Sinhvien
        readonly DbQLSVDataContext db = new DbQLSVDataContext();


        public ActionResult Dkngoaitru()
        {
            if (Session["LoggedSV"] == null || Session["LoggedSV"].ToString() == "")
            {
                TempData["Login"] = "fail";
                return RedirectToAction("Index", "Home");
            }
            sinhvien sv = (sinhvien)Session["LoggedSV"];
            ViewBag.sinhvien = sv.Tensv;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Dkngoaitru(phieudangkingoaitru dknt)
        {
            if (Session["LoggedSV"] == null || Session["LoggedSV"].ToString() == "")
            {

                TempData["Login"] = "fail";
                return RedirectToAction("Index", "Home");
            }
            try
            {
                sinhvien sv = (sinhvien)Session["LoggedSV"];
                ViewBag.sinhvien = sv.Tensv;
                DateTime date = DateTime.Now;
                dknt.Masv = sv.Masv;
                dknt.TrangThai = 0;
                dknt.Ngaylapphieu = date;
                db.phieudangkingoaitrus.InsertOnSubmit(dknt);
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }

            TempData["message2"] = "add";
            return RedirectToAction("DanhSachPhieu");
        }

        public ActionResult Dktimphongtro()
        {
            if (Session["LoggedSV"] == null || Session["LoggedSV"].ToString() == "")
            {
                TempData["Login"] = "fail";
                return RedirectToAction("Index", "Home");
            }
            sinhvien sv = (sinhvien)Session["LoggedSV"];
            ViewBag.sinhvien = sv.Tensv;
            ViewBag.quanhuyen = db.quanhuyens.ToList().OrderBy(n => n.Tenquanhuyen);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Dktimphongtro(phieudangkitimphongtro dktpt)
        {
            if (Session["LoggedSV"] == null || Session["LoggedSV"].ToString() == "")
            {
                TempData["Login"] = "fail";
                return RedirectToAction("Index", "Home");
            }
            try
            {
                sinhvien sv = (sinhvien)Session["LoggedSV"];
                ViewBag.sinhvien = sv.Tensv;
                DateTime date = DateTime.Now;
                dktpt.Masv = sv.Masv;
                dktpt.TrangThai = 0;
                dktpt.Ngaylap = date;
                db.phieudangkitimphongtros.InsertOnSubmit(dktpt);
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }

            TempData["message2"] = "add";
            return RedirectToAction("DanhSachPhieu");
        }

        public ActionResult ThemDkthaydoidiachi()
        {
            if (Session["LoggedSV"] == null || Session["LoggedSV"].ToString() == "")
            {
                TempData["Login"] = "fail";
                return RedirectToAction("Index", "Home");
            }
            sinhvien sv = (sinhvien)Session["LoggedSV"];
            ViewBag.sinhvien = sv.Tensv;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemDkthaydoidiachi(phieuthaydoidiachi  tddc)
        {
            if (Session["LoggedSV"] == null || Session["LoggedSV"].ToString() == "")
            {
                TempData["Login"] = "fail";
                return RedirectToAction("Index", "Home");
            }
            try
            {
                sinhvien sv = (sinhvien)Session["LoggedSV"];
                ViewBag.sinhvien = sv.Tensv;
                tddc.Masv = sv.Masv;
                tddc.TrangThai = 0;
                db.phieuthaydoidiachis.InsertOnSubmit(tddc);
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }

            TempData["message2"] = "add";
            return RedirectToAction("DanhSachPhieu");
        }
        
        public ActionResult PhieuXacNhanThongTinNgoaiTru()
        {
            if (Session["LoggedSV"] == null || Session["LoggedSV"].ToString() == "")
            {
                TempData["Login"] = "fail";
                return RedirectToAction("Index", "Home");
            }
            sinhvien sv = (sinhvien)Session["LoggedSV"];
            ViewBag.sinhvien = sv.Tensv;
            ViewBag.quanhuyen = db.quanhuyens.ToList().OrderBy(n => n.Tenquanhuyen);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PhieuXacNhanThongTinNgoaiTru(phieuxacnhanthongtinngoaitru ttnt)
        {
            if (Session["LoggedSV"] == null || Session["LoggedSV"].ToString() == "")
            {
                TempData["Login"] = "fail";
                return RedirectToAction("Index", "Home");
            }
            try
            {
                sinhvien sv = (sinhvien)Session["LoggedSV"];
                ViewBag.sinhvien = sv.Tensv;
                DateTime date = DateTime.Now;
                ttnt.Masv = sv.Masv;
                ttnt.TrangThai = 0;
                ttnt.Nhanxetsinhvien = "";
                ttnt.Ngaylap = date;
                db.phieuxacnhanthongtinngoaitrus.InsertOnSubmit(ttnt);
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }

            TempData["message2"] = "add";
            return RedirectToAction("DanhSachPhieu");
        }
        
        public ActionResult PhieuGioiThieuSinhVienNgoaiTru()
        {
            if (Session["LoggedSV"] == null || Session["LoggedSV"].ToString() == "")
            {
                TempData["Login"] = "fail";
                return RedirectToAction("Index", "Home");
            }
            try
            {
                sinhvien sv = (sinhvien)Session["LoggedSV"];
                ViewBag.sinhvien = sv.Tensv;
                DateTime date = DateTime.Now;
                phieugioithieusinhvienngoaitru gtsvnt = new phieugioithieusinhvienngoaitru();
                gtsvnt.Masv = sv.Masv;
                gtsvnt.TrangThai = 0;
                gtsvnt.Ngaylap = date;
                db.phieugioithieusinhvienngoaitrus.InsertOnSubmit(gtsvnt);
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }

            TempData["message2"] = "add";
            return RedirectToAction("DanhSachPhieu");
        }

        public ActionResult DanhSachPhieu()
        {
            if (Session["LoggedSV"] == null || Session["LoggedSV"].ToString() == "")
            {
                TempData["Login"] = "fail";
                return RedirectToAction("Index", "Home");
            }
            sinhvien sv = (sinhvien)Session["LoggedSV"];
            ViewBag.sinhvien = sv.Tensv;
            DanhSachPhieu dsp = new DanhSachPhieu();
            dsp.dktpt = db.phieudangkitimphongtros.Where(m => m.Masv == sv.Masv).ToList();
            dsp.tddc = db.phieuthaydoidiachis.Where(m => m.Masv == sv.Masv).ToList();
            dsp.xnttnt = db.phieuxacnhanthongtinngoaitrus.Where(m => m.Masv == sv.Masv).ToList();
            dsp.dknt = db.phieudangkingoaitrus.Where(m => m.Masv == sv.Masv).ToList();
            dsp.gtsvnt = db.phieugioithieusinhvienngoaitrus.Where(m => m.Masv == sv.Masv).ToList();
            ViewBag.danhsachphieu = dsp;
            return View();
        }

    }
}