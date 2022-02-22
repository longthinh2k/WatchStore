using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models.EF;

namespace WebBanHang.Areas.Admin.Controllers
{
    [SessionAuthorize]
    [Authorize(Roles = "Adminitrator")]
    public class GopYController : Controller
    {
        QLBanHangDBContext db = new QLBanHangDBContext();
        // GET: Admin/GopY
        public ActionResult Index()
        {
            return View(db.tbl_LienHe.ToList());
        }


    }
}