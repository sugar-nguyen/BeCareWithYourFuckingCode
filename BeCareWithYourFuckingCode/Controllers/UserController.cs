using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeCareWithYourFuckingCode.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult getLogin()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult getSingUp()
        {
            return View();
        }
	}
}