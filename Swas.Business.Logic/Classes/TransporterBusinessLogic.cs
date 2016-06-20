namespace Swas.Business.Logic.Classes
{
    using Common;
    using Data.Entity;
    using Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TransporterBusinessLogic : BusinessLogicBase
    {
        public TransporterBusinessLogic()
            : base()
        {
        }
        public List<TransporterSearchItem> FindByCarNumber(string findText)
        {
            var result = new List<TransporterSearchItem>();

            try
            {
                Connect();

                findText = findText.Trim();

                var searchItemSource = (from transporter in Context.Transporters
                                        where transporter.CarNumber.Contains(findText)
                                        select new
                                        {
                                            CarNumber = transporter.CarNumber,
                                            CarModel = transporter.CarModel,
                                            DriverInfo = transporter.DriverInfo,
                                        }).ToList();

                result = (from item in searchItemSource
                          select new TransporterSearchItem
                          {
                              Description = String.Format("{0} - {1}, {2}", item.CarNumber.Trim(), item.CarModel.Trim(), item.DriverInfo),
                              CarNumber = item.CarNumber,
                              CarModel = item.CarModel,
                              DriverInfo = item.DriverInfo,
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

        public List<string> FindCarModel(string findText)
        {
            var result = new List<string>();

            try
            {
                Connect();

                findText = findText.Trim();

                result = (from transporter in Context.Transporters
                                        where transporter.CarModel.Contains(findText)
                                        group transporter.CarModel by transporter.CarModel into g
                                        select g.Key).ToList();

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

        public List<ComboBoxItem> LoadCarModelForSolidWasteAct()
        {
            var result = new List<ComboBoxItem>();

            try
            {
                Connect();

                result = (from transporter in Context.Transporters
                          select new ComboBoxItem
                          {
                              Id = transporter.Id,
                              Name = transporter.CarNumber
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
        public List<TransporterItem> Load()
        {
            var result = new List<TransporterItem>();

            try
            {
                Connect();

                result = (from transporter in Context.Transporters
                          select new TransporterItem
                          {
                              Id = transporter.Id,
                              CarModel = transporter.CarModel,
                              CarNumber = transporter.CarNumber,
                              DriverInfo = transporter.DriverInfo,
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

        public TransporterItem Get(int id)
        {
            var result = new TransporterItem();

            try
            {
                Connect();

                result = (from transporter in Context.Transporters
                          where transporter.Id == id
                          select new TransporterItem
                          {
                              Id = transporter.Id,
                              CarModel = transporter.CarModel,
                              CarNumber = transporter.CarNumber,
                              DriverInfo = transporter.DriverInfo,
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

        public void Insert(TransporterItem item)
        {
            try
            {
                Connect();

                Context.Transporters.Add(new Transporter
                {
                    CarNumber = item.CarNumber,
                    CarModel = item.CarModel,
                    DriverInfo = item.DriverInfo,
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

        public void Edit(TransporterItem item)
        {
            try
            {
                Connect();

                var transporterInfo = (from transporter in Context.Transporters
                                          where transporter.Id == item.Id
                                          select transporter).FirstOrDefault();

                if (transporterInfo != null)
                {
                    transporterInfo.CarNumber= item.CarNumber;
                    transporterInfo.CarModel = item.CarModel;
                    transporterInfo.DriverInfo = item.DriverInfo;

                    Context.SaveChanges();
                }
                else
                {
                    throw new Exception("ჩანაწერი ვერ მოიძებნა");
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
        }

        public void Remove(int id)
        {
            try
            {
                Connect();

                var transporterInfo = (from transporter in Context.Transporters
                                       where transporter.Id == id
                                       select transporter).FirstOrDefault();


                if (transporterInfo != null)
                {
                    Context.Transporters.Remove(transporterInfo);

                    Context.SaveChanges();
                }
                else
                {
                    throw new Exception("ჩანაწერი ვერ მოიძებნა");
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
        }


    }
}
