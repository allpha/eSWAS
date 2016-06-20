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

        public ReportItem LoadDetailedReport(Guid sessionId, int? id, DateTime? fromDate, DateTime? endDate, List<int> landFillIdSource,
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

                if (fromDate.HasValue)
                    fromDate = new DateTime(fromDate.Value.Year, fromDate.Value.Month, fromDate.Value.Day, 0, 0, 0);

                if (endDate.HasValue)
                    endDate = new DateTime(endDate.Value.Year, endDate.Value.Month, endDate.Value.Day, 23, 59, 59);

                result.ReportData = (from solidWasteAct in Context.SolidWasteActs
                                     join landfill in Context.Landfills on solidWasteAct.LandfillId equals landfill.Id
                                     join userRegion in Context.UserRegions on landfill.RegionId equals userRegion.RegionId
                                     join user in Context.Users on userRegion.UserId equals user.Id
                                     join receiver in Context.Receivers on solidWasteAct.ReceiverId equals receiver.Id
                                     join customer in Context.Customers on solidWasteAct.CustomerId equals customer.Id
                                     join region in Context.Regions on landfill.RegionId equals region.Id
                                     join transporter in Context.Transporters on solidWasteAct.TransporterId equals transporter.Id
                                     join solidWasteActDetail in Context.SolidWasteActDetails on solidWasteAct.Id equals solidWasteActDetail.SolidWasteActId into LeftJoinSolidWasteActDetail
                                     from solidWasteActDetail in LeftJoinSolidWasteActDetail.DefaultIfEmpty()
                                     join wasteType in Context.WasteTypes on solidWasteActDetail.WasteTypeId equals wasteType.Id into LeftJoinWasteType
                                     from wasteType in LeftJoinWasteType.DefaultIfEmpty()
                                     where ((id.HasValue && solidWasteAct.Id == id.Value) ||
                                            (!id.HasValue &&
                                            ((!fromDate.HasValue) || (fromDate.HasValue && solidWasteAct.ActDate >= fromDate.Value)) &&
                                            ((!endDate.HasValue) || (endDate.HasValue && solidWasteAct.ActDate <= endDate.Value)) &&
                                            ((loadAllLandfill) || landFillIdSource.Contains(solidWasteAct.LandfillId)) &&
                                            ((loadAllCustomer) || (!loadAllCustomer && customerIdSource.Contains(solidWasteAct.CustomerId))) &&
                                            ((loadAllCarNumber) || (!loadAllCarNumber && carNumbers.Contains(solidWasteAct.CustomerId))) &&
                                            ((loadAllWasteType) || (!loadAllWasteType && wasteTypeIdSource.Contains(solidWasteActDetail.WasteTypeId))))) && user.SeassionId == sessionId
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


        public ReportItem LoadGroupReport(Guid sessionId, int? id, DateTime? fromDate, DateTime? endDate, List<int> landFillIdSource,
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

                if (fromDate.HasValue)
                    fromDate = new DateTime(fromDate.Value.Year, fromDate.Value.Month, fromDate.Value.Day, 0, 0, 0);

                if (endDate.HasValue)
                    endDate = new DateTime(endDate.Value.Year, endDate.Value.Month, endDate.Value.Day, 23, 59, 59);

                result.ReportData = (from solidWasteAct in Context.SolidWasteActs
                                     join landfill in Context.Landfills on solidWasteAct.LandfillId equals landfill.Id
                                     join userRegion in Context.UserRegions on landfill.RegionId equals userRegion.RegionId
                                     join user in Context.Users on userRegion.UserId equals user.Id
                                     join receiver in Context.Receivers on solidWasteAct.ReceiverId equals receiver.Id
                                     join customer in Context.Customers on solidWasteAct.CustomerId equals customer.Id
                                     join region in Context.Regions on landfill.RegionId equals region.Id
                                     join transporter in Context.Transporters on solidWasteAct.TransporterId equals transporter.Id
                                     join solidWasteActDetail in Context.SolidWasteActDetails on solidWasteAct.Id equals solidWasteActDetail.SolidWasteActId into LeftJoinSolidWasteActDetail
                                     from solidWasteActDetail in LeftJoinSolidWasteActDetail.DefaultIfEmpty()
                                     join wasteType in Context.WasteTypes on solidWasteActDetail.WasteTypeId equals wasteType.Id into LeftJoinWasteType
                                     from wasteType in LeftJoinWasteType.DefaultIfEmpty()

                                     where ((id.HasValue && solidWasteAct.Id == id.Value) ||
                                            (!id.HasValue &&
                                            ((!fromDate.HasValue) || (fromDate.HasValue && solidWasteAct.ActDate >= fromDate.Value)) &&
                                            ((!endDate.HasValue) || (endDate.HasValue && solidWasteAct.ActDate <= endDate.Value)) &&
                                            ((loadAllLandfill) || landFillIdSource.Contains(solidWasteAct.LandfillId)) &&
                                            ((loadAllCustomer) || (!loadAllCustomer && customerIdSource.Contains(solidWasteAct.CustomerId))) &&
                                            ((loadAllCarNumber) || (!loadAllCarNumber && carNumbers.Contains(solidWasteAct.CustomerId))) &&
                                            ((loadAllWasteType) || (!loadAllWasteType && wasteTypeIdSource.Contains(solidWasteActDetail.WasteTypeId))))) && user.SeassionId == sessionId
                                     group solidWasteActDetail by new
                                     {
                                         Id = solidWasteAct.Id,
                                         ActDate = solidWasteAct.ActDate.Year,
                                         CusotmerCode = customer.Code,
                                         CustomerName = customer.Name,
                                         RegionName = region.Name,
                                         LandfillName = landfill.Name,
                                         ReceiverName = receiver.Name,
                                         CustomerType = customer.Type,
                                         CarNumber = transporter.CarNumber,
                                         ReceiverLastName = receiver.LastName,
                                     } into Group
                                     select new SolidWasteActTotalSumReportItem
                                     {
                                         Id = Group.Key.Id,
                                         ActDate = Group.Key.ActDate,
                                         CustomerCode = Group.Key.CusotmerCode,
                                         CustomerName = Group.Key.CustomerName,
                                         LandfillName = Group.Key.LandfillName,
                                         RegionName = Group.Key.RegionName,
                                         CustomerType = Group.Key.CustomerType,
                                         CarNumber = Group.Key.CarNumber,
                                         ReceiverName = Group.Key.ReceiverName + " " + Group.Key.ReceiverLastName,
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

        public ReportItem LoadPayment(Guid sessionId, int? id, DateTime? fromDate, DateTime? endDate, List<int> landFillIdSource,
                                        List<int> wasteTypeIdSource, List<int> customerIdSource, bool loadAllWasteType, bool loadAllCustomer, bool loadAllLandfill)
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

                if (fromDate.HasValue)
                    fromDate = new DateTime(fromDate.Value.Year, fromDate.Value.Month, fromDate.Value.Day, 0, 0, 0);

                if (endDate.HasValue)
                    endDate = new DateTime(endDate.Value.Year, endDate.Value.Month, endDate.Value.Day, 23, 59, 59);

                var reportData = (from solidWasteAct in Context.SolidWasteActs
                                  join landfill in Context.Landfills on solidWasteAct.LandfillId equals landfill.Id
                                  join userRegion in Context.UserRegions on landfill.RegionId equals userRegion.RegionId
                                  join user in Context.Users on userRegion.UserId equals user.Id
                                  join customer in Context.Customers on solidWasteAct.CustomerId equals customer.Id
                                  join solidWasteActDetail in Context.SolidWasteActDetails on solidWasteAct.Id equals solidWasteActDetail.SolidWasteActId into LeftJoinSolidWasteActDetail
                                  from solidWasteActDetail in LeftJoinSolidWasteActDetail.DefaultIfEmpty()
                                  where ((id.HasValue && solidWasteAct.Id == id.Value) ||
                                         (!id.HasValue &&
                                         ((!fromDate.HasValue) || (fromDate.HasValue && solidWasteAct.ActDate >= fromDate.Value)) &&
                                         ((!endDate.HasValue) || (endDate.HasValue && solidWasteAct.ActDate <= endDate.Value)) &&
                                         ((loadAllLandfill) || landFillIdSource.Contains(solidWasteAct.LandfillId)) &&
                                         ((loadAllCustomer) || (!loadAllCustomer && customerIdSource.Contains(solidWasteAct.CustomerId))) &&
                                         ((loadAllWasteType) || (!loadAllWasteType && wasteTypeIdSource.Contains(solidWasteActDetail.WasteTypeId))))) && user.SeassionId == sessionId
                                  group solidWasteActDetail by new
                                  {
                                      Id = solidWasteAct.Id,
                                      ActDate = solidWasteAct.ActDate,
                                      CusotmerCode = customer.Code,
                                      CustomerName = customer.Name,
                                      ContactInfo = customer.ContactInfo,
                                      LandfillName = landfill.Name,
                                  } into Group
                                  select new PaymentInfoItem
                                  {
                                      ActId = Group.Key.Id,
                                      ActDate = Group.Key.ActDate,
                                      CustomerCode = Group.Key.CusotmerCode,
                                      CustomerName = Group.Key.CustomerName,
                                      LandfillName = Group.Key.LandfillName,
                                      CustomerInfo = Group.Key.ContactInfo,
                                      Quantity = Group.Sum(a => a == null ? 0 : a.Quantity),
                                      Price = Group.Sum(a => a == null ? 0 : a.Amount)
                                  }).OrderByDescending(a => a.ActId).ToList();


                var solidWasteActIdSource = reportData.Select(a => a.ActId).ToList();

                var paymentItemSource = (from payment in Context.Payments
                                         where solidWasteActIdSource.Contains(payment.SolidWasteActId)
                                         group payment by new
                                         {
                                             payment.SolidWasteActId
                                         } into Group
                                         select new
                                         {
                                             SolidWasteActId = Group.Key.SolidWasteActId,
                                             Amount = Group.Sum(a => a.Amount)
                                         }).ToDictionary(key => key.SolidWasteActId, value => value.Amount);

                foreach (var item in reportData)
                    if (paymentItemSource.ContainsKey(item.ActId))
                    {
                        item.PaidAmount = paymentItemSource[item.ActId];
                        item.DebtAmount = item.Price - item.PaidAmount;
                    }
                    else
                    {
                        item.PaidAmount = 0;
                        item.DebtAmount = 0;
                    }

                result.ReportData = reportData;
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


