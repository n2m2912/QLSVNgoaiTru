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
        readonly DbQLSVDataContext Db = new DbQLSVDataContext();
        public ActionResult Index()
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
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
            return View(Db.phongtros.ToList().Where(n => n.Machunhatro == cntObj.Machunhatro));
        }

        [HttpGet]
        public ActionResult AddRoom()
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AddRoom(phongtro pt, HttpPostedFileBase fileupload)
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            if (fileupload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }

            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/images"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                        return View();
                    }
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    pt.Images = fileName;
                    Db.phongtros.InsertOnSubmit(pt);
                    Db.SubmitChanges();
                }
                return RedirectToAction("Room");
            }
        }


        public ActionResult EditRoom(int id)
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }

            phongtro pt = new phongtro();
             pt = Db.phongtros.SingleOrDefault(n => n.Maphongtro == id);
            if (pt == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(pt);
        }

        [HttpGet]
        public string getImage(int id)
        {
            return Db.phongtros.SingleOrDefault(n => n.Maphongtro == id).Images;
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditRoom(phongtro pt, HttpPostedFileBase fileUpload)
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }

            phongtro ptt = Db.phongtros.SingleOrDefault(n => n.Maphongtro == pt.Maphongtro);

            var TenPhong = pt.Tenphong;
            var TienDatCoc = pt.Tiendatcoc;
            var MoTa = pt.Mota;
            var SoPhong = pt.Sophong;
            var NoiQuyPhongTro = pt.Noiquyphongtro;
            var MaChuNhaTro = pt.Machunhatro;
            var GoogleMap = pt.Googlemap;

            var cnt = (chunhatro)Session["LoggedCNT"];

            ptt.Tiendatcoc = TienDatCoc;
            ptt.Sophong = SoPhong;
            ptt.Noiquyphongtro = NoiQuyPhongTro;
            ptt.Machunhatro = cnt.Machunhatro;
            ptt.Googlemap = GoogleMap;
            ptt.Tenphong = TenPhong;
            ptt.Mota = MoTa;

            if (ModelState.IsValid)
            {
                if (fileUpload == null)
                {
                    ptt.Images = getImage(pt.Maphongtro);
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var fileName = Path.GetFileName(fileUpload.FileName);
                        var path = Path.Combine(Server.MapPath("~/images"), fileName);
                        if (System.IO.File.Exists(path))
                        {
                            pt.Images = fileName;
                            ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                            return View(pt);
                        }
                        else
                        {
                            fileUpload.SaveAs(path);
                            ptt.Images = fileName;
                        }
                    }
                }
                Db.SubmitChanges();
            }
            return RedirectToAction("Room");
        }

        public ActionResult Service()
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            var cntObj = (chunhatro)Session["LoggedCNT"];
            return View(Db.dichvus.ToList().Where(n => n.Machunhatro == cntObj.Machunhatro));
        }
        public ActionResult AddService()
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AddService(dichvu dv)
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            var cnt = (chunhatro)Session["LoggedCNT"];
            dv.Machunhatro = cnt.Machunhatro;
            Db.dichvus.InsertOnSubmit(dv);
            Db.SubmitChanges();
            return RedirectToAction("Service");
        }
        public ActionResult EditService(int id)
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            dichvu dv = new dichvu();
            dv = Db.dichvus.SingleOrDefault(n => n.MaloaiDV == id);
            return View(dv);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditService(dichvu dv)
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            dichvu dvv = Db.dichvus.SingleOrDefault(n => n.MaloaiDV == dv.MaloaiDV);

            var TenloaiDV = dv.TenloaiDV;
            var Donvitinh =  dv.Donvitinh;
            var Dongia = dv.Dongia;
            var Hinhthucthanhtoan = dv.Hinhthucthanhtoan;
            var Thoidiemthanhtoan = dv.Thoidiemthanhtoan;

            var cnt = (chunhatro)Session["LoggedCNT"];

            dvv.TenloaiDV = TenloaiDV;
            dvv.Donvitinh = Donvitinh;
            dvv.Dongia = Dongia;
            dvv.Hinhthucthanhtoan = Hinhthucthanhtoan;
            dvv.Thoidiemthanhtoan = Thoidiemthanhtoan;
            dvv.Machunhatro = cnt.Machunhatro;
            Db.SubmitChanges();
            return RedirectToAction("Service");
        }

        
        public ActionResult DeleteServiceByID(int id)
        {
            dichvu dv = Db.dichvus.SingleOrDefault(n => n.MaloaiDV == id);
            return View(dv);
        }


        [HttpPost, ActionName("DeleteServiceByID")]
        public ActionResult AcceptDeleteServiceByID(int id)
        {
            dichvu dvv= Db.dichvus.SingleOrDefault(n => n.MaloaiDV == id);
            Db.dichvus.DeleteOnSubmit(dvv);
            Db.SubmitChanges();
            return RedirectToAction("Service");
        }

        public ActionResult DeleteAllService()
        {
            return RedirectToAction("Service");
        }

        public ActionResult PhieuDongTienPhong()
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            var cntObj = (chunhatro)Session["LoggedCNT"];
            return View(Db.phieudongtienphongs.ToList().Where(n => n.Machunhatro == cntObj.Machunhatro));
        }

        public ActionResult AddPhieuDongTienPhong()
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }

            var cntObj = (chunhatro)Session["LoggedCNT"];
            ViewBag.Maphongtro = new SelectList(Db.phongtros.ToList().Where(m => m.Machunhatro==cntObj.Machunhatro).OrderBy(n => n.Tenphong), "Maphongtro", "Tenphong");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AddPhieuDongTienPhong(phieudongtienphong pdtp)
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            var cnt = (chunhatro)Session["LoggedCNT"];
            pdtp.Machunhatro = cnt.Machunhatro;
            pdtp.Ngaylapphieu = DateTime.Now;
            Db.phieudongtienphongs.InsertOnSubmit(pdtp);
            Db.SubmitChanges();
            return RedirectToAction("PhieuDongTienPhong");
        }


        public ActionResult EditDongTienPhong(int id)
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }

            var cntObj = (chunhatro)Session["LoggedCNT"];
            return View(Db.phieudongtienphongs.ToList().Where(n => n.Maphieudongtienphong == id));
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditDongTienPhong(phieudongtienphong dtp)
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            phieudongtienphong dtpp = Db.phieudongtienphongs.SingleOrDefault(n => n.Maphieudongtienphong == dtp.Maphieudongtienphong);

            var Ngaylapphieu = dtp.Ngaylapphieu;
            var Doituonguutien = dtp.Doituonguutien;
            var SDTgiadinh = dtp.SDTgiadinh;
            var Maphongtro = dtp.Maphongtro;

            var cnt = (chunhatro)Session["LoggedCNT"];

            dtpp.Ngaylapphieu = Ngaylapphieu;
            dtpp.Doituonguutien = Doituonguutien;
            dtpp.SDTgiadinh = SDTgiadinh;
            dtpp.Machunhatro = cnt.Machunhatro;
            dtpp.Maphongtro = Maphongtro;
            Db.SubmitChanges();
            return RedirectToAction("DongTienPhong");
        }

        public ActionResult UsernamePartial()
        {
            if (Session["LoggedCNT"] != null)
            {
                var cnt = (chunhatro)Session["LoggedCNT"];
                ViewBag.Username = cnt.Tenchunhatro;
            }

            return PartialView();
        }
    }
}