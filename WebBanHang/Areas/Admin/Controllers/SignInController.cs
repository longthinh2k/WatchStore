using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models.EF;
using System.Web.Security;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class SignInController : Controller
    {
        QLBanHangDBContext db = new QLBanHangDBContext();
        //GET: Admin/SignIn
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(UserAdmin model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Không đúng tài khoản");
                return View(model);
            }
            var account = db.UserAdmins.SingleOrDefault(m => m.TaiKhoan == model.TaiKhoan && m.MatKhau == model.MatKhau);
            if (account != null)
            {
                FormsAuthentication.SetAuthCookie(account.TaiKhoan, false);
                Session["Role"] = account.TaiKhoan.ToString();
                Session["Name"] = account.TaiKhoan;
                Session["Id"] = account.id;
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Không đúng tài khoản");
            return View(model);
        }

        public ActionResult Signout()
        {
            Session["Name"] = "";
            FormsAuthentication.SignOut();
            return  RedirectToAction("SignIn", "SignIn");
        }
        //public JsonResult Login(string TaiKhoan, string MatKhau)
        //{
        //    //var tk = taikhoan;
        //    //var mk = matkhau;
        //    //var acc = db.UserAdmins.SingleOrDefault(x => x.TaiKhoan == tk && x.MatKhau == mk);
        //    //if(acc != null)
        //    //{
        //    //    //Đăng nhập thành công
        //    //    Session[User] = acc;
        //    //   return RedirectToAction("Home", "Index");
        //    //}
        //    //else
        //    //{
        //    //    //Đăng nhập thất bại
        //    //    return View();
        //    //}
        //    //string TaiKhoan = f["TaiKhoan"].ToString();
        //    //string MatKhau = f["MatKhau"].ToString();
        //    var returnData = new ReturnData();
        //    var ad = db.UserAdmins.SingleOrDefault(n => n.TaiKhoan == TaiKhoan && n.MatKhau == MatKhau);
        //    if (ad != null)
        //    {
        //        returnData.ResponseCode = 1;
        //        returnData.Description = "Thành công";
        //        return Json(returnData, JsonRequestBehavior.AllowGet);
        //    }

        //    else
        //    {
        //        returnData.ResponseCode = -1;
        //        returnData.Description = "Thất bại";
        //        return Json(returnData, JsonRequestBehavior.AllowGet);
        //    }

        // }
     
    }
}