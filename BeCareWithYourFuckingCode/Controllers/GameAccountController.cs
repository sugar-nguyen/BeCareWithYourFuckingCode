using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeCareWithYourFuckingCode.Models;
namespace BeCareWithYourFuckingCode.Controllers
{
    public class GameAccountController : Controller
    {
        //
        // GET: /Sale/
        private WEBACCOUNTEntities entities = new WEBACCOUNTEntities();
        public ActionResult UploadAcc()
        {
            ViewBag.tbgank = entities.TB_RANK_NAME.ToList();
            return View();
        }

        //GET
        public ActionResult getDetail(string id)
        {
            ViewBag.id = id;
            return View();
        }

        public ActionResult isBoss(int id) // kiểm tra xem người mua có phải là chủ sở hữu của account hiện tại
        {
            var message = 0;
            if(Session["UserID"] == null) return Json(message,JsonRequestBehavior.AllowGet);
            message = entities.TB_GAME_ACCOUNT.Where(x => x.ID == id).First().USER_ACCOUNT_ID == Session["UserID"].ToString() ? 1 : 0; // lấy ra userid trong bảng account để so sánh với session hiện tại
            return Json(message, JsonRequestBehavior.AllowGet);
        }


        public ActionResult getDetailAction(string id)
        {
            var message = "";
            int _id = Int32.Parse(id);
            try
            {

                var model = entities.TB_GAME_ACCOUNT.Where(x => x.ID == _id).Select(x => new
                {
                    id = x.ID,
                    username = x.USERNAME,
                    tuong = x.TB_GAME_ACCOUNT_DETAIL.GENERAL_NUMBER,
                    skin = x.TB_GAME_ACCOUNT_DETAIL.SKIN_NUMBER,
                    ngoc = x.TB_GAME_ACCOUNT_DETAIL.GEM_NUMBER,
                    hangb = x.TB_GAME_ACCOUNT_DETAIL.TB_RANK_NAME.RANK_NAME,
                    hangc = x.TB_GAME_ACCOUNT_DETAIL.TB_RANK_NAME1.RANK_NAME,
                    clan = x.TB_GAME_ACCOUNT_DETAIL.CLAN_NAME,
                    gia = x.ORIGINAL_PRICE,
                    img1 = x.TB_GAME_IMAGE.IMG_1,
                    img2 = x.TB_GAME_IMAGE.IMG_2,
                    img3 = x.TB_GAME_IMAGE.IMG_3,
                    img4 = x.TB_GAME_IMAGE.IMG_4
                }).SingleOrDefault();
                if (model == null)
                {
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult offerPrice(string id) // Tính giá để thương lượng
        {
            int _price = Int32.Parse(id);
            List<int> ListPriceOffer = new List<int>();
            for (int i = 75; i >= 5; i -= 5)
            {
                int tmp = _price - ((_price * i)/100);
                ListPriceOffer.Add(tmp);
            }
            return Json(ListPriceOffer, JsonRequestBehavior.AllowGet);
        }


       // get all with the same price
        public JsonResult GetAllByPrice(string id)
        {
            int _id = Int32.Parse(id);
            bool proxyCreation = entities.Configuration.ProxyCreationEnabled;
            try
            {
                entities.Configuration.ProxyCreationEnabled = false;
                var data = entities.TB_GAME_ACCOUNT.Where(x => x.CURRENT_STATUS == 1).Select(x =>
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
                    }).Take(8);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            finally
            {
                entities.Configuration.ProxyCreationEnabled = proxyCreation;
            }
        }
    }
}