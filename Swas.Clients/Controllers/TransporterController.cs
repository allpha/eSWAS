namespace Swas.Clients.Controllers
{
    using Common;
    using Swas.Business.Logic.Classes;
    using Swas.Business.Logic.Entity;
    using Swas.Clients.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;

    [ClientErrorHandler]
    public class TransporterController : Controller
    {
        [Authorization("Transporter.View")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorization("Transporter.View")]
        public JsonResult Load()
        {
            var result = new List<TransporterItem>();
            var bussinessLogic = new TransporterBusinessLogic();

            try
            {
                result = bussinessLogic.Load();

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

        [Authorization("Transporter.Insert")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorization("Transporter.Insert")]
        public JsonResult Create(string carNumber, string carModel, string driverInfo)
        {
            var bussinessLogic = new TransporterBusinessLogic();

            try
            {
                bussinessLogic.Insert(new TransporterItem
                {
                    CarModel = carModel,
                    CarNumber = carNumber,
                    DriverInfo = driverInfo
                });
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

        [Authorization("Transporter.Edit")]
        public ActionResult Edit(int id)
        {
            return View(new EditViewModel { Id = id });
        }

        [HttpPost]
        [Authorization("Transporter.Edit")]
        public JsonResult Get(int id)
        {
            var bussinessLogic = new TransporterBusinessLogic();

            try
            {
                var user = bussinessLogic.Get(id);

                return Json(user, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bussinessLogic = null;
            }
        }

        [HttpPost]
        [Authorization("Transporter.Edit")]
        public JsonResult Edit(int id, string carNumber, string carModel, string driverInfo)
        {
            var bussinessLogic = new TransporterBusinessLogic();

            try
            {
                bussinessLogic.Edit(new TransporterItem
                {
                    Id = id,
                    CarModel = carModel,
                    CarNumber = carNumber,
                    DriverInfo = driverInfo
                });
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

        [Authorization("Transporter.Delete")]
        public ActionResult Delete(int id)
        {
            return View(new EditViewModel { Id = id });
        }

        [HttpPost]
        [Authorization("Transporter.Delete")]
        public JsonResult Remove(int Id)
        {
            var bussinessLogic = new TransporterBusinessLogic();

            try
            {
                bussinessLogic.Remove(Id);
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