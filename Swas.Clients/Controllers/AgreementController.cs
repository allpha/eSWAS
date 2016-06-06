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
    public class AgreementController : Controller
    {
        [Authorization("Agreement.View")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorization("Agreement.View")]
        public JsonResult Load()
        {
            var result = new List<AgreementItem>();
            var bussinessLogic = new AgreementBusinessLogic();

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
        [Authorization("Agreement.View")]
        public JsonResult LoadCustomer()
        {
            var result = new List<ComboBoxItem>();
            var bussinessLogic = new CustomerBusinessLogic();

            try
            {
                result = bussinessLogic.LoadJuridicalPersons();

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

        [Authorization("Agreement.Create")]
        public ActionResult Create()
        {
            var NewItem = new AgreementViewModel()
            {
                Code = string.Empty,
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
            };

            return View(NewItem);
        }

        [HttpPost]
        [Authorization("Agreement.Create")]
        public JsonResult Create(string code, int customerId, DateTime startDate, DateTime endDate)
        {
            var bussinessLogic = new AgreementBusinessLogic();

            try
            {
                bussinessLogic.Create(new AgreementItem
                {
                    Code = code,
                    CustomerId = customerId,
                    StartDate =  startDate,
                    EndDate = endDate
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

        [Authorization("Agreement.Edit")]
        public ActionResult Edit(int Id)
        {
            var bussinessLogic = new AgreementBusinessLogic();

            try
            {
                var agreementItem = bussinessLogic.Get(Id);

                var model = new AgreementViewModel
                {
                    Id = agreementItem.Id,
                    Code = agreementItem.Code,
                    CustomerId = agreementItem.CustomerId,
                    EndDate = agreementItem.EndDate,
                    StartDate = agreementItem.StartDate
                };

                return View(model);
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
        [Authorization("Agreement.Edit")]
        public JsonResult Edit(int id, string code, int customerId, DateTime startDate, DateTime endDate)
        {
            var bussinessLogic = new AgreementBusinessLogic();

            try
            {
                bussinessLogic.Edit(new AgreementItem {
                    Id = id,
                    Code = code,
                    CustomerId = customerId,
                    StartDate = startDate,
                    EndDate = endDate
                });

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                bussinessLogic = null;
            }

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [Authorization("Agreement.Delete")]
        public ActionResult Delete(int id)
        {

            var bussinessLogic = new AgreementBusinessLogic();

            try
            {
                var agreementItem = bussinessLogic.Get(id);
                var model = new AgreementViewModel
                {
                    Id = agreementItem.Id,
                    Code = agreementItem.Code
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return View();
            }
            finally
            {
                bussinessLogic = null;
            }
        }

        [HttpPost]
        [Authorization("Agreement.Delete")]
        public JsonResult Remove(int Id)
        {
            var bussinessLogic = new AgreementBusinessLogic();

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