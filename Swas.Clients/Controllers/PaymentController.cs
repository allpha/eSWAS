namespace Swas.Client.Controllers
{
    using Business.Logic.Classes;
    using Business.Logic.Entity;
    using Clients.Common;
    using Clients.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;

    public class PaymentController : Controller
    {
        [Authorization("Payments.View")]
        public ActionResult Index()
        {
            return View();
        }


        [Authorization("Payments.Edit")]
        public ActionResult Edit(int id)
        {
            return View(new EditViewModel { Id = id});
        }

        [HttpPost]
        [Authorization("Payments.Edit")]
        public JsonResult GetPayment(int id)
        {
            var result = (PaymentInfoItem)null;
            var bussinessLogic = new PaymentBusinessLogic();

            try
            {
                result = bussinessLogic.Get(id);
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


        [HttpPost]
        [Authorization("Payments.Edit")]
        public JsonResult SavePayment(int id, List<PaymentHistoryItem> payments)
        {
            var bussinessLogic = new PaymentBusinessLogic();

            try
            {
                bussinessLogic.Insert(id, payments);
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

        #region Filter

        [HttpPost]
        [Authorization("Payments.View")]
        public JsonResult LoadFilterRegions()
        {
            var result = new List<RegionItem>();
            var bussinessLogic = new RegionBusinessLogic();

            try
            {
                result = bussinessLogic.LoadSearchSource(Globals.SessionContext.Current.User.SessionId);
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

        [HttpPost]
        [Authorization("Payments.View")]
        public JsonResult LoadFilterLandfills(bool selectAll, List<int> regionItemSource)
        {
            var result = new List<LandfillItem>();
            var bussinessLogic = new LandfillBusinessLogic();

            try
            {
                result = bussinessLogic.LoadForSearch(selectAll, regionItemSource);
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

        [HttpPost]
        [Authorization("Payments.View")]
        public JsonResult LoadFilterWasteType()
        {
            var result = new List<WasteTypeSmartItem>();
            var bussinessLogic = new WasteTypeBusinessLogic();

            try
            {
                result = bussinessLogic.LoadForSearch();
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

        [HttpPost]
        [Authorization("Payments.View")]
        public JsonResult LoadFilterCustomer()
        {
            var result = new List<CustomerRootItem>();
            var bussinessLogic = new CustomerBusinessLogic();

            try
            {
                result = bussinessLogic.LoadForSearch();
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


        [HttpPost]
        [Authorization("Payments.View")]
        public JsonResult Load(int? id, string fromDate, string endDate, List<int> landFillIdSource,
                                                List<int> wasteTypeIdSource, List<int> customerIdSource,
                                                bool loadAllWasteType, bool loadAllCustomer, bool loadAllLandfill, int pageNumber)
        {
            var result = new List<PaymentInfoItem>();
            var bussinessLogic = new PaymentBusinessLogic();

            try
            {
                result = bussinessLogic.Load(id, ConvertStringToDate(fromDate), ConvertStringToDate(endDate), landFillIdSource, wasteTypeIdSource, customerIdSource, loadAllWasteType, loadAllCustomer, loadAllLandfill, pageNumber);
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

        private DateTime? ConvertStringToDate(string date)
        {
            var result = (DateTime?)null;

            if (!string.IsNullOrEmpty(date))
            {
                var splitSource = date.Split('/');
                if (splitSource.Count() == 3)
                    result = new DateTime(Convert.ToInt32(splitSource[2]), Convert.ToInt32(splitSource[1]), Convert.ToInt32(splitSource[0]));
            }

            return result;
        }

        [HttpPost]
        [Authorization("Payments.View")]
        public JsonResult LoadPageCount(int? id, string fromDate, string endDate, List<int> landFillIdSource,
                                        List<int> wasteTypeIdSource, List<int> customerIdSource,
                                        bool loadAllWasteType, bool loadAllCustomer, bool loadAllLandfill)
        {
            var result = (int)0;
            var bussinessLogic = new PaymentBusinessLogic();

            try
            {
                result = bussinessLogic.LoadPageCount(id, ConvertStringToDate(fromDate), ConvertStringToDate(endDate), landFillIdSource, wasteTypeIdSource, customerIdSource, loadAllWasteType, loadAllCustomer, loadAllLandfill);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bussinessLogic = null;
            }

            return Json(new { pageCount = result }, JsonRequestBehavior.AllowGet);
        }

        #endregion Filter

    }
}



