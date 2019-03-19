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

        public ActionResult AddRoom()
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult EditRoom(int id)
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }

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

        public ActionResult Service()
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            var cntObj = (chunhatro)Session["LoggedCNT"];
            return View(Db.dichvus.ToList());
        }
        public ActionResult AddService()
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult EditService()
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult DongTienPhong()
        {
            if (Session["LoggedCNT"] == null || Session["LoggedCNT"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
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