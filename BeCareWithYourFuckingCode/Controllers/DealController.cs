using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeCareWithYourFuckingCode.Models;
namespace BeCareWithYourFuckingCode.Controllers
{
    public class DealController : Controller
    {
        //
        // GET: /Deal/
        WEBACCOUNTEntities entities = new WEBACCOUNTEntities();

        [HttpPost]
        public ActionResult CreateDeal(TB_GAME_PRICE_OFFER newOffer) // kiểm tra xem tải khoản đã trả giá cho acc đó chưa nếu rồi thì phản hồi và gọi action cập nhật
        {
            try
            {
                var message = "";
                TB_GAME_PRICE_OFFER offer = entities.TB_GAME_PRICE_OFFER.Where(x => x.USER_ACCOUNT_ID == newOffer.USER_ACCOUNT_ID && x.USERNAME == newOffer.USERNAME).SingleOrDefault();
                if (offer == null)
                {
                    entities.TB_GAME_PRICE_OFFER.Add(newOffer);
                    entities.SaveChanges();
                    message = "success";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
                message = "?";
                return Json(message, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult updateDeal(TB_GAME_PRICE_OFFER newOffer)
        {
            try
            {
                var message = "";
                TB_GAME_PRICE_OFFER offer = entities.TB_GAME_PRICE_OFFER.Where(x => x.USER_ACCOUNT_ID == newOffer.USER_ACCOUNT_ID && x.USERNAME == newOffer.USERNAME).SingleOrDefault();
                if (offer != null)
                {
                    offer.USER_PRICE_OFFER = newOffer.USER_PRICE_OFFER;
                    entities.SaveChanges();
                    message = "success";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
                message = "fail";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}