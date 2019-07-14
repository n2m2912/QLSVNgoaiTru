using PagedList;
using QLSVNgoaiTru.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLSVNgoaiTru.Controllers
{
    public class AdminController : Controller
    {
        readonly DbQLSVDataContext db = new DbQLSVDataContext();
        // GET: admin

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            Session["LoggedAD"] = "";
            nguoidung admin = new nguoidung();
            var tendn = collection["user"];
            var matkhau = collection["pass"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                admin = db.nguoidungs.SingleOrDefault(n => n.username == tendn && n.userpassword == matkhau);
                if (admin != null)
                {
                    Session["LoggedAD"] = admin;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Admin");
        }

        public ActionResult Index()
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            List<int> number = new List<int>();
            number.Add(db.phongtros.ToList().Count());
            number.Add(db.sinhviens.ToList().Count());
            number.Add(db.chunhatros.ToList().Count());
            ViewBag.ThongKe = number;
            return View();
        }

        public ActionResult Chunhatro()
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            return View(db.chunhatros.ToList());
        }

        [HttpGet]
        public ActionResult Themchunhatro()
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themchunhatro(chunhatro cnt, HttpPostedFileBase fileupload)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
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
                        var path = Path.Combine(Server.MapPath("~/images/chunhatro/"), fileName);
                        //Kiem tra hình anh ton tai chua?
                        if (System.IO.File.Exists(path))
                            ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                        else
                        {
                            //Luu hinh anh vao duong dan
                            fileupload.SaveAs(path);
                        }
                        cnt.Imagees = fileName;
                        //Luu vao CSDL

                        DateTime date = DateTime.Now;
                        cnt.Machunhatro = "cnt" + date.ToString("yyMMHHmmss");
                        db.chunhatros.InsertOnSubmit(cnt);
                        db.SubmitChanges();
                    }

                }
                TempData["message"] = "success";
            }
            catch (Exception ex) { TempData["message"] = "fail"; }
            TempData["message2"] = "add";
            return RedirectToAction("Chunhatro");
        }

        public ActionResult CNTDetail(string id)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            ChiTietCNT ctcnt = new ChiTietCNT();
            ctcnt.pt = db.phongtros.Where(m => m.Machunhatro == id).ToList();
            ctcnt.ctdv= db.ctdvs.Where(m => m.Macnt == id).ToList();
            ViewBag.chitietcnt = ctcnt;
            ViewBag.loaidichvu = db.dichvus.ToList().OrderBy(n => n.TenloaiDV);
            return View(db.chunhatros.SingleOrDefault(n => n.Machunhatro == id));
        }

        public ActionResult Suachunhatro(string id)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            return View(db.chunhatros.SingleOrDefault(n => n.Machunhatro == id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suachunhatro(chunhatro cnt, HttpPostedFileBase fileupload)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            try {
                chunhatro cntt = db.chunhatros.SingleOrDefault(n => n.Machunhatro == cnt.Machunhatro);
                cntt.Tenchunhatro = cnt.Tenchunhatro;
                cntt.Namsinh = cnt.Namsinh;
                cntt.SDT = cnt.SDT;
                cntt.Hokhau = cnt.Hokhau;
                if (ModelState.IsValid)
                {
                    ViewBag.Lop = db.lops.ToList().OrderBy(n => n.Tenlop);
                    if (fileupload == null)
                    {
                        cntt.Imagees = db.chunhatros.SingleOrDefault(n => n.Machunhatro == cnt.Machunhatro).Imagees;
                    }
                    else
                    {
                        if (ModelState.IsValid)
                        {
                            var fileName = Path.GetFileName(fileupload.FileName);
                            var path = Path.Combine(Server.MapPath("~/images/chunhatro"), fileName);
                            if (System.IO.File.Exists(path))
                            {
                                cnt.Imagees = fileName;
                                ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                                return View(cnt);
                            }
                            else
                            {
                                fileupload.SaveAs(path);
                                cntt.Imagees = fileName;
                            }
                        }
                    }
                    db.SubmitChanges();
                    TempData["message"] = "success";
                }
            } catch (Exception ex) { TempData["message"] = "fail"; }
            TempData["message2"] = "edit";
            return RedirectToAction("Chunhatro");
        }

        public ActionResult XoaChunhatro(string id)
        {
            try
            {
                chunhatro cnt = db.chunhatros.SingleOrDefault(m => m.Machunhatro == id);
                db.chunhatros.DeleteOnSubmit(cnt);
                db.SubmitChanges();
                TempData["message"] = "success";
            } catch (Exception ex)
            {
                TempData["message"] = "fail";
            }

            TempData["message2"] = "delete";
            return RedirectToAction("Chunhatro");
        }

        public ActionResult Sinhvien()
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            ChiTietSinhVien ctsv;

            List<ChiTietSinhVien> dsctsv = new List<ChiTietSinhVien>();
            List<sinhvien> dssv = db.sinhviens.ToList();
            foreach (sinhvien sv in dssv){
                ctsv = new ChiTietSinhVien();
                ctsv.sv = db.sinhviens.SingleOrDefault(n => n.Masv == sv.Masv);
                ctsv.cl = db.lops.SingleOrDefault(n => n.Malop == ctsv.sv.Malop);
                ctsv.k = db.khoas.SingleOrDefault(n => n.Makhoa == ctsv.cl.Makhoa);
                dsctsv.Add(ctsv);
            }
            ViewBag.danhsachsv = dsctsv;
            return View();
        }

       
        
        public ActionResult ThemSinhVien()
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.Lop = db.lops.ToList().OrderBy(n => n.Tenlop);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemSinhVien(sinhvien sv, HttpPostedFileBase fileupload)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
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
                        var path = Path.Combine(Server.MapPath("~/images/sv"), fileName);
                        //Kiem tra hình anh ton tai chua?
                        if (System.IO.File.Exists(path))
                            ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                        else
                        {
                            //Luu hinh anh vao duong dan
                            fileupload.SaveAs(path);
                        }
                        sv.Images = fileName;
                        //Luu vao CSDL

                        DateTime date = DateTime.Now;
                        sv.Masv = "sv" + date.ToString("yyMMHHmmss");
                        db.sinhviens.InsertOnSubmit(sv);
                        db.SubmitChanges();
                    }
                }
                TempData["message"] = "success";
            }
            catch (Exception ex) { TempData["message"] = "fail"; }
            TempData["message2"] = "add";
            return RedirectToAction("Sinhvien");
        }


        public ActionResult SuaSinhVien(string id)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.Lop = db.lops.ToList().OrderBy(n => n.Tenlop);
            return View(db.sinhviens.SingleOrDefault(n => n.Masv == id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaSinhVien(sinhvien sv, HttpPostedFileBase fileupload)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            try {
                sinhvien svv = db.sinhviens.SingleOrDefault(n => n.Masv == sv.Masv);
                svv.Tensv = sv.Tensv;
                svv.Namsinh = sv.Namsinh;
                svv.SDTgiadinh = sv.SDT;
                svv.Doituonguutien = sv.Doituonguutien;
                svv.SDT = sv.SDT;
                svv.Quequan = sv.Quequan;
                svv.Nienkhoa = sv.Nienkhoa;
                svv.Malop = sv.Malop;
                svv.Gioitinh = sv.Gioitinh;
                svv.Diachi = sv.Diachi;
                if (ModelState.IsValid)
                {
                    ViewBag.Lop = db.lops.ToList().OrderBy(n => n.Tenlop);
                    if (fileupload == null)
                    {
                        svv.Images = db.sinhviens.SingleOrDefault(n => n.Masv == sv.Masv).Images;
                    }
                    else
                    {
                        if (ModelState.IsValid)
                        {
                            var fileName = Path.GetFileName(fileupload.FileName);
                            var path = Path.Combine(Server.MapPath("~/images/sv"), fileName);
                            if (System.IO.File.Exists(path))
                            {
                                sv.Images = fileName;
                                ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                                return View(sv);
                            }
                            else
                            {
                                fileupload.SaveAs(path);
                                svv.Images = fileName;
                            }
                        }
                    }
                    db.SubmitChanges();
                    TempData["message"] = "success";
                }
            } catch (Exception){ TempData["message"] = "fail"; }
            TempData["message2"] = "edit";
            return RedirectToAction("Sinhvien");
        }

        public ActionResult XoaSinhVien(string id)
        {
            try
            {
                sinhvien sv = db.sinhviens.SingleOrDefault(m => m.Masv == id);
                db.sinhviens.DeleteOnSubmit(sv);
                db.SubmitChanges();
                TempData["message"] = "success";
            } catch (Exception ex) { TempData["message"] = "fail"; }
            TempData["message2"] = "delete";
            return RedirectToAction("Sinhvien");
        }

        public ActionResult CTSinhVien(string id)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            ChiTietSinhVien ctsv = new ChiTietSinhVien();
            ctsv.sv = db.sinhviens.SingleOrDefault(n => n.Masv == id);
            ctsv.cl = db.lops.SingleOrDefault(n => n.Malop == ctsv.sv.Malop);
            ctsv.k = db.khoas.SingleOrDefault(n => n.Makhoa == ctsv.cl.Makhoa);

            DanhSachPhieu dsp = new DanhSachPhieu();
            dsp.dktpt = db.phieudangkitimphongtros.Where(m => m.Masv == id).ToList();
            dsp.tddc = db.phieuthaydoidiachis.Where(m => m.Masv == id ).ToList();
            dsp.xnttnt = db.phieuxacnhanthongtinngoaitrus.Where(m => m.Masv == id).ToList();
            dsp.dknt = db.phieudangkingoaitrus.Where(m => m.Masv == id ).ToList();
            dsp.gtsvnt = db.phieugioithieusinhvienngoaitrus.Where(m => m.Masv == id).ToList();
            ViewBag.danhsachphieu = dsp;
            return View(ctsv);
        }

        public ActionResult Service()
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }

            return View(db.dichvus.ToList());
        }

        public ActionResult AddService()
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddService(dichvu dv)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            try
            {
                db.dichvus.InsertOnSubmit(dv);
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch(Exception ex)
            {
                TempData["message"] = "fail";
            }
            TempData["message2"] = "add";
            return RedirectToAction("Service");
        }

        public ActionResult EditService(int id)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            return View(db.dichvus.SingleOrDefault(n => n.MaloaiDV == id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditService(dichvu dv)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            try
            {
                dichvu dvv = db.dichvus.SingleOrDefault(n => n.MaloaiDV == dv.MaloaiDV);
                dvv.TenloaiDV = dv.TenloaiDV == null ? dvv.TenloaiDV : dv.TenloaiDV;
                dvv.Donvitinh = dv.Donvitinh == null ? dvv.Donvitinh : dv.Donvitinh;
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception ex) { TempData["message"] = "fail"; }
            TempData["message2"] = "edit";
            return RedirectToAction("Service");
        }

        public ActionResult DeleteService(int id)
        {
            try
            {
                dichvu dv = db.dichvus.SingleOrDefault(m => m.MaloaiDV == id);
                db.dichvus.DeleteOnSubmit(dv);
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception ex) {
                TempData["message"] = "fail";
            }
            TempData["message2"] = "delete";
            return RedirectToAction("Service");
        }

        public ActionResult Faculty()
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }

            return View(db.khoas.ToList());
        }

        public ActionResult AddFaculty()
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddFaculty(khoa k)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            try
            {
                db.khoas.InsertOnSubmit(k);
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }

            TempData["message2"] = "add";
            return RedirectToAction("Faculty");
        }

        public ActionResult EditFaculty(int id)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            return View(db.khoas.SingleOrDefault(n => n.Makhoa == id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditFaculty(khoa k)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            try
            {
                khoa kk = db.khoas.SingleOrDefault(n => n.Makhoa == k.Makhoa);
                kk.Tenkhoa = k.Tenkhoa == null ? kk.Tenkhoa : k.Tenkhoa;
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch(Exception e)
            {
                TempData["message"] = "fail";
            }
            TempData["message2"] = "edit";
            return RedirectToAction("Faculty");
        }

        public ActionResult DeleteFaculty(int id)
        {
            try
            {

                khoa k = db.khoas.SingleOrDefault(m => m.Makhoa == id);
                db.khoas.DeleteOnSubmit(k);
                db.SubmitChanges();
                TempData["message"] = "success";
            } catch (Exception ex){
                TempData["message"] = "fail";
            }

            TempData["message2"] = "delete";
            return RedirectToAction("Faculty");
        }

        public ActionResult City()
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }

            return View(db.tinhtps.ToList());
        }

        public ActionResult AddCity()
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddCity(tinhtp tp)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            try
            {
                db.tinhtps.InsertOnSubmit(tp);
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }

            TempData["message2"] = "add";
            return RedirectToAction("City");
        }

        public ActionResult EditCity(int id)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            return View(db.tinhtps.SingleOrDefault(n => n.Matinhtp == id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditCity(tinhtp tp)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            try
            {
                tinhtp tpp = db.tinhtps.SingleOrDefault(n => n.Matinhtp == tp.Matinhtp);
                tpp.Tentinhtp = tp.Tentinhtp== null ? tpp.Tentinhtp: tp.Tentinhtp;
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception e)
            {
                TempData["message"] = "fail";
            }
            TempData["message2"] = "edit";
            return RedirectToAction("City");
        }

        public ActionResult DeleteCity(int id)
        {
            try
            {

                tinhtp tp = db.tinhtps.SingleOrDefault(m => m.Matinhtp== id);
                db.tinhtps.DeleteOnSubmit(tp);
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }

            TempData["message2"] = "delete";
            return RedirectToAction("City");
        }


        public ActionResult Classes()
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }

            return View(db.lops.ToList());
        }

        public ActionResult AddClass()
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.Khoa = db.khoas.ToList().OrderBy(n => n.Tenkhoa);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddClass(lop l)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            try
            {
                db.lops.InsertOnSubmit(l);
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }

            TempData["message2"] = "add";
            return RedirectToAction("Classes");
        }

        public ActionResult EditClass(int id)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.Khoa = db.khoas.ToList().OrderBy(n => n.Tenkhoa);

            return View(db.lops.SingleOrDefault(n => n.Malop == id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditClass(lop l)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            try
            {
                lop ll = db.lops.SingleOrDefault(n => n.Malop == l.Malop);
                ll.Tenlop = l.Tenlop == null ? ll.Tenlop : l.Tenlop;
                ll.Makhoa = l.Makhoa;
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception e)
            {
                TempData["message"] = "fail";
            }
            TempData["message2"] = "edit";
            return RedirectToAction("Classes");
        }

        public ActionResult DeleteClass(int id)
        {
            try
            {

                lop l = db.lops.SingleOrDefault(m => m.Malop == id);
                db.lops.DeleteOnSubmit(l);
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }

            TempData["message2"] = "delete";
            return RedirectToAction("Classes");
        }

        public ActionResult Ward()
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }

            return View(db.quanhuyens.ToList());
        }

        public ActionResult AddWard()
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.Tinh = db.tinhtps.ToList().OrderBy(n => n.Tentinhtp);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddWard(quanhuyen qh)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            try
            {
                db.quanhuyens.InsertOnSubmit(qh);
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }

            TempData["message2"] = "add";
            return RedirectToAction("Ward");
        }

        public ActionResult EditWard(int id)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.Tinh = db.tinhtps.ToList().OrderBy(n => n.Tentinhtp);

            return View(db.quanhuyens.SingleOrDefault(n => n.Maquanhuyen == id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditWard(quanhuyen qh)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            try
            {
                quanhuyen qhh = db.quanhuyens.SingleOrDefault(n => n.Maquanhuyen == qh.Maquanhuyen);
                qhh.Tenquanhuyen = qh.Tenquanhuyen == null ? qhh.Tenquanhuyen : qh.Tenquanhuyen;
                qhh.Matinhtp = qh.Matinhtp;
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception e)
            {
                TempData["message"] = "fail";
            }
            TempData["message2"] = "edit";
            return RedirectToAction("Ward");
        }

        public ActionResult DeleteWard(int id)
        {
            try
            {

                quanhuyen qh = db.quanhuyens.SingleOrDefault(m => m.Maquanhuyen == id);
                db.quanhuyens.DeleteOnSubmit(qh);
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }

            TempData["message2"] = "delete";
            return RedirectToAction("Ward");
        }

        public ActionResult Rule()
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }

            return View(db.loaivps.ToList());
        }

        public ActionResult AddRule()
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddRule(loaivp vp)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            try
            {
                db.loaivps.InsertOnSubmit(vp);
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }

            TempData["message2"] = "add";
            return RedirectToAction("Rule");
        }

        public ActionResult EditRule(int id)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }

            return View(db.loaivps.SingleOrDefault(n => n.MaloaiVP == id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditRule(loaivp vp)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            try
            {
                loaivp vpp = db.loaivps.SingleOrDefault(n => n.MaloaiVP == vp.MaloaiVP);
                vpp.TenloaiVP = vp.TenloaiVP == null ? vpp.TenloaiVP: vp.TenloaiVP;
                vpp.Hinhthucxuly = vp.Hinhthucxuly == null ? vpp.Hinhthucxuly: vp.Hinhthucxuly;
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception e)
            {
                TempData["message"] = "fail";
            }
            TempData["message2"] = "edit";
            return RedirectToAction("Rule");
        }

        public ActionResult DeleteRule(int id)
        {
            try
            {

                loaivp vp = db.loaivps.SingleOrDefault(m => m.MaloaiVP == id);
                db.loaivps.DeleteOnSubmit(vp);
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }

            TempData["message2"] = "delete";
            return RedirectToAction("Rule");
        }

        public ActionResult PhieuXacNhanThongTinNgoaiTru()
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            var query = @"select * from phieuxacnhanthongtinngoaitru ttnt join quanhuyen qh on ttnt.Maquanhuyen = qh.Maquanhuyen ";
            return View(db.phieuxacnhanthongtinngoaitrus.ToList());
        }

        public ActionResult DuyetPhieuXacNhanThongTinNgoaiTru(int id)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.quanhuyen = db.quanhuyens.ToList().OrderBy(n => n.Tenquanhuyen);
            return View(db.phieuxacnhanthongtinngoaitrus.SingleOrDefault(n => n.Maphieuxacnhan == id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DuyetPhieuXacNhanThongTinNgoaiTru(phieuxacnhanthongtinngoaitru ttnt)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            try
            {
                phieuxacnhanthongtinngoaitru ttntt = db.phieuxacnhanthongtinngoaitrus.SingleOrDefault(n => n.Maphieuxacnhan == ttnt.Maphieuxacnhan);
                ttntt.Conganphuongxa = ttnt.Conganphuongxa;
                ttntt.Maquanhuyen = ttnt.Maquanhuyen;
                ttntt.Diachicutru = ttnt.Diachicutru;
                ttntt.Todanpho = ttnt.Todanpho;
                ttntt.Quanhevoichuho = ttnt.Quanhevoichuho;
                ttntt.Nhanxetsinhvien = ttnt.Nhanxetsinhvien;
                ttntt.Masv = ttnt.Masv;
                ttntt.TrangThai = 1;
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception e)
            {
                TempData["message"] = "fail";
            }
            TempData["message2"] = "revise";
            return RedirectToAction("Sinhvien");
        }

        public ActionResult DuyetDkTimPhongTro(int id)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            try
            {
                phieudangkitimphongtro dktpt = db.phieudangkitimphongtros.SingleOrDefault(m => m.Madangkytimnhatro == id);
                dktpt.TrangThai = 1;
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }
            TempData["message2"] = "revise";
            return RedirectToAction("Sinhvien");
        }

        public ActionResult DuyetDkthaydoidiachi(int id)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            try
            {
                phieuthaydoidiachi tddc = db.phieuthaydoidiachis.SingleOrDefault(m => m.Maphieuthaydoidiachi == id);
                tddc.TrangThai = 1;
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }
            TempData["message2"] = "revise";
            return RedirectToAction("Sinhvien");
        }

        public ActionResult DuyetXacNhanThongTinNgoaiTru(int id)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            try
            {
                phieuxacnhanthongtinngoaitru xnttnt = db.phieuxacnhanthongtinngoaitrus.SingleOrDefault(m => m.Maphieuxacnhan == id);
                xnttnt.TrangThai = 1;
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }
            TempData["message2"] = "revise";
            return RedirectToAction("Sinhvien");
        }

        public ActionResult DuyetDKNgoaiTru(int id)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            try
            {
                phieudangkingoaitru dknt = db.phieudangkingoaitrus.SingleOrDefault(m => m.MaPDKNT == id);
                dknt.TrangThai = 1;
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception ex)
            {
                TempData["message"] = "fail";
            }
            TempData["message2"] = "revise";
            return RedirectToAction("Sinhvien");
        }

        public ActionResult DuyetPhieuGioiThieuSinhVienNgoaiTru(int id)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }

            return View(db.phieugioithieusinhvienngoaitrus.SingleOrDefault(n => n.Maxacnhan == id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DuyetPhieuGioiThieuSinhVienNgoaiTru(phieugioithieusinhvienngoaitru gtsvnt)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            try
            {
                phieugioithieusinhvienngoaitru gtsvntt = db.phieugioithieusinhvienngoaitrus.SingleOrDefault(n => n.Maxacnhan == gtsvnt.Maxacnhan);
                gtsvntt.Masv = gtsvntt.Masv;
                gtsvntt.Ngaylap = gtsvntt.Ngaylap;
                gtsvntt.Noidung = gtsvnt.Noidung;
                gtsvntt.TrangThai = 1;
                db.SubmitChanges();
                TempData["message"] = "success";
            }
            catch (Exception e)
            {
                TempData["message"] = "fail";
            }
            TempData["message2"] = "revise";
            return RedirectToAction("Sinhvien");
        }

        public ActionResult GetStatistic()
        {

            return View();
        }
    }

}