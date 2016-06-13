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
using Swas.Business.Logic.Entity;
using System.Collections.Generic;

namespace Swas.Clients.Controllers
{
    [ClientErrorHandler]
    public class SolidWasteActHistoryController : Controller
    {
        [Authorization("SolidWasteActJunal.History")]

        public ActionResult Index(int solidWasteActId)
        {
            return View(new EditViewModel { Id = solidWasteActId});
        }

        [Authorization("SolidWasteActJunal.History")]
        [HttpPost]
        public JsonResult Load(int solidWasteActId)
        {
            var result = new List<SolidWasteActHistoryItem>();
            var bussinessLogic = new SolidWasteActHistoryBusinessLogic();

            try
            {
                result = bussinessLogic.Load(solidWasteActId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bussinessLogic = null;
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [Authorization("SolidWasteActJunal.History")]
        public ActionResult LoadHistory(int historyId)
        {
            var result = new HistoryViewModel { Content = string.Empty };
            var bussinessLogic = new SolidWasteActHistoryBusinessLogic();

            try
            {
                result.Content = bussinessLogic.Get(historyId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bussinessLogic = null;
            }

            return View(result);
        }

    }
}