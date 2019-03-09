using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeCareWithYourFuckingCode.Models;
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
            // write your code hear
            return RedirectToAction("Index", "Home");
        }

        public ActionResult getSignUp()
        {
           
            return View();
        }

        public ActionResult checkMatchPass(string PASSWORD_KEY, string password2)
        {
            if (string.Compare(PASSWORD_KEY,password2) == 0)
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult getSignUp(TB_USER user)
        {
            if (ModelState.IsValid)
            {
                Entities.TB_USER.Add(user);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
	}
}