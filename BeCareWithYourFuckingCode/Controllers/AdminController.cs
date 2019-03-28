using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeCareWithYourFuckingCode.Models;
using PagedList;
namespace BeCareWithYourFuckingCode.Models
{
    public class AdminController : Controller
    {
        WEBACCOUNTEntities entities = new WEBACCOUNTEntities();

        public ActionResult checkAdminLogin() // Kiểm tra trạng thái đăng nhập của admin
        {
            var message = "";
            if (Session["admin"] != null)
            {
                message = Session["admin"].ToString();
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult getLogin(TB_ADMIN admin)
        {
            var message = "";
            try
            {
                TB_ADMIN curAdmin = entities.TB_ADMIN.Where(x => x.USERNAME == admin.USERNAME.Trim() && x.PASSWORD_KEY == admin.PASSWORD_KEY.Trim()).SingleOrDefault();
                if (curAdmin != null)
                {
                    message = "/Admin/Index";
                    Session["admin"] = curAdmin.ID;
                    Session["adminName"] = curAdmin.NAME;
                }
                else
                {
                    message = "fail";
                }
                if (Request.IsAjaxRequest())
                {
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Json(message, JsonRequestBehavior.AllowGet);
            }

        }

      
        public ActionResult getTBGameAccount()
        {
            return View(entities.TB_GAME_ACCOUNT);
        }

     
    }
}