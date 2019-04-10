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
        public ActionResult updateDeal(TB_GAME_PRICE_OFFER newOffer)  //update lại giá mới cho lần trả giá thứ 2
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
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult BuyNow(string id) //return view Buynow
        {
            ViewBag.id = id;
            return View();
        }

        public ActionResult BuyNowAction(string id)  //action Buynow
        {
            int _id = int.Parse(id);
            var model = entities.TB_GAME_ACCOUNT.Where(x => x.ID == _id).Select(x => new
            {
                id = x.ID,
                username = x.USERNAME,
                price1 = x.ORIGINAL_PRICE,
                price2 = x.ORIGINAL_PRICE,
                tuong = x.TB_GAME_ACCOUNT_DETAIL.GENERAL_NUMBER,
                skin = x.TB_GAME_ACCOUNT_DETAIL.SKIN_NUMBER,
                ngoc = x.TB_GAME_ACCOUNT_DETAIL.GEM_NUMBER,
                hang = x.TB_GAME_ACCOUNT_DETAIL.TB_RANK_NAME1.RANK_NAME
            }).SingleOrDefault();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateBill(TB_BILL bill)
        {
            var message = "";
            try
            {
                bill.BILL_DATE = DateTime.Now;
                bill.STATUS = 0;
                entities.TB_BILL.Add(bill);
                if (entities.SaveChanges() > 0)
                {
                    TB_GAME_ACCOUNT account = entities.TB_GAME_ACCOUNT.Find(bill.GAME_ACCOUNT_ID);
                    account.CURRENT_STATUS = 2; // cập nhật lại trạng thái 2(đã bán)
                    TB_USER user = entities.TB_USER.Find(bill.USER_ACCOUNT_ID);
                    user.TB_MONEY.TOTAL_MONEY = user.TB_MONEY.TOTAL_MONEY - bill.SUCCESS_PRICE; // cập nhật lại số dư
                    if (entities.SaveChanges() > 0)
                    {
                        message = "success";
                    }
                }
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = ex.GetBaseException().ToString();
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }

}