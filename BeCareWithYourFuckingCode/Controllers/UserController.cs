using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeCareWithYourFuckingCode.Models;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity.Validation;
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
        public ActionResult getLogin(TB_USER user)
        {
            if (ModelState.IsValid)
            {
                if (CheckUserName(user.USERNAME) == null)
                {
                    ModelState.AddModelError("", "Tên đăng nhập không đúng");
                }
                else
                {
                    TB_USER Valid_User = Entities.TB_USER.Where(x => x.PASSWORD_KEY == user.PASSWORD_KEY).SingleOrDefault();
                    if (Valid_User == null)
                    {
                        ModelState.AddModelError("", "Mật khẩu không đúng");
                    }
                    else
                    {
                        Session["user"] = Valid_User.NAME.ToUpper();
                        return RedirectToAction("Index", Request.Url.AbsoluteUri);

                    }
                }
            }
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

        public TB_USER CheckUserName(string username)
        {
            TB_USER user = Entities.TB_USER.Where(x => x.USERNAME == username).SingleOrDefault();
            return user;
        }

        public TB_USER CheckEmail(string email)
        {
            TB_USER user = Entities.TB_USER.Where(x => x.EMAIL == email).SingleOrDefault();
            return user;
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
                    if (CheckUserName(user.USERNAME) != null)
                    {
                        ModelState.AddModelError("", "Tên đăng nhập đã được sử dụng");
                    }
                    else if (CheckEmail(user.EMAIL) != null)
                    {
                        ModelState.AddModelError("", "Email đã được sử dụng");
                    }
                    else
                    {
                        user.ID = CreateID();
                        Entities.TB_USER.Add(user);
                        Entities.SaveChanges();
                        ViewBag.Success = "Đăng nhập thành công";
                    }


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