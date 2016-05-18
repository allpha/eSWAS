namespace Swas.Business.Logic.Classes
{
    using Common;
    using Data.Entity;
    using Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class WasteTypeBusinessLogic : BusinessLogicBase
    {
        public WasteTypeBusinessLogic()
            : base()
        {
        }

        public List<WasteTypeItem> Load()
        {
            var result = new List<WasteTypeItem>();

            try
            {
                Connect();

                result = (from wasteType in Context.WasteTypes
                          select new WasteTypeItem
                          {
                              Id = wasteType.Id,
                              Name = wasteType.Name,

                              LessQuantity = wasteType.LessQuantity,
                              FromQuantity = wasteType.FromQuantity,
                              EndQuantity = wasteType.EndQuantity,
                              MoreQuantity = wasteType.MoreQuantity,
                              MunicipalityLessQuantityPrice = wasteType.MunicipalityLessQuantityPrice,
                              MunicipalityIntervalQuantityPrice = wasteType.MunicipalityIntervalQuantityPrice,
                              MunicipalityMoreQuantityPrice = wasteType.MunicipalityMoreQuantityPrice,
                              LegalPersonLessQuantityPrice = wasteType.LegalPersonLessQuantityPrice,
                              LegalPersonIntervalQuantityPrice = wasteType.LegalPersonIntervalQuantityPrice,
                              LegalPersonMoreQuantityPrice = wasteType.LegalPersonMoreQuantityPrice,
                              PhysicalPersonLessQuantityPrice = wasteType.PhysicalPersonLessQuantityPrice,
                              PhysicalPersonIntervalQuantityPrice = wasteType.PhysicalPersonIntervalQuantityPrice,
                              PhysicalPersonMoreQuantityPrice = wasteType.PhysicalPersonMoreQuantityPrice,
                              Coeficient = wasteType.Coeficient,
                          }).ToList();
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

        public List<WasteTypeSmartItem> LoadForSearch()
        {
            var result = new List<WasteTypeSmartItem>();

            try
            {
                Connect();

                result = (from wasteType in Context.WasteTypes
                          select new WasteTypeSmartItem
                          {
                              Id = wasteType.Id,
                              Name = wasteType.Name,
                          }).ToList();
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

        public void Create(WasteTypeItem item)
        {
            try
            {
                Connect();

                Context.WasteTypes.Add(new WasteType()
                {
                    Name = item.Name,
                    LessQuantity = item.LessQuantity,
                    FromQuantity = item.FromQuantity,
                    EndQuantity = item.EndQuantity,
                    MoreQuantity = item.MoreQuantity,
                    MunicipalityLessQuantityPrice = item.MunicipalityLessQuantityPrice,
                    MunicipalityIntervalQuantityPrice = item.MunicipalityIntervalQuantityPrice,
                    MunicipalityMoreQuantityPrice = item.MunicipalityMoreQuantityPrice,
                    LegalPersonLessQuantityPrice = item.LegalPersonLessQuantityPrice,
                    LegalPersonIntervalQuantityPrice = item.LegalPersonIntervalQuantityPrice,
                    LegalPersonMoreQuantityPrice = item.LegalPersonMoreQuantityPrice,
                    PhysicalPersonLessQuantityPrice = item.PhysicalPersonLessQuantityPrice,
                    PhysicalPersonIntervalQuantityPrice = item.PhysicalPersonIntervalQuantityPrice,
                    PhysicalPersonMoreQuantityPrice = item.PhysicalPersonMoreQuantityPrice,
                    Coeficient = item.Coeficient,

                });

                Context.SaveChanges();
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

        public WasteTypeItem Get(int Id)
        {
            var result = (WasteTypeItem)null;

            try
            {
                Connect();

                result = (from wasteType in Context.WasteTypes
                          where wasteType.Id == Id
                          select new WasteTypeItem
                          {
                              Id = wasteType.Id,
                              Name = wasteType.Name,
                              LessQuantity = wasteType.LessQuantity,
                              FromQuantity = wasteType.FromQuantity,
                              EndQuantity = wasteType.EndQuantity,
                              MoreQuantity = wasteType.MoreQuantity,
                              MunicipalityLessQuantityPrice = wasteType.MunicipalityLessQuantityPrice,
                              MunicipalityIntervalQuantityPrice = wasteType.MunicipalityIntervalQuantityPrice,
                              MunicipalityMoreQuantityPrice = wasteType.MunicipalityMoreQuantityPrice,
                              LegalPersonLessQuantityPrice = wasteType.LegalPersonLessQuantityPrice,
                              LegalPersonIntervalQuantityPrice = wasteType.LegalPersonIntervalQuantityPrice,
                              LegalPersonMoreQuantityPrice = wasteType.LegalPersonMoreQuantityPrice,
                              PhysicalPersonLessQuantityPrice = wasteType.PhysicalPersonLessQuantityPrice,
                              PhysicalPersonIntervalQuantityPrice = wasteType.PhysicalPersonIntervalQuantityPrice,
                              PhysicalPersonMoreQuantityPrice = wasteType.PhysicalPersonMoreQuantityPrice,
                              Coeficient = wasteType.Coeficient,
                          }).FirstOrDefault();
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

        public void Edit(WasteTypeItem item)
        {
            try
            {
                Connect();

                var editItem = (from wasteType in Context.WasteTypes
                                where wasteType.Id == item.Id
                                select wasteType).FirstOrDefault();

                editItem.Name = item.Name;
                editItem.LessQuantity = item.LessQuantity;
                editItem.FromQuantity = item.FromQuantity;
                editItem.EndQuantity = item.EndQuantity;
                editItem.MoreQuantity = item.MoreQuantity;
                editItem.MunicipalityLessQuantityPrice = item.MunicipalityLessQuantityPrice;
                editItem.MunicipalityIntervalQuantityPrice = item.MunicipalityIntervalQuantityPrice;
                editItem.MunicipalityMoreQuantityPrice = item.MunicipalityMoreQuantityPrice;
                editItem.LegalPersonLessQuantityPrice = item.LegalPersonLessQuantityPrice;
                editItem.LegalPersonIntervalQuantityPrice = item.LegalPersonIntervalQuantityPrice;
                editItem.LegalPersonMoreQuantityPrice = item.LegalPersonMoreQuantityPrice;
                editItem.PhysicalPersonLessQuantityPrice = item.PhysicalPersonLessQuantityPrice;
                editItem.PhysicalPersonIntervalQuantityPrice = item.PhysicalPersonIntervalQuantityPrice;
                editItem.PhysicalPersonMoreQuantityPrice = item.PhysicalPersonMoreQuantityPrice;
                editItem.Coeficient = item.Coeficient;

                Context.SaveChanges();
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

        public void Remove(int wastTypeId)
        {
            try
            {
                Connect();

                var deleteItem = (from wastType in Context.WasteTypes
                                  where wastType.Id == wastTypeId
                                  select wastType).FirstOrDefault();

                if (deleteItem != null)
                {
                    Context.WasteTypes.Remove(deleteItem);
                    Context.SaveChanges();
                }
                else
                    throw new Exception("ჩანაწერი ვერ მოიძებნა");
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

        private decimal WastePrice(decimal quantity, decimal lessQuantity, decimal fromQuantity, decimal endQuantity, decimal moreQuantity, decimal lessQuantityPrice, decimal intervalQuantityPrice, decimal moreQuantityPrice)
        {
            if (quantity <= lessQuantity)
                return lessQuantityPrice;
            else
                if (fromQuantity <= quantity && quantity <= endQuantity)
                return intervalQuantityPrice;
            else
                return moreQuantityPrice;
        }

        public SolidWasteActDetailItem CalculateWastePrice(CustomerType customerType, int wasteTypeId, decimal quantity, bool isInQubeMeter)
        {
            var result = new SolidWasteActDetailItem();

            try
            {
                Connect();

                var wasteTypeInfo = (from wasteType in Context.WasteTypes
                                     where wasteType.Id == wasteTypeId
                                     select new WasteTypeItem
                                     {
                                         Id = wasteType.Id,
                                         Name = wasteType.Name,
                                         LessQuantity = wasteType.LessQuantity,
                                         FromQuantity = wasteType.FromQuantity,
                                         EndQuantity = wasteType.EndQuantity,
                                         MoreQuantity = wasteType.MoreQuantity,
                                         MunicipalityLessQuantityPrice = wasteType.MunicipalityLessQuantityPrice,
                                         MunicipalityIntervalQuantityPrice = wasteType.MunicipalityIntervalQuantityPrice,
                                         MunicipalityMoreQuantityPrice = wasteType.MunicipalityMoreQuantityPrice,
                                         LegalPersonLessQuantityPrice = wasteType.LegalPersonLessQuantityPrice,
                                         LegalPersonIntervalQuantityPrice = wasteType.LegalPersonIntervalQuantityPrice,
                                         LegalPersonMoreQuantityPrice = wasteType.LegalPersonMoreQuantityPrice,
                                         PhysicalPersonLessQuantityPrice = wasteType.PhysicalPersonLessQuantityPrice,
                                         PhysicalPersonIntervalQuantityPrice = wasteType.PhysicalPersonIntervalQuantityPrice,
                                         PhysicalPersonMoreQuantityPrice = wasteType.PhysicalPersonMoreQuantityPrice,
                                         Coeficient = wasteType.Coeficient,
                                     }).FirstOrDefault();

                if (wasteTypeInfo != null)
                {
                    if (isInQubeMeter)
                        quantity *= wasteTypeInfo.Coeficient;

                    result.WasteTypeId = wasteTypeInfo.Id;
                    result.WasteTypeName = wasteTypeInfo.Name;
                    result.Quantity = quantity;

                    switch (customerType)
                    {
                        case CustomerType.Municipal:
                            {
                                result.UnitPrice = WastePrice(quantity, wasteTypeInfo.LessQuantity, wasteTypeInfo.FromQuantity, wasteTypeInfo.EndQuantity, wasteTypeInfo.MoreQuantity, wasteTypeInfo.MunicipalityLessQuantityPrice, wasteTypeInfo.MunicipalityIntervalQuantityPrice, wasteTypeInfo.MunicipalityMoreQuantityPrice);
                                break;
                            }
                        case CustomerType.Juridical:
                            {
                                result.UnitPrice = WastePrice(quantity, wasteTypeInfo.LessQuantity, wasteTypeInfo.FromQuantity, wasteTypeInfo.EndQuantity, wasteTypeInfo.MoreQuantity, wasteTypeInfo.LegalPersonLessQuantityPrice, wasteTypeInfo.LegalPersonIntervalQuantityPrice, wasteTypeInfo.LegalPersonMoreQuantityPrice);
                                break;
                            }

                        case CustomerType.Personal:
                            {
                                result.UnitPrice = WastePrice(quantity, wasteTypeInfo.LessQuantity, wasteTypeInfo.FromQuantity, wasteTypeInfo.EndQuantity, wasteTypeInfo.MoreQuantity, wasteTypeInfo.PhysicalPersonLessQuantityPrice, wasteTypeInfo.PhysicalPersonIntervalQuantityPrice, wasteTypeInfo.PhysicalPersonMoreQuantityPrice);
                                break;
                            }

                    }

                    result.Amount = result.Quantity * result.UnitPrice;

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
