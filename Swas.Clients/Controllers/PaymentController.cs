namespace Swas.Client.Controllers
{
    using Business.Logic.Classes;
    using Business.Logic.Entity;
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
        // GET: SolidWasteAct
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Edit(int Id)
        {
            return View();

            //var bussinessLogic = new RegionBusinessLogic();

            //try
            //{
            //    var regionItem = bussinessLogic.Get(Id);

            //    var model = new RegionViewModel
            //    {
            //        Id = regionItem.Id,
            //        Name = regionItem.Name
            //    };

            //    return View(model);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    bussinessLogic = null;
            //}
        }



        #region Filter

        [HttpPost]
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



