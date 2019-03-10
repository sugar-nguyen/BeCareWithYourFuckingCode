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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult getSignUp(TB_USER user)
        {
            if (ModelState.IsValid)
            {
                user.ID = CreateID();
                Entities.TB_USER.Add(user);
                Entities.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
	}
}