using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeCareWithYourFuckingCode.Models;
namespace BeCareWithYourFuckingCode.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        Models.WEBACCOUNTEntities2 Entities = new Models.WEBACCOUNTEntities2();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAll()
        {

            bool proxyCreation = Entities.Configuration.ProxyCreationEnabled;
            try
            {
                Entities.Configuration.ProxyCreationEnabled = false;
                var data = Entities.TB_GAME_ACCOUNT.Select(x =>
                    new
                    {
                        id = x.USERNAME,
                        tuong = x.TB_GAME_ACCOUNT_DETAIL.GENERAL_NUMBER,
                        skin = x.TB_GAME_ACCOUNT_DETAIL.SKIN_NUMBER,
                        ngoc = x.TB_GAME_ACCOUNT_DETAIL.GEM_NUMBER,
                        img = x.RE_IMAGE,
                        hang = x.TB_GAME_ACCOUNT_DETAIL.TB_RANK_NAME.RANK_NAME,
                        gia = x.ORIGINAL_PRICE
                    });
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            finally
            {
                Entities.Configuration.ProxyCreationEnabled = proxyCreation;
            }
        }

        public ActionResult getLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult getDetail(string id)
        {
            try
            {
                var data = Entities.TB_GAME_ACCOUNT_DETAIL.Where(x => x.USERNAME == id).Select(x => new
                {
                    id = x.USERNAME,
                    tuong = x.GENERAL_NUMBER,
                    skin = x.SKIN_NUMBER,
                    ngoc = x.GEM_NUMBER,
                    hang = x.TB_RANK_NAME.RANK_NAME,
                    hang1 = x.TB_RANK_NAME1.RANK_NAME,
                    clan = x.CLAN_NAME
                });
                if (ViewBag.detail != null)
                {
                    return Json(data,JsonRequestBehavior.AllowGet);
                }
                return Content("Rỗng");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
    }
}