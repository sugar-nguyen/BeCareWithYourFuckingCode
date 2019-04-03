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

        WEBACCOUNTEntities Entities = new WEBACCOUNTEntities();
        // Thông tin 
        public ActionResult getProfile()
        {
            string id = Session["UserID"].ToString();
            var model = Entities.TB_USER.Find(id);
            return View(model);
        }

        public ActionResult getUserProfile()
        {
            if (Session["UserID"] != null)
            {
                string id = Session["UserID"].ToString();
                var model = Entities.TB_USER.Find(id);
                return View("_UserProfile", model);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getDealHistory(TB_BILL his)
        {
            TB_BILL hist=Entities.TB_BILL.Where(x=>x.GAME_ACCOUNT_ID==his.GAME_ACCOUNT_ID.Select(x=>x).SingleOrDefault();
            if()
            {
                var model = Entities.TB_BILL.Where(x => x.GAME_ACCOUNT_ID != null );
                return View("_UserDealHistory", model);
        }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult getLogin(TB_USER user) // đăng nhập
        {
            string message = "";
            TB_USER users = Entities.TB_USER.Where(x => x.USERNAME ==user.USERNAME.Trim() && x.PASSWORD_KEY.Trim() == user.PASSWORD_KEY).Select(x=>x).SingleOrDefault();

            if (users != null)
            {
                Session["UserID"] = users.ID;
                Session["UserName"] = VietHoa(users.NAME);
                message = "success";
            }
            else
            {

                message = "empty";
            }
            if (Request.IsAjaxRequest())
            {
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        //Kiểm tra trạng thái đăng nhập
        public ActionResult checkLogined()
        {
            var status = Session["UserID"] ?? 0;
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getSignOut()
        {
            Session["UserID"] = null;
            Session["UserName"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult getUser(string id) // trả về tên, số dư
        {
            if (id == null) return Json(null, JsonRequestBehavior.AllowGet);
            var model = Entities.TB_USER.Find(id);
            if (model != null)
            {
                var obj = new { name = model.NAME, sodu = model.TB_MONEY.TOTAL_MONEY };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getSignUp() //trả về view đăng ký
        {

            return View();
        }

        public string CreateID() // Cơ chế sinh mã
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

        public static string VietHoa(string s)
        {
            if (String.IsNullOrEmpty(s))
                return s;

            string result = "";

            //lấy danh sách các từ  

            string[] words = s.Split(' ');

            foreach (string word in words)
            {
                // từ nào là các khoảng trắng thừa thì bỏ  
                if (word.Trim() != "")
                {
                    if (word.Length > 1)
                        result += word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower() + " ";
                    else
                        result += word.ToUpper() + " ";
                }

            }
            return result.Trim();
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
        public ActionResult getSignUp(TB_USER user)  // đăng ký
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