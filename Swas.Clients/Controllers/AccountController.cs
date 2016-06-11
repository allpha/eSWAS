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
        }


        [AllowAnonymous]
        public ActionResult Logout()
        {
            try
            {
                Globals.SessionContext.Clear();
                return RedirectToAction("LogIn", "Account");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }


        [AllowAnonymous]
        public ActionResult Error()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ChangePasswrod()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult ChangePasswrod(string oldPassword, string newPassword, string retryNewPassword)
        {
            var bussinessLogic = new LoginBusinessLogic();

            try
            {
                bussinessLogic.ChangePassword(Globals.SessionContext.Current.User.SessionId, oldPassword, newPassword, retryNewPassword);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bussinessLogic = null;
            }


            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}