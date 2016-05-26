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
    using System.IO;

    public class ReportingController : Controller
    {

        [HttpPost]
        public JsonResult DetailedReport(int? id, string fromDate, string endDate, List<int> landFillIdSource,
                                                List<int> wasteTypeIdSource, List<int> customerIdSource,
                                                bool loadAllWasteType, bool loadAllCustomer, bool loadAllLandfill)
        {
            var bussinessLogic = new ReportBusinessLogic();
            var result = string.Empty;

            try
            {
                var report = bussinessLogic.LoadDetailedReport(id, ConvertStringToDate(fromDate), ConvertStringToDate(endDate), landFillIdSource, wasteTypeIdSource, customerIdSource, loadAllWasteType, loadAllCustomer, loadAllLandfill);

                IList<SolidWasteActReportDetailReportViewModel> reportModel = new List<SolidWasteActReportDetailReportViewModel>();
                var custoemrTypeDescription = (new CustomerTypeDescriotion()).Description;

                foreach (var item in (List<SolidWasteActReportDetailReportItem>)report.ReportData)
                    reportModel.Add(new SolidWasteActReportDetailReportViewModel
                    {
                        ActDate = item.ActDate,
                        CustomerCode = item.CustomerCode,
                        Id = item.Id,
                        CustomerName = item.CustomerName,
                        LandfillName = item.LandfillName,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        ReceiverName = item.ReceiverName,
                        RegionName = item.RegionName,
                        UnitPrice = item.UnitPrice,
                        WasteTypeName = item.WasteTypeName,
                        CustomerType = custoemrTypeDescription[item.CustomerType]
                    });

                result = ExcelExporter.ExportData(reportModel, "Statistic");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bussinessLogic = null;
            }


            return Json(new { fileName = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GroupedReport(int? id, string fromDate, string endDate, List<int> landFillIdSource,
                                        List<int> wasteTypeIdSource, List<int> customerIdSource,
                                        bool loadAllWasteType, bool loadAllCustomer, bool loadAllLandfill)
        {
            var bussinessLogic = new ReportBusinessLogic();
            var result = string.Empty;

            try
            {
                var report = bussinessLogic.LoadGroupReport(id, ConvertStringToDate(fromDate), ConvertStringToDate(endDate), landFillIdSource, wasteTypeIdSource, customerIdSource, loadAllWasteType, loadAllCustomer, loadAllLandfill);

                IList<SolidWasteActTotalSumReportViewModel> reportModel = new List<SolidWasteActTotalSumReportViewModel>();
                var custoemrTypeDescription = (new CustomerTypeDescriotion()).Description;

                foreach (var item in (List<SolidWasteActTotalSumReportItem>)report.ReportData)
                    reportModel.Add(new SolidWasteActTotalSumReportViewModel
                    {
                        Year = item.ActDate,
                        CustomerCode = item.CustomerCode,
                        CustomerName = item.CustomerName,
                        LandfillName = item.LandfillName,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        ReceiverName = item.ReceiverName,
                        RegionName = item.RegionName,
                        WasteTypeName = item.WasteTypeName,
                        CustomerType = custoemrTypeDescription[item.CustomerType]
                    });

                result = ExcelExporter.ExportData(reportModel, "Statistic");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bussinessLogic = null;
            }
            
            return Json(new { fileName = result }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public void DownloadFile(string fileName)
        {
            var exportFileName = string.Format("{0}Temp\\{1}", System.Web.HttpContext.Current.Request.PhysicalApplicationPath, fileName);

            try
            {
                //Send result
                if (System.IO.File.Exists(exportFileName))
                {
                    System.Web.HttpContext.Current.Response.Clear();
                    System.Web.HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                    System.Web.HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}.xlsx", "SolidWasteActReport"));

                    byte[] buff = null;
                    int Block_Size = 4096;
                    using (FileStream fs = new FileStream(exportFileName, FileMode.OpenOrCreate, FileAccess.Read))
                    {
                        Int32 Buff_Size = Convert.ToInt32(fs.Length);
                        buff = new byte[Block_Size];
                        while (Buff_Size > 0 && System.Web.HttpContext.Current.Response.IsClientConnected)
                        {
                            Int32 Length_Per_Read = fs.Read(buff, 0, Block_Size);
                            System.Web.HttpContext.Current.Response.OutputStream.Write(buff, 0, Length_Per_Read);
                            Buff_Size = Buff_Size - Length_Per_Read;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (System.IO.File.Exists(exportFileName))
                    System.IO.File.Delete(exportFileName);

                System.Web.HttpContext.Current.Response.Flush();
                System.Web.HttpContext.Current.Response.End();
            }

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


    }
}


