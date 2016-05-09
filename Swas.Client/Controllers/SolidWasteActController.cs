namespace Swas.Client.Controllers
{
    using Business.Logic.Classes;
    using Business.Logic.Entity;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class SolidWasteActController : Controller
    {
        // GET: SolidWasteAct
        public ActionResult Index()
        {
            return View();
        }

        //Get: SolidWasteAct / Create
        public ActionResult Create()
        {
            var bussinessLogic = new SolidWasteActBusinessLogic();

            try
            {

                var model = new SolidWasteActViewModel()
                {
                    ActDate = DateTime.Now,
                    SolidWasteActDetails = new List<SolidWasteActDetailViewModel>()
                };

                var hellperDataSource = bussinessLogic.LoadHellperSource();
                var selectedLandfillId = (int?)null;
                var selectedWasteTypeId = (int?)null;
                if (hellperDataSource.LandfillItemSource.Count > 0)
                    selectedLandfillId = hellperDataSource.LandfillItemSource[0].Id;
                if (hellperDataSource.WasteTypeItemSource.Count > 0)
                    selectedWasteTypeId = hellperDataSource.WasteTypeItemSource[0].Id;

                LoadTypeItemSource(new List<LandfillItem>() {
                                        new LandfillItem { Id = 0, Name = "მუნიციპალიტეტი" },
                                        new LandfillItem { Id = 1, Name = "იურიდიული პირი" },
                                        new LandfillItem { Id = 2, Name = "ფიზიკური პირი" },
                                    }, 0);

                LoadlandfillItemSource(hellperDataSource.LandfillItemSource, selectedLandfillId);
                LoadWasteTypeItemSource(hellperDataSource.WasteTypeItemSource, selectedWasteTypeId);

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
        public JsonResult LoadReciever(string Prefix)
        {
            var result = new List<ReceiverPositionSearchItem>();
            var bussinessLogic = new ReceiverPositionBusinessLogic();

            try
            {
                result = bussinessLogic.Find(Prefix.Trim());
            }
            catch (Exception ex)
            {
                return Json(new List<ReceiverPositionSearchItem>(), JsonRequestBehavior.AllowGet);
            }
            finally
            {
                bussinessLogic = null;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LoadCustomerByCode(string Prefix, int CustomerType)
        {
            var result = new List<CustomerSearchItem>();
            var bussinessLogic = new CustomerBusinessLogic();

            try
            {
                result = bussinessLogic.FindByCode(Prefix.Trim(), CustomerType);
            }
            catch (Exception ex)
            {
                return Json(new List<ReceiverPositionSearchItem>(), JsonRequestBehavior.AllowGet);
            }
            finally
            {
                bussinessLogic = null;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LoadCustomerByName(string Prefix, int CustomerType)
        {
            var result = new List<CustomerSearchItem>();
            var bussinessLogic = new CustomerBusinessLogic();

            try
            {
                result = bussinessLogic.FindByName(Prefix.Trim(), CustomerType);
            }
            catch (Exception ex)
            {
                return Json(new List<ReceiverPositionSearchItem>(), JsonRequestBehavior.AllowGet);
            }
            finally
            {
                bussinessLogic = null;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LoadTransporter(string Prefix)
        {
            var result = new List<TransporterSearchItem>();
            var bussinessLogic = new TransporterBusinessLogic();

            try
            {
                result = bussinessLogic.FindByCarNumber(Prefix.Trim());
            }
            catch (Exception ex)
            {
                return Json(new List<ReceiverPositionSearchItem>(), JsonRequestBehavior.AllowGet);
            }
            finally
            {
                bussinessLogic = null;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LoadCarModel(string Prefix)
        {
            var result = new List<string>();
            var bussinessLogic = new TransporterBusinessLogic();

            try
            {
                result = bussinessLogic.FindCarModel(Prefix.Trim());
            }
            catch (Exception ex)
            {
                return Json(new List<ReceiverPositionSearchItem>(), JsonRequestBehavior.AllowGet);
            }
            finally
            {
                bussinessLogic = null;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CalculateWasteAmount(int customerType, int wasteTypeId, decimal quantity, bool isInQubeMeter)
        {
            var result = new SolidWasteActDetailItem();
            var bussinessLogic = new WasteTypeBusinessLogic();

            try
            {
                result = bussinessLogic.CalculateWastePrice((CustomerType)customerType, wasteTypeId, quantity, isInQubeMeter);
            }
            catch (Exception ex)
            {
                return Json(new List<ReceiverPositionSearchItem>(), JsonRequestBehavior.AllowGet);
            }
            finally
            {
                bussinessLogic = null;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private void LoadWasteTypeItemSource(IList<WasteTypeSmartItem> wasteTypeDataSource, object selectedWasteTypeId)
        {
            ViewBag.WasteTypeItemSource = new SelectList(wasteTypeDataSource, "Id", "Name", selectedWasteTypeId);
        }
        
        private void LoadlandfillItemSource(IList<LandfillItem> landfillDataSource, object selectedLandfillId)
        {
            ViewBag.LandfillItemSource = new SelectList(landfillDataSource, "Id", "Name", selectedLandfillId);
        }

        private void LoadTypeItemSource(IList<LandfillItem> typeDataSource, object selectedType)
        {
            ViewBag.LandTypeItemSource = new SelectList(typeDataSource, "Id", "Name", selectedType);
        }

    }
}