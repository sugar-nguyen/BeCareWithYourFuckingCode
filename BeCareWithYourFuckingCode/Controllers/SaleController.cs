using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeCareWithYourFuckingCode.Models;
namespace BeCareWithYourFuckingCode.Controllers
{
    public class SaleController : Controller
    {
        //
        // GET: /Sale/
        private WEBACCOUNTEntities entities = new WEBACCOUNTEntities();
        public ActionResult Sale()
        {
            ViewBag.tbgank = entities.TB_RANK_NAME.ToList();
            return View();
        }
	}
}