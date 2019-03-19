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

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            Session["LoggedCNT"] = null;
            Session["LoggedSV"] = null;
            Session["LoggedAD"] = null;
            var tendn = collection["username"];
            var matkhau = collection["password"];
            chunhatro cnt = new chunhatro();
            sinhvien sv = new sinhvien();
            if(Db.chunhatros.SingleOrDefault(n => n.SDT == Convert.ToInt32(tendn) && n.Pass == matkhau) != null)
            {
                cnt = Db.chunhatros.SingleOrDefault(n => n.SDT == Convert.ToInt32(tendn) && n.Pass == matkhau);
                Session["LoggedCNT"] = cnt;
                return RedirectToAction("Index", "ChuNT");
            } else if(Db.sinhviens.SingleOrDefault(n => n.Masv == Convert.ToInt32(tendn) && n.Pass == matkhau) != null)
            {
                sv = Db.sinhviens.SingleOrDefault(n => n.Masv == Convert.ToInt32(tendn) && n.Pass == matkhau);
                Session["LoggedSV"] = sv;
                return RedirectToAction("Index", "Home");
            }
            else
            {

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
            var phong = Layphong(50);
            return View(phong);
        }
        public ActionResult Detail(int id)
        {
            CustomObject co = new CustomObject();
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

        public ActionResult Logout()
        {
            Session["LoggedCNT"] = null;
            Session["LoggedSV"] = null;
            Session["LoggedAD"] = null;
            UsernamePartial();
            var phong = Layphong(50);
            return View("Index",phong);
        }

        public ActionResult UsernamePartial()
        {
            if(Session["LoggedCNT"] != null)
            {
                var cnt = (chunhatro)Session["LoggedCNT"];
                ViewBag.Username = cnt.Tenchunhatro;
            }
            else if (Session["LoggedSV"] != null)
            {
                var sv = (sinhvien)Session["LoggedSV"];
                ViewBag.Username = sv.Tensv;
            } else
            {
                ViewBag.Username = "Chưa đăng nhập";
            }
            
            return PartialView();
        }
        

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }

    }
}