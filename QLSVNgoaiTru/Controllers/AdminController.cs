using PagedList;
using QLSVNgoaiTru.Models;
using System;
using System.Collections.Generic;
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
            Session["LoggedCNT"] = "";
            Session["LoggedSV"] = "";
            Session["LoggedAD"] = "";
            chunhatro cnt = new chunhatro();
            sinhvien sv = new sinhvien();
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
                    db.chunhatros.InsertOnSubmit(cnt);
                    db.SubmitChanges();
                }
                return RedirectToAction("Chunhatro");
            }
        }

        public ActionResult Xoachutro(int id)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            //Lay ra doi tuong can xoa theo ma
            chunhatro sp = db.chunhatros.SingleOrDefault(n => n.Machunhatro == id);
            ViewBag.Machunhatro = sp.Machunhatro;
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }

        [HttpPost, ActionName("Xoachutro")]
        public ActionResult Xacnhanxoa(int id)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            //Lay ra doi tuong can xoa theo ma
            chunhatro sp = db.chunhatros.SingleOrDefault(n => n.Machunhatro == id);
            ViewBag.Machunhatro = sp.Machunhatro;
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.chunhatros.DeleteOnSubmit(sp);
            db.SubmitChanges();
            return RedirectToAction("Chunhatro");
        }

        public ActionResult Sinhvien(int? page)
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            //return View(db.SanPhams.ToList());
            return View(db.sinhviens.ToList().OrderBy(n => n.Masv).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult CNTDetail()
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }
        public ActionResult Index()
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }
        public ActionResult ThemSinhVien()
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }
        public ActionResult SuaSinhVien()
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }
        public ActionResult CTSinhVien()
        {
            if (Session["LoggedAD"] == null || Session["LoggedAD"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }
    }
}