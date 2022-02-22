using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using WebBanHang.Models.EF;

namespace WebBanHang.Areas.Admin.Controllers
{
    [SessionAuthorize]
    [Authorize(Roles = "Adminitrator")]
    public class HomeController : Controller
    {

        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

    }
}