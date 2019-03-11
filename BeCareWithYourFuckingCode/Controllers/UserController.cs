using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeCareWithYourFuckingCode.Models;
using System.Security.Cryptography;
using System.Text;
namespace BeCareWithYourFuckingCode.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        WEBACCOUNTEntities2 Entities = new WEBACCOUNTEntities2();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult getLogin()
        {

            return RedirectToAction("Index", "Home");
        }

        public ActionResult getSignUp()
        {

            return View();
        }

        public string CreateID()
        {
            string id = "";
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                int a = rand.Next() % 10;
                a = a < 0 ? -1 * a : a;
                id += a.ToString();
            }
            TB_USER user = Entities.TB_USER.Find(id);
            if (user != null) return CreateID();
            return id;
        }

        public string toMD5(string pass)
        {
            //Tạo MD5 
            MD5 mh = MD5.Create();
            //Chuyển kiểu chuổi thành kiểu byte
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(pass);
            //mã hóa chuỗi đã chuyển
            byte[] hash = mh.ComputeHash(inputBytes);
            //tạo đối tượng StringBuilder (làm việc với kiểu dữ liệu lớn)
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult getSignUp(TB_USER user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //TB_USER CurUser = new TB_USER();
                    //CurUser.ID = CreateID();
                    //CurUser.USERNAME = user.USERNAME;
                    //CurUser.PASSWORD_KEY = toMD5(user.PASSWORD_KEY);
                    //CurUser.NAME = user.NAME;
                    //CurUser.PHONE = user.PHONE;
                    //CurUser.CURRENT_ADDRESS = user.CURRENT_ADDRESS;
                    //CurUser.EMAIL = user.EMAIL;
                    user.ID = CreateID();
                    string tmp = user.PASSWORD_KEY;
                    user.PASSWORD_KEY = toMD5(tmp);
                    Entities.TB_USER.Add(user);
                    Entities.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }


        }
    }
}