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

    }
}
