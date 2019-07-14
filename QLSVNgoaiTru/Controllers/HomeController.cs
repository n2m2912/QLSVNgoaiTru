using PagedList;
using QLSVNgoaiTru.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLSVNgoaiTru.Controllers
{
    public class HomeController : Controller
    {
        readonly DbQLSVDataContext Db = new DbQLSVDataContext();

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            Session["LoggedCNT"] = "";
            Session["LoggedSV"] = "";
            Session["LoggedAD"] = "";
            chunhatro cnt = new chunhatro();
            sinhvien sv = new sinhvien();
            nguoidung admin = new nguoidung();
            var tendn = collection["username"];
            var matkhau = collection["password"];
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
                
                    cnt = Db.chunhatros.SingleOrDefault(n => n.Machunhatro == tendn  && n.Pass == matkhau);
                    sv = Db.sinhviens.SingleOrDefault(n => n.Masv == tendn && n.Pass == matkhau);
                

                if (cnt!= null)
                {
                    Session["LoggedCNT"] = cnt;
                    Session["cnt"] = cnt.Tenchunhatro;
                    return RedirectToAction("Index", "ChuNT");
                }
                else if (sv != null)
                {
                    Session["LoggedSV"] = sv;
                    return RedirectToAction("Index");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }

        private List<phongtro> Layphong(int count)
        {
            //Sắp xếp phòng theo số phòng, sau đó lấy top @count 
            return Db.phongtros.OrderByDescending(a => a.Sophong).Take(count).ToList();
        }
        // GET: SinhVienNT
        public ActionResult Index()
        {
            if(Session["LoggedSV"] != null)
            {
                sinhvien sv = (sinhvien)Session["LoggedSV"];
                ViewBag.sinhvien = sv.Tensv;
            }
            else
            {
                ViewBag.sinhvien = "Chưa đăng nhập";
            }
            var phong = Layphong(50);
            return View(phong);
        }
        public ActionResult Detail(int id)
        {
            if (Session["LoggedSV"] != null)
            {
                sinhvien sv = (sinhvien)Session["LoggedSV"];
                ViewBag.sinhvien = sv.Tensv;
            }
            else
            {
                ViewBag.sinhvien = "Chưa đăng nhập";
            }
            ChiTietPhongTro co = new ChiTietPhongTro();
            co.phongtro = Db.phongtros.SingleOrDefault(n => n.Maphongtro == id);
            co.chunhatro = Db.chunhatros.SingleOrDefault(n => n.Machunhatro == co.phongtro.Machunhatro);
            ViewBag.Maphongtro = co.phongtro.Maphongtro;
            if (co.phongtro == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(co);
        }
        public ActionResult MoreRooms(int? page)
        {
            int pageSize = 9;
            int pageNum = (page ?? 1);
            var phong = Db.phongtros.ToList();
            return View(phong.ToPagedList(pageNum, pageSize));
        }
        public ActionResult MoreNews()
        {
            return View();
        }
    }
}