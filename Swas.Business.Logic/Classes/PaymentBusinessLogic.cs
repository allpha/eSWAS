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
                                 join receiver in Context.Receivers on solidWasteAct.ReceiverId equals receiver.Id
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



        public List<SolidWasteActInfoItem> Load(int? id, DateTime? fromDate, DateTime? endDate, List<int> landFillIdSource,
                                                List<int> wasteTypeIdSource, List<int> customerIdSource, bool loadAllWasteType, bool loadAllCustomer, bool loadAllLandfill, int pageNumber)
        {
            var result = new List<SolidWasteActInfoItem>();

            try
            {
                Connect();

                if (customerIdSource == null) customerIdSource = new List<int>();
                if (wasteTypeIdSource == null) wasteTypeIdSource = new List<int>();
                if (landFillIdSource == null) landFillIdSource = new List<int>();

                result = (from solidWasteAct in Context.SolidWasteActs
                          join landfill in Context.Landfills on solidWasteAct.LandfillId equals landfill.Id
                          join receiver in Context.Receivers on solidWasteAct.ReceiverId equals receiver.Id
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
                              LandfillName = landfill.Name,
                              ReceiverName = receiver.Name,
                              ReceiverLastName = receiver.LastName
                          } into Group
                          select new SolidWasteActInfoItem
                          {
                              Id = Group.Key.Id,
                              ActDate = Group.Key.ActDate,
                              CustomerCode = Group.Key.CusotmerCode,
                              CustomerName = Group.Key.CustomerName,
                              LandfillName = Group.Key.LandfillName,
                              ReceiverName = Group.Key.ReceiverName,
                              ReceiverLastName = Group.Key.ReceiverLastName,
                              Quantity = Group.Sum(a => a == null ? 0 : a.Quantity),
                              Price = Group.Sum(a => a == null ? 0 : a.Amount)
                          }).OrderByDescending(a => a.Id).Skip((pageNumber - 1) * _recordCount).Take(_recordCount).ToList();


                var solidWasteActIdSource = result.Select(a => a.Id).ToList();

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
                    if (paymentItemSource.ContainsKey(item.Id))
                    {
                        item.PaydAmount = paymentItemSource[item.Id];
                        item.DebtAmount = item.Price - item.PaydAmount;
                    }
                    else
                    {
                        item.PaydAmount = 0;
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
                          join customer in Context.Customers on solidWasteAct.CustomerId equals customer.Id
                          where solidWasteAct.Id == solidWasteActId
                          select new PaymentInfoItem
                          {
                              ActId = solidWasteAct.Id,
                              ActDate = solidWasteAct.Id,
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
                    result.PayedAmount = 0;
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
                        result.PayedAmount += item.Amount;
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
