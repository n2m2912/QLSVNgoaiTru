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

        public ActionResult Dkngoaitru()
        {
            if (Session["LoggedSV"] == null || Session["LoggedSV"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Dktimphongtro()
        {
            if (Session["LoggedSV"] == null || Session["LoggedSV"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Dkthaydoidiachi()
        {
            if (Session["LoggedSV"] == null || Session["LoggedSV"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

    }
}