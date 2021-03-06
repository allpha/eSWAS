﻿namespace Swas.Client.Controllers
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

    public class SolidWasteActJurnalController : Controller
    {
        private string Validation(DateTime actDate, int landfillId, string receiverName, string receiverLastName, string positionName, int type,
                                string customerName, string customerCode, string customerContactInfo, string representativeName, string transporterCarNumber,
                                string transporterCarModel, string transporterDriverInfo, string remark)
        {
            var errorText = new StringBuilder();
            if (landfillId == 0) errorText.AppendLine("ნაგავსაყრელის მდებარეობა არ არის შევსებული");
            if (string.IsNullOrEmpty(receiverName)) errorText.AppendLine("მიმღების სახელი არ არის შევსებული");
            if (string.IsNullOrEmpty(receiverLastName)) errorText.AppendLine("მიმღების გვარი არ არის შევსებული");
            if (string.IsNullOrEmpty(positionName)) errorText.AppendLine("მიმღების პოზიცია არ არის შევსებული");
            if (type != 0 && type != 1 && type != 2) errorText.AppendLine("შემომტანის ტიპი არ არის შევსებული");

            return errorText.ToString();

        }

        [Authorization("SolidWasteActJunal.Detail")]
        public ActionResult Detail(int Id)
        {
            return View(new SolidWasteActPrintViewModel
            {
                Id = Id
            });
        }

        [HttpGet]
        [Authorization("SolidWasteActJunal.Edit")]
        public JsonResult LoadCustomerByCode(int CustomerType)
        {
            var result = new List<CustomerSearchItem>();
            var bussinessLogic = new CustomerBusinessLogic();

            try
            {
                result = bussinessLogic.FindByCode(CustomerType);
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

        [HttpGet]
        [Authorization("SolidWasteActJunal.Edit")]
        public JsonResult LoadCustomerByName(int CustomerType)
        {
            var result = new List<CustomerSearchItem>();
            var bussinessLogic = new CustomerBusinessLogic();

            try
            {
                result = bussinessLogic.FindByName(CustomerType);
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
        [Authorization("SolidWasteActJunal.Edit")]
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
        [Authorization("SolidWasteActJunal.Edit")]
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
        [Authorization("SolidWasteActJunal.Edit")]
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

        [Authorization("SolidWasteActJunal.Veiw")]

        public ActionResult Index()
        {
            return View(new List<SolidWasteActInfoViewModel>());
        }

        [Authorization("SolidWasteActJunal.Edit")]
        public ActionResult Edit(int Id)
        {
            var bussinessLogic = new SolidWasteActBusinessLogic();

            try
            {
                var hellperDataSource = bussinessLogic.Get(Globals.SessionContext.Current.SessionId, Id);
                var editItem = hellperDataSource.EditorItem;

                var model = new SolidWasteActViewModel()
                {
                    Id = editItem.Id,
                    ActDate = editItem.ActDate,
                    ReceiverName = editItem.Receiver.Name,
                    ReceiverLastName = editItem.Receiver.LastName,
                    LandfillId = editItem.LandfillId,
                    PositionName = editItem.Position.Name,
                    Remark = editItem.Remark,
                    Type = (int)editItem.Customer.Type,
                    CustomerCode = editItem.Customer.Code,
                    CustomerName = editItem.Customer.Name,
                    CustomerContactInfo = editItem.Customer.ContactInfo,
                    RepresentativeName = editItem.Representative.Name,
                    TransporterCarNumber = editItem.Transporter.CarNumber,
                    TransporterCarModel = editItem.Transporter.CarModel,
                    TransporterDriverInfo = editItem.Transporter.DriverInfo,

                    SolidWasteActDetails = new List<SolidWasteActDetailViewModel>()
                };

                foreach (var item in editItem.SolidWasteActDetails)
                    model.SolidWasteActDetails.Add(new SolidWasteActDetailViewModel
                    {
                        WasteTypeId = item.WasteTypeId,
                        WasteTypeName = item.WasteTypeName,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        Amount = item.Amount
                    });

                model.RecieverItemSource = hellperDataSource.RecieverItemSource;
                model.CustomerItemSource = hellperDataSource.CustomerItemSource;
                model.TransporterItemSource = hellperDataSource.TransporterItemSource;

                LoadTypeItemSource(new List<LandfillItem>() {
                                        new LandfillItem { Id = 0, Name = "მუნიციპალიტეტი" },
                                        new LandfillItem { Id = 1, Name = "იურიდიული პირი" },
                                        new LandfillItem { Id = 2, Name = "ფიზიკური პირი" },
                                    }, 0);

                LoadlandfillItemSource(hellperDataSource.LandfillItemSource, model.LandfillId);

                var selectedWasteTypeId = (int?)null;
                if (hellperDataSource.WasteTypeItemSource.Count > 0)
                    selectedWasteTypeId = hellperDataSource.WasteTypeItemSource[0].Id;


                LoadWasteTypeItemSource(hellperDataSource.WasteTypeItemSource, selectedWasteTypeId);

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
        [Authorization("SolidWasteActJunal.Edit")]
        public JsonResult Edit(int id, DateTime actDate, int landfillId, string receiverName, string receiverLastName, string positionName, int type,
                           string customerName, string customerCode, string customerContactInfo, string representativeName, string transporterCarNumber,
                           string transporterCarModel, string transporterDriverInfo, string remark,
                           List<SolidWasteActDetailViewModel> solidWasteActDetails)
        {
            var result = (int?)null;
            var bussinessLogic = new SolidWasteActBusinessLogic();

            try
            {
                var validationText = Validation(actDate, landfillId, receiverName, receiverLastName, positionName, type,
                        customerName, customerCode, customerContactInfo, representativeName, transporterCarNumber,
                        transporterCarModel, transporterDriverInfo, remark);

                if (!string.IsNullOrEmpty(validationText))
                    throw new Exception(validationText);

                var newItem = new SolidWasteActItem
                {
                    Id = id,
                    ActDate = actDate,
                    LandfillId = landfillId,
                    Receiver = new ReceiverItem
                    {
                        Name = receiverName,
                        LastName = receiverLastName,
                    },
                    Position = new PositionItem
                    {
                        Name = positionName,
                    },
                    Customer = new CustomerItem
                    {
                        Type = (CustomerType)type,
                        Name = customerName,
                        Code = customerCode,
                        ContactInfo = customerContactInfo,
                    },
                    Representative = new RepresentativeItem
                    {
                        Name = representativeName
                    },
                    Transporter = new TransporterItem
                    {
                        CarModel = transporterCarModel,
                        CarNumber = transporterCarNumber,
                        DriverInfo = transporterDriverInfo
                    },
                    Remark = remark,
                    SolidWasteActDetails = new List<SolidWasteActDetailItem>(),
                };

                if (solidWasteActDetails != null)
                    foreach (var item in solidWasteActDetails)
                        newItem.SolidWasteActDetails.Add(new SolidWasteActDetailItem
                        {
                            WasteTypeId = item.WasteTypeId,
                            Quantity = item.Quantity,
                            Amount = item.Amount,
                            UnitPrice = item.UnitPrice
                        });

                result = bussinessLogic.Edit(newItem, ((System.Web.HttpRequestWrapper)this.Request).Url.Authority);

            }
            catch (Exception ex)
            {
                throw ex;
                //return Json(ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
            finally
            {
                bussinessLogic = null;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorization("SolidWasteActJunal.Delete")]
        public ActionResult Delete(int id)
        {
            try
            {
                var model = new SolidWasteActInfoViewModel
                {
                    Id = id
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        [Authorization("SolidWasteActJunal.Delete")]
        public JsonResult DeleteSolidWasteAct(int Id)
        {
            var bussinessLogic = new SolidWasteActBusinessLogic();

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

        #region Filter

        [HttpPost]
        [Authorization("SolidWasteActJunal.Veiw")]
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
        [Authorization("SolidWasteActJunal.Veiw")]
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
        [Authorization("SolidWasteActJunal.Veiw")]
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
        [Authorization("SolidWasteActJunal.Veiw")]
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
        [Authorization("SolidWasteActJunal.Veiw")]
        public JsonResult LoadTransporterCarNumberForFilter()
        {
            var result = new List<ComboBoxItem>();
            var bussinessLogic = new TransporterBusinessLogic();

            try
            {
                result = bussinessLogic.LoadCarModelForSolidWasteAct();
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
        [Authorization("SolidWasteActJunal.Veiw")]
        public JsonResult FilterSolidWasteAct(int? id, string fromDate, string endDate, List<int> landFillIdSource,
                                                List<int> wasteTypeIdSource, List<int> customerIdSource,
                                                bool loadAllWasteType, bool loadAllCustomer, bool loadAllLandfill, int pageNumber,
                                                bool loadAllCarNumber, List<int> carNubmers)
        {
            var result = new List<SolidWasteActInfoViewModel>();
            var bussinessLogic = new SolidWasteActBusinessLogic();

            try
            {
                var itemSource = bussinessLogic.Load(Globals.SessionContext.Current.SessionId, id, ConvertStringToDate(fromDate), ConvertStringToDate(endDate), landFillIdSource, wasteTypeIdSource, customerIdSource, loadAllWasteType, loadAllCustomer, loadAllLandfill, pageNumber, loadAllCarNumber, carNubmers);

                foreach (var item in itemSource)
                    result.Add(new SolidWasteActInfoViewModel
                    {
                        Id = item.Id,
                        ActDate = item.ActDate,
                        Customer = string.Format("{0} {1}", item.CustomerCode, item.CustomerName),
                        LandfillName = item.LandfillName,
                        CarNumber = item.CarNumber,
                        Receiver = string.Format("{0} {1}", item.ReceiverName, item.ReceiverLastName),
                        Quantity = item.Quantity,
                        Price = item.Price
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
        [Authorization("SolidWasteActJunal.Veiw")]
        public JsonResult LoadPageCount(int? id, string fromDate, string endDate, List<int> landFillIdSource,
                                        List<int> wasteTypeIdSource, List<int> customerIdSource,
                                        bool loadAllWasteType, bool loadAllCustomer, bool loadAllLandfill,
                                        bool loadAllCarNumber, List<int> carNubmers)
        {
            var result = (int)0;
            var bussinessLogic = new SolidWasteActBusinessLogic();

            try
            {
                result = bussinessLogic.LoadPageCount(Globals.SessionContext.Current.SessionId, id, ConvertStringToDate(fromDate), ConvertStringToDate(endDate), landFillIdSource, wasteTypeIdSource, customerIdSource, loadAllWasteType, loadAllCustomer, loadAllLandfill, loadAllCarNumber, carNubmers);
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



