using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeCareWithYourFuckingCode.Models;
using BeCareWithYourFuckingCode.Controllers;
namespace BeCareWithYourFuckingCode.Models
{
    public class AdminController : Controller
    {
        WEBACCOUNTEntities entities = new WEBACCOUNTEntities();
        GameAccountController GameAccount = new GameAccountController();
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

        public ActionResult getAdmin()
        {
            var name = Session["adminName"].ToString() ?? "";
            return Json(name, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getTBGameAccount()
        {
            return View(entities.TB_GAME_ACCOUNT);
        }
        public ActionResult getTBGameAccountAcc(int id)
        {
            if (id == -1)
            {
                return Json(entities.TB_GAME_ACCOUNT.Select(x=> new {
                    id = x.ID,
                    username = x.USERNAME,
                    pass = x.PASSWORD_KEY,
                    gia = x.ORIGINAL_PRICE,
                    level = x.TB_GAME_ACCOUNT_DETAIL.LEVEL_NUMBER,
                    img = x.RE_IMAGE,
                    status = x.CURRENT_STATUS
                }), JsonRequestBehavior.AllowGet);
            }
            return Json(entities.TB_GAME_ACCOUNT.Where(x=>x.CURRENT_STATUS == id).Select(x=>new {
                id = x.ID,
                    username = x.USERNAME,
                    pass = x.PASSWORD_KEY,
                    gia = x.ORIGINAL_PRICE,
                    level = x.TB_GAME_ACCOUNT_DETAIL.LEVEL_NUMBER,
                    img = x.RE_IMAGE,
                    status = x.CURRENT_STATUS
            }),JsonRequestBehavior.AllowGet);
        }


        public ActionResult changeStatusGameAccountTo_1(string id)
        {
            TB_GAME_ACCOUNT acc = entities.TB_GAME_ACCOUNT.Find(id);
            var message = "fail";
            if (acc != null)
            {
                acc.CURRENT_STATUS = 1;
                if (entities.SaveChanges() > 0)
                {
                    message = "success";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getTBUSER()
        {
            return View(entities.TB_USER);
        }

        public ActionResult DetailsTB_GAME_ACCOUNT(string id)
        {
            return GameAccount.getDetailAction(id);
        }

        // sua


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateTB_GAME_ACCOUNT(TB_GAME_ACCOUNT entity)
        {
            if (entity == null) return Content("Null cmnr");
            if (ModelState.IsValid)
            {
                try
                {
                    TB_GAME_ACCOUNT model = entities.TB_GAME_ACCOUNT.Find(entity.USERNAME);
                    model.ORIGINAL_PRICE = entity.ORIGINAL_PRICE;
                    model.CURRENT_STATUS = entity.CURRENT_STATUS;
                    entities.SaveChanges();

                }
                catch (Exception e)
                {
                    return Content(e.Message);
                }
                return RedirectToAction("getTBGameAccount");
            }
            return View(entity);
        }

    }
}