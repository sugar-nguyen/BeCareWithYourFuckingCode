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
        Models.WEBACCOUNTEntities Entities = new Models.WEBACCOUNTEntities();
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult getHeader() // kiểm tra trạng thái đăng nhập để lựa chọn header nào
        {
            if (Session["UserID"] != null)
            {
                return PartialView("_Header2");
            }
            else
            {
                return PartialView("_Header1");
            }  
        }
        public JsonResult GetAll(string id)
        {
            int limit = id != null ? Int32.Parse(id) : 8;

            bool proxyCreation = Entities.Configuration.ProxyCreationEnabled;
            try
            {
                Entities.Configuration.ProxyCreationEnabled = false;
                var data = Entities.TB_GAME_ACCOUNT.Select(x =>
                    new
                    {
                        id = x.ID,
                        username = x.USERNAME,
                        tuong = x.TB_GAME_ACCOUNT_DETAIL.GENERAL_NUMBER,
                        skin = x.TB_GAME_ACCOUNT_DETAIL.SKIN_NUMBER,
                        ngoc = x.TB_GAME_ACCOUNT_DETAIL.GEM_NUMBER,
                        img = x.RE_IMAGE,
                        hang = x.TB_GAME_ACCOUNT_DETAIL.TB_RANK_NAME.RANK_NAME,
                        gia = x.ORIGINAL_PRICE,
                        ngay = x.DATE_UPLOAD
                    }).Take(limit);
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
       

    }
}