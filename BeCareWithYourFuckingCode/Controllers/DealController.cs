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
            var message = "";
            try
            {
                
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
                message = "fail";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
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
        public ActionResult BuyNow(string id)
        {
            ViewBag.id = id;
            return View();
        }

        public ActionResult BuyNowAction(string id)
        {
            int _id = int.Parse(id);
            var model = entities.TB_GAME_ACCOUNT.Where(x => x.ID == _id).Select(x => new
            {
                id = x.ID,
                price1 = x.ORIGINAL_PRICE,
                price2 = x.ORIGINAL_PRICE,
                tuong = x.TB_GAME_ACCOUNT_DETAIL.GENERAL_NUMBER,
                skin = x.TB_GAME_ACCOUNT_DETAIL.SKIN_NUMBER,
                ngoc = x.TB_GAME_ACCOUNT_DETAIL.GEM_NUMBER,
                hang = x.TB_GAME_ACCOUNT_DETAIL.TB_RANK_NAME1.RANK_NAME
            }).SingleOrDefault();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }

}