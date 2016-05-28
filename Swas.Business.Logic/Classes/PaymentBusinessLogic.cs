namespace Swas.Business.Logic.Classes
{
    using Common;
    using Data.Access.Context;
    using Data.Entity;
    using Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PaymentBusinessLogic : BusinessLogicBase
    {
        public PaymentBusinessLogic()
            : base()
        {
        }

        public PaymentBusinessLogic(DataContext dataContext)
            : base(dataContext)
        {
        }

        private int _recordCount = 25;

        public int LoadPageCount(int? id, DateTime? fromDate, DateTime? endDate, List<int> landFillIdSource,
                          List<int> wasteTypeIdSource, List<int> customerIdSource, bool loadAllWasteType, bool loadAllCustomer, bool loadAllLandfill)
        {
            var result = (int)0;

            try
            {
                Connect();

                if (customerIdSource == null) customerIdSource = new List<int>();
                if (wasteTypeIdSource == null) wasteTypeIdSource = new List<int>();
                if (landFillIdSource == null) landFillIdSource = new List<int>();

                var pageCount = (from solidWasteAct in Context.SolidWasteActs
                                 join landfill in Context.Landfills on solidWasteAct.LandfillId equals landfill.Id
                                 join customer in Context.Customers on solidWasteAct.CustomerId equals customer.Id
                                 join solidWasteActDetail in Context.SolidWasteActDetails on solidWasteAct.Id equals solidWasteActDetail.SolidWasteActId into LeftJoinSolidWasteActDetail
                                 from solidWasteActDetail in LeftJoinSolidWasteActDetail.DefaultIfEmpty()
                                 where (id.HasValue && solidWasteAct.Id == id.Value) ||
                                        (!id.HasValue &&
                                        ((!fromDate.HasValue) || (fromDate.HasValue && solidWasteAct.ActDate >= fromDate.Value)) &&
                                        ((!endDate.HasValue) || (endDate.HasValue && solidWasteAct.ActDate <= endDate.Value)) &&
                                        ((loadAllLandfill) || landFillIdSource.Contains(solidWasteAct.LandfillId)) &&
                                        ((loadAllCustomer) || (!loadAllCustomer && customerIdSource.Contains(solidWasteAct.CustomerId))) &&
                                        ((loadAllWasteType) || (!loadAllWasteType && wasteTypeIdSource.Contains(solidWasteActDetail.WasteTypeId))))
                                 group solidWasteActDetail by new
                                 {
                                     Id = solidWasteAct.Id,
                                 } into Group
                                 select new
                                 {
                                     Id = Group.Key.Id
                                 }).Count();

                result = pageCount / _recordCount;
                if (pageCount % _recordCount != 0)
                    result++;
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

        public List<PaymentInfoItem> Load(int? id, DateTime? fromDate, DateTime? endDate, List<int> landFillIdSource,
                                                List<int> wasteTypeIdSource, List<int> customerIdSource, bool loadAllWasteType, bool loadAllCustomer, bool loadAllLandfill, int pageNumber)
        {
            var result = new List<PaymentInfoItem>();

            try
            {
                Connect();

                if (customerIdSource == null) customerIdSource = new List<int>();
                if (wasteTypeIdSource == null) wasteTypeIdSource = new List<int>();
                if (landFillIdSource == null) landFillIdSource = new List<int>();

                result = (from solidWasteAct in Context.SolidWasteActs
                          join landfill in Context.Landfills on solidWasteAct.LandfillId equals landfill.Id
                          join customer in Context.Customers on solidWasteAct.CustomerId equals customer.Id
                          join solidWasteActDetail in Context.SolidWasteActDetails on solidWasteAct.Id equals solidWasteActDetail.SolidWasteActId into LeftJoinSolidWasteActDetail
                          from solidWasteActDetail in LeftJoinSolidWasteActDetail.DefaultIfEmpty()
                          where (id.HasValue && solidWasteAct.Id == id.Value) ||
                                 (!id.HasValue &&
                                 ((!fromDate.HasValue) || (fromDate.HasValue && solidWasteAct.ActDate >= fromDate.Value)) &&
                                 ((!endDate.HasValue) || (endDate.HasValue && solidWasteAct.ActDate <= endDate.Value)) &&
                                 ((loadAllLandfill) || landFillIdSource.Contains(solidWasteAct.LandfillId)) &&
                                 ((loadAllCustomer) || (!loadAllCustomer && customerIdSource.Contains(solidWasteAct.CustomerId))) &&
                                 ((loadAllWasteType) || (!loadAllWasteType && wasteTypeIdSource.Contains(solidWasteActDetail.WasteTypeId))))
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
                          }).OrderByDescending(a => a.ActId).Skip((pageNumber - 1) * _recordCount).Take(_recordCount).ToList();


                var solidWasteActIdSource = result.Select(a => a.ActId).ToList();

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

                foreach (var item in result)
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


        public PaymentInfoItem Get(int solidWasteActId)
        {
            var result = new PaymentInfoItem()
            {
                HistoryItemSource = new List<PaymentHistoryItem>()
            };

            try
            {
                Connect();

                result = (from solidWasteAct in Context.SolidWasteActs
                          join landfill in Context.Landfills on solidWasteAct.LandfillId equals landfill.Id
                          join customer in Context.Customers on solidWasteAct.CustomerId equals customer.Id
                          where solidWasteAct.Id == solidWasteActId
                          select new PaymentInfoItem
                          {
                              ActId = solidWasteAct.Id,
                              ActDate = solidWasteAct.ActDate,
                              LandfillName = landfill.Name,
                              CustomerCode = customer.Code,
                              CustomerName = customer.Name,
                              CustomerInfo = customer.ContactInfo,
                          }).FirstOrDefault();

                if (result != null)
                {
                    var solidWasteActDetailInfo = (from solidWasteActDetail in Context.SolidWasteActDetails
                                                   where solidWasteActDetail.SolidWasteActId == solidWasteActId
                                                   group solidWasteActDetail by solidWasteActDetail.SolidWasteActId into Group
                                                   select new
                                                   {
                                                       Quantity = Group.Sum(a => a.Quantity),
                                                       Amount = Group.Sum(a => a.Amount),
                                                   }).FirstOrDefault();

                    result.Quantity = 0;
                    result.Price = 0;
                    result.PaidAmount = 0;
                    result.DebtAmount = 0;

                    if (solidWasteActDetailInfo != null)
                    {
                        result.Quantity = solidWasteActDetailInfo.Quantity;
                        result.Price = solidWasteActDetailInfo.Amount;
                        result.DebtAmount = result.Price;
                    }

                    result.HistoryItemSource = (from payment in Context.Payments
                                                where payment.SolidWasteActId == solidWasteActId
                                                orderby payment.PayDate
                                                select new PaymentHistoryItem
                                                {

                                                    PayDate = payment.PayDate,
                                                    Amount = payment.Amount
                                                }).ToList();

                    foreach (var item in result.HistoryItemSource)
                    {
                        result.PaidAmount += item.Amount;
                        result.DebtAmount -= item.Amount;
                    }
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


        public void Insert(int solidWasteActId, decimal amount)
        {
            try
            {
                Connect();

                Context.Payments.Add(new Payment
                {
                    PayDate = DateTime.Now,
                    Amount = amount
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Dispose();
            }
        }
    }
}
