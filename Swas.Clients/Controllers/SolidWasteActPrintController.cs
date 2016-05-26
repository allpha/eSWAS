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

    public class SolidWasteActPrintController : Controller
    {
        // GET: SolidWasteAct
        public ActionResult Index(int id)
        {
            return View(loadData(id));
        }

        [HttpPost]
        public JsonResult Load(int id)
        {
            return Json(loadData(id), JsonRequestBehavior.AllowGet);
        }

        private SolidWasteActPrintViewModel loadData(int id)
        {
            var result = new SolidWasteActPrintViewModel();
            var bussinessLogic = new SolidWasteActBusinessLogic();

            try
            {
                var solidWasteItem = bussinessLogic.GetForPrint(id);
                result = new SolidWasteActPrintViewModel()
                {
                    Id = solidWasteItem.Id,
                    ActDate = solidWasteItem.ActDate,
                    LandfillName = solidWasteItem.LandfillName,
                    Remark = solidWasteItem.Remark,
                    CustomerName = solidWasteItem.CustomerName,
                    CustomerCode = solidWasteItem.CustomerCode,
                    CustomerContactInfo = solidWasteItem.CustomerContactInfo,
                    ReceiverName = solidWasteItem.ReceiverName,
                    ReceiverLastName = solidWasteItem.ReceiverLastName,
                    PositionName = solidWasteItem.PositionName,
                    RepresentativeName = solidWasteItem.RepresentativeName,
                    TransporterCarModel = solidWasteItem.TransporterCarModel,
                    TransporterCarNumber = solidWasteItem.TransporterCarNumber,
                    TransporterDriverInfo = solidWasteItem.TransporterDriverInfo,
                    TotalAmount = solidWasteItem.TotalAmount,
                    SolidWasteActDetails = new List<SolidWasteActDetailPrintViewModel>()
                };


                foreach (var item in solidWasteItem.DetailItemSource)
                    result.SolidWasteActDetails.Add(new SolidWasteActDetailPrintViewModel
                    {
                        WasteTypeName = item.WasteTypeName,
                        Amount = item.Amount,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
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

            return result;
        }

    }
}


