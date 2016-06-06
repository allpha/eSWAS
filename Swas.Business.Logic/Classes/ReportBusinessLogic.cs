namespace Swas.Business.Logic.Classes
{
    using Common;
    using Data.Entity;
    using Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ReportBusinessLogic : BusinessLogicBase
    {
        public ReportBusinessLogic()
            : base()
        {
        }

        public ReportItem LoadDetailedReport(int? id, DateTime? fromDate, DateTime? endDate, List<int> landFillIdSource,
                                                List<int> wasteTypeIdSource, List<int> customerIdSource, bool loadAllWasteType, bool loadAllCustomer, bool loadAllLandfill,
                                                bool loadAllCarNumber, List<int> carNumbers)
        {
            var result = new ReportItem
            {
                ReportData = new List<SolidWasteActReportDetailReportItem>(),
                TotalQuantity = 0M,
                TotalAmount = 0M
            };

            try
            {
                Connect();

                if (customerIdSource == null) customerIdSource = new List<int>();
                if (wasteTypeIdSource == null) wasteTypeIdSource = new List<int>();
                if (landFillIdSource == null) landFillIdSource = new List<int>();
                if (carNumbers == null) carNumbers = new List<int>();

                result.ReportData = (from solidWasteAct in Context.SolidWasteActs
                                     join landfill in Context.Landfills on solidWasteAct.LandfillId equals landfill.Id
                                     join receiver in Context.Receivers on solidWasteAct.ReceiverId equals receiver.Id
                                     join customer in Context.Customers on solidWasteAct.CustomerId equals customer.Id
                                     join region in Context.Regions on landfill.RegionId equals region.Id
                                     join transporter in Context.Transporters on solidWasteAct.TransporterId equals transporter.Id
                                     join solidWasteActDetail in Context.SolidWasteActDetails on solidWasteAct.Id equals solidWasteActDetail.SolidWasteActId into LeftJoinSolidWasteActDetail
                                     from solidWasteActDetail in LeftJoinSolidWasteActDetail.DefaultIfEmpty()
                                     join wasteType in Context.WasteTypes on solidWasteActDetail.WasteTypeId equals wasteType.Id into LeftJoinWasteType
                                     from wasteType in LeftJoinWasteType.DefaultIfEmpty()
                                     where (id.HasValue && solidWasteAct.Id == id.Value) ||
                                            (!id.HasValue &&
                                            ((!fromDate.HasValue) || (fromDate.HasValue && solidWasteAct.ActDate >= fromDate.Value)) &&
                                            ((!endDate.HasValue) || (endDate.HasValue && solidWasteAct.ActDate <= endDate.Value)) &&
                                            ((loadAllLandfill) || landFillIdSource.Contains(solidWasteAct.LandfillId)) &&
                                            ((loadAllCustomer) || (!loadAllCustomer && customerIdSource.Contains(solidWasteAct.CustomerId))) &&
                                            ((loadAllCarNumber) || (!loadAllCarNumber && carNumbers.Contains(solidWasteAct.CustomerId))) &&
                                            ((loadAllWasteType) || (!loadAllWasteType && wasteTypeIdSource.Contains(solidWasteActDetail.WasteTypeId))))
                                     orderby solidWasteAct.ActDate descending
                                     select new SolidWasteActReportDetailReportItem
                                     {
                                         Id = solidWasteAct.Id,
                                         ActDate = solidWasteAct.ActDate,
                                         CustomerCode = customer.Code,
                                         CustomerName = customer.Name,
                                         RegionName = region.Name,
                                         CustomerType = customer.Type,
                                         LandfillName = landfill.Name,
                                         CarNumber = transporter.CarNumber,
                                         ReceiverName = receiver.Name + " " + receiver.LastName,
                                         WasteTypeName = wasteType.Name,
                                         Quantity = solidWasteActDetail == null ? 0M : solidWasteActDetail.Quantity,
                                         UnitPrice = solidWasteActDetail == null ? 0M : solidWasteActDetail.UnitPrice,
                                         Price = solidWasteActDetail == null ? 0M : solidWasteActDetail.Amount
                                     }).ToList();


                foreach (var it in (List<SolidWasteActReportDetailReportItem>)result.ReportData)
                {
                    result.TotalAmount = it.Price;
                    result.TotalQuantity = it.Quantity;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Dispose();
            }

            return result;
        }


        public ReportItem LoadGroupReport(int? id, DateTime? fromDate, DateTime? endDate, List<int> landFillIdSource,
                                        List<int> wasteTypeIdSource, List<int> customerIdSource, bool loadAllWasteType, bool loadAllCustomer, bool loadAllLandfill,
                                        bool loadAllCarNumber, List<int> carNumbers)
        {
            var result = new ReportItem
            {
                ReportData = new List<SolidWasteActTotalSumReportItem>(),
                TotalQuantity = 0M,
                TotalAmount = 0M
            };

            try
            {
                Connect();

                if (customerIdSource == null) customerIdSource = new List<int>();
                if (wasteTypeIdSource == null) wasteTypeIdSource = new List<int>();
                if (landFillIdSource == null) landFillIdSource = new List<int>();
                if (carNumbers == null) carNumbers = new List<int>();


                result.ReportData = (from solidWasteAct in Context.SolidWasteActs
                                     join landfill in Context.Landfills on solidWasteAct.LandfillId equals landfill.Id
                                     join receiver in Context.Receivers on solidWasteAct.ReceiverId equals receiver.Id
                                     join customer in Context.Customers on solidWasteAct.CustomerId equals customer.Id
                                     join region in Context.Regions on landfill.RegionId equals region.Id
                                     join transporter in Context.Transporters on solidWasteAct.TransporterId equals transporter.Id
                                     join solidWasteActDetail in Context.SolidWasteActDetails on solidWasteAct.Id equals solidWasteActDetail.SolidWasteActId into LeftJoinSolidWasteActDetail
                                     from solidWasteActDetail in LeftJoinSolidWasteActDetail.DefaultIfEmpty()
                                     join wasteType in Context.WasteTypes on solidWasteActDetail.WasteTypeId equals wasteType.Id into LeftJoinWasteType
                                     from wasteType in LeftJoinWasteType.DefaultIfEmpty()

                                     where (id.HasValue && solidWasteAct.Id == id.Value) ||
                                            (!id.HasValue &&
                                            ((!fromDate.HasValue) || (fromDate.HasValue && solidWasteAct.ActDate >= fromDate.Value)) &&
                                            ((!endDate.HasValue) || (endDate.HasValue && solidWasteAct.ActDate <= endDate.Value)) &&
                                            ((loadAllLandfill) || landFillIdSource.Contains(solidWasteAct.LandfillId)) &&
                                            ((loadAllCustomer) || (!loadAllCustomer && customerIdSource.Contains(solidWasteAct.CustomerId))) &&
                                            ((loadAllCarNumber) || (!loadAllCarNumber && carNumbers.Contains(solidWasteAct.CustomerId))) &&
                                            ((loadAllWasteType) || (!loadAllWasteType && wasteTypeIdSource.Contains(solidWasteActDetail.WasteTypeId))))
                                     group solidWasteActDetail by new
                                     {
                                         ActDate = solidWasteAct.ActDate.Year,
                                         CusotmerCode = customer.Code,
                                         CustomerName = customer.Name,
                                         RegionName = region.Name,
                                         LandfillName = landfill.Name,
                                         ReceiverName = receiver.Name,
                                         CustomerType = customer.Type,
                                         CarNumber = transporter.CarNumber,
                                         ReceiverLastName = receiver.LastName,
                                         WasteTypeName = wasteType.Name
                                     } into Group
                                     select new SolidWasteActTotalSumReportItem
                                     {
                                         ActDate = Group.Key.ActDate,
                                         CustomerCode = Group.Key.CusotmerCode,
                                         CustomerName = Group.Key.CustomerName,
                                         LandfillName = Group.Key.LandfillName,
                                         RegionName = Group.Key.RegionName,
                                         CustomerType = Group.Key.CustomerType,
                                         WasteTypeName = Group.Key.WasteTypeName,
                                         CarNumber = Group.Key.CarNumber,
                                         ReceiverName = Group.Key.ReceiverName + " " +Group.Key.ReceiverLastName,
                                         Quantity = Group.Sum(a => a == null ? 0 : a.Quantity),
                                         Price = Group.Sum(a => a == null ? 0 : a.Amount)
                                     }).ToList();

                foreach (var it in (List<SolidWasteActTotalSumReportItem>)result.ReportData)
                {
                    result.TotalAmount = it.Price;
                    result.TotalQuantity = it.Quantity;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Dispose();
            }

            return result;
        }


    }
}


