using QLSVNgoaiTru.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace QLSVNgoaiTru.Controllers
{
    public class ChuNTController : Controller
    {
        readonly DbQLSVDataContext db = new DbQLSVDataContext();

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Index()
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {

                TempData["Login"] = "fail";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Room()
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            var cntObj = (chunhatro)Session["LoggedCNT"];
            return View(db.phongtros.ToList().Where(n => n.Machunhatro == cntObj.Machunhatro));
        }

        public ActionResult AddRooms()
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }phongtro pt = new phongtro();
            return View("AddRooms",pt);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddRooms(phongtro pt, HttpPostedFileBase fileupload)
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            //Kiem tra duong dan file
            try {
                if (fileupload == null)
                {
                    ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                    return View();
                }
                //Them vao CSDL
                else
                {
                    if (ModelState.IsValid)
                    {
                        //Luu ten fie, luu y bo sung thu vien using System.IO;
                        var fileName = Path.GetFileName(fileupload.FileName);
                        //Luu duong dan cua file
                        var path = Path.Combine(Server.MapPath("~/images/phongtro/"), fileName);
                        //Kiem tra hình anh ton tai chua?
                        if (System.IO.File.Exists(path))
                            ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                        else
                        {
                            //Luu hinh anh vao duong dan
                            fileupload.SaveAs(path);
                        }
                        pt.Images = fileName;
                        //Luu vao CSDL

                        DateTime date = DateTime.Now;
                        chunhatro cnt = (chunhatro)Session["LoggedCNT"];
                        pt.Machunhatro = cnt.Machunhatro;
                        db.phongtros.InsertOnSubmit(pt);
                        db.SubmitChanges();
                    }
                }
                TempData["message"] = "success";
            }
            catch (Exception ex) { TempData["message"] = "fail"; }
            TempData["message2"] = "add";
            return RedirectToAction("Room");
        }

        public ActionResult EditRoom(int id)
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            return View(db.phongtros.SingleOrDefault(n => n.Maphongtro == id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditRoom(phongtro pt, HttpPostedFileBase fileupload)
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            try { 
            phongtro ptt = db.phongtros.SingleOrDefault(n => n.Maphongtro == pt.Maphongtro);
            chunhatro cnt = (chunhatro)Session["LoggedCNT"];
            ptt.Machunhatro = cnt.Machunhatro;
            ptt.Googlemap = "";
            ptt.Sophong = pt.Sophong;
            ptt.Diachi = pt.Diachi;
            ptt.Tiendatcoc = pt.Tiendatcoc;
            ptt.Noiquyphongtro = pt.Noiquyphongtro;
            ptt.Mota = pt.Mota;
            if (ModelState.IsValid)
            {
                if (fileupload == null)
                {
                    ptt.Images= db.phongtros.SingleOrDefault(n => n.Maphongtro== pt.Maphongtro).Images;
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var fileName = Path.GetFileName(fileupload.FileName);
                        var path = Path.Combine(Server.MapPath("~/images/phongtro"), fileName);
                        if (System.IO.File.Exists(path))
                        {
                            pt.Images= fileName;
                            ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                            return View(pt);
                        }
                        else
                        {
                            fileupload.SaveAs(path);
                            ptt.Images= fileName;
                        }
                    }
                }
                    db.SubmitChanges();
                    TempData["message"] = "success";
                }
            } catch (Exception ex) { TempData["message"] = "fail"; }
             TempData["message2"] = "edit";
            return RedirectToAction("Room");
        }

        public ActionResult DeleteRoom(int id)
        {
            try
            {
                phongtro pt= db.phongtros.SingleOrDefault(m => m.Maphongtro == id);
                db.phongtros.DeleteOnSubmit(pt);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }
            TempData["message2"] = "delete";
            return RedirectToAction("Room");
        }

        public ActionResult CTService()
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            var cntObj = (chunhatro)Session["LoggedCNT"];
            return View(db.ctdvs.ToList().Where(n => n.Macnt == cntObj.Machunhatro));
        }


        public ActionResult AddCTService()
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.loaidv = db.dichvus.ToList().OrderBy(n => n.MaloaiDV);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddCTService(ctdv ct, HttpPostedFileBase fileupload)
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            try { 
                chunhatro cnt = (chunhatro)Session["LoggedCNT"];
                ct.Macnt = cnt.Machunhatro;
                db.ctdvs.InsertOnSubmit(ct);
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch(Exception ex)
            {
                TempData["message"] = "fail";
            }
            TempData["message2"] = "add";
            return RedirectToAction("CTService");
        }

        public ActionResult EditCTService(int id)
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.loaidv = db.dichvus.ToList().OrderBy(n => n.MaloaiDV);
            return View(db.ctdvs.SingleOrDefault(n => n.MaCTDV == id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditCTService(ctdv ct)
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            try { 
                ctdv ctt = db.ctdvs.SingleOrDefault(n => n.MaCTDV== ct.MaCTDV);
                ctt.MaloaiDV= ct.MaloaiDV;
                ctt.Dongia = ct.Dongia;
                ctt.HinhThucThanhToan = ct.HinhThucThanhToan;
                ctt.ThoiGianThanhToan = ct.ThoiGianThanhToan;
                ctt.Macnt = ct.Macnt;

                db.SubmitChanges();

                TempData["message"] = "success";
            }catch (Exception ex)
            {
                TempData["message"] = "fail";
            }
            TempData["message2"] = "edit";
            return RedirectToAction("CTService");
        }

        public ActionResult DeleteCTService(int id)
        {
            try { 
                ctdv ct= db.ctdvs.SingleOrDefault(m => m.MaCTDV== id);
                db.ctdvs.DeleteOnSubmit(ct);
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }
            TempData["message2"] = "delete";
            return RedirectToAction("CTService");
        }

        public ActionResult PhieuDongTienPhong()
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            return View(db.phieudongtienphongs.ToList());
        }

        public ActionResult AddPhieuDongTienPhong()
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            chunhatro cnt = (chunhatro)Session["LoggedCNT"];
            ViewBag.Phong = db.phongtros.ToList().OrderBy(n => n.Sophong);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddPhieuDongTienPhong(phieudongtienphong dtp)
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                chunhatro cnt = (chunhatro)Session["LoggedCNT"];
                dtp.Machunhatro = cnt.Machunhatro;
                db.phieudongtienphongs.InsertOnSubmit(dtp);
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }
            TempData["message2"] = "add";
            return RedirectToAction("PhieuDongTienPhong");
        }

        public ActionResult SuaPhieuDongTienPhong(int id)
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            chunhatro cnt = (chunhatro)Session["LoggedCNT"];
            ViewBag.Phong = db.phongtros.ToList().OrderBy(n => n.Sophong);
            return View(db.phieudongtienphongs.SingleOrDefault(n => n.Maphieudongtienphong == id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaPhieuDongTienPhong(phieudongtienphong dtp)
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                phieudongtienphong dtpp = db.phieudongtienphongs.SingleOrDefault(n => n.Maphieudongtienphong == dtp.Maphieudongtienphong);
                dtpp.Maphongtro = dtp.Maphongtro;
                dtpp.Ngaylap = dtp.Ngaylap;
                dtpp.SoTien = dtp.SoTien;
                dtpp.Machunhatro = dtp.Machunhatro;

                db.SubmitChanges();

                TempData["message"] = "success";
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }
            TempData["message2"] = "edit";
            return RedirectToAction("PhieuDongTienPhong");
        }

        public ActionResult XoaPhieuDongTienPhong(int id)
        {
            try
            {
                phieudongtienphong dtp = db.phieudongtienphongs.SingleOrDefault(m => m.Maphieudongtienphong== id);
                db.phieudongtienphongs.DeleteOnSubmit(dtp);
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }
            TempData["message2"] = "delete";
            return RedirectToAction("PhieuDongTienPhong");
        }

        public ActionResult BienBanViPham()
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            return View(db.bienbanviphams.ToList());
        }

        public ActionResult ThemBienBanViPham()
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.loaivipham = db.loaivps.ToList().OrderBy(n => n.TenloaiVP);
            return View();
        }

        public ActionResult CTBienBanViPham(int id)
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            return View(db.bienbanviphams.SingleOrDefault(n => n.Mabienbanvipham == id));
        }

        public ActionResult SuaBienBanViPham(int id)
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.loaivipham = db.loaivps.ToList().OrderBy(n => n.TenloaiVP);
            return View(db.bienbanviphams.SingleOrDefault(n => n.Mabienbanvipham == id));
        }

        public ActionResult XoaBienBanViPham(int id)
        {
            try
            {
                bienbanvipham bbvp = db.bienbanviphams.SingleOrDefault(m => m.Mabienbanvipham == id);
                db.bienbanviphams.DeleteOnSubmit(bbvp);
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }

            TempData["message2"] = "delete";
            return RedirectToAction("Chunhatro");
        }

        public ActionResult BienBanXuLiViPham()
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            var cntObj = (chunhatro)Session["LoggedCNT"];
            return View(db.bienbanxuliviphams.ToList());
        }


        public ActionResult ThemBienBanXuLiViPham()
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemBienBanXuLiViPham(bienbanxulivipham xlvp)
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            try { 
            db.bienbanxuliviphams.InsertOnSubmit(xlvp);
            db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }
            TempData["message2"] = "add";
            return RedirectToAction("BienBanXuLiViPham");
        }

        public ActionResult SuaBienBanXuLiViPham(int id)
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            return View(db.bienbanxuliviphams.SingleOrDefault(n => n.Mabienbanxuly == id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaBienBanXuLiViPham(bienbanxulivipham xlvp)
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            try { 
            bienbanxulivipham xlvpp = db.bienbanxuliviphams.SingleOrDefault(n => n.Mabienbanxuly == xlvp.Mabienbanxuly);
            xlvpp.Noidung = xlvp.Noidung;
            db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }
            TempData["message2"] = "edit";
            return RedirectToAction("BienBanXuLiViPham");
        }

        public ActionResult XoaBienBanXuLiViPham(int id)
        {
            try { 
            bienbanxulivipham xlvp = db.bienbanxuliviphams.SingleOrDefault(m => m.Mabienbanxuly == id);
            db.bienbanxuliviphams.DeleteOnSubmit(xlvp);
            db.SubmitChanges(); TempData["message"] = "success";
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }
            TempData["message2"] = "delete";
            return RedirectToAction("BienBanXuLiViPham");
        }
    }
}