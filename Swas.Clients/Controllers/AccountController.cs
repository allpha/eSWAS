using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Swas.Clients.Models;
using Swas.Business.Logic.Classes;
using Swas.Clients.Common;

namespace Swas.Clients.Controllers
{
    [Authorize]
    [ClientErrorHandler]
    public class AccountController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogIn(string userName, string password)
        {
            var bussinessLogic = new LoginBusinessLogic();

            try
            {
                var userInfo = bussinessLogic.Login(userName, password);
                Globals.SessionContext.SetUser(userInfo);
                //  Response.Redirect("SolidWasteAct/Index",true);
                //return RedirectToAction("Index", "SolidWasteAct");
                return Json(new { ok = true, newurl = Url.Action("Index", "SolidWasteAct") });

            }
            catch (Exception ex)
            {
                Globals.SessionContext.SetPasswordAttampt();
                throw ex;
            }
            finally
            {
                bussinessLogic = null;
            }
            return View();
        }

    }
}