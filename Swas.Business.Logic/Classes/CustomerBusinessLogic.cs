namespace Swas.Business.Logic.Classes
{
    using Common;
    using Data.Entity;
    using Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomerBusinessLogic : BusinessLogicBase
    {
        public CustomerBusinessLogic()
            : base()
        {
        }
        public List<CustomerSearchItem> FindByCode(string findText, int customerType)
        {
            var result = new List<CustomerSearchItem>();

            try
            {
                Connect();

                findText = findText.Trim();

                var searchItemSource = (from customerRepresentative in Context.CustomerRepresentatives
                                        join customer in Context.Customers on customerRepresentative.CustomerId equals customer.Id
                                        join representative in Context.Representatives on customerRepresentative.RepresentativeId equals representative.Id
                                        join transporter in Context.Transporters on customerRepresentative.TransporterId equals transporter.Id
                                        where customer.Code.Contains(findText) && customer.Type == customerType
                                        select new
                                        {
                                            Name = customer.Name,
                                            Code = customer.Code,
                                            ContactInfo = customer.ContactInfo,
                                            RepresentativeName = representative.Name,
                                            CarNumber = transporter.CarNumber,
                                            CarModel = transporter.CarModel,
                                            DriverInfo = transporter.DriverInfo,
                                        }).ToList();

                result = (from item in searchItemSource
                          select new CustomerSearchItem
                          {
                              Description = String.Format("{0} - {1}, {2}, {3}, {4}, {5} ", item.Code.Trim(), item.Name.Trim(), item.RepresentativeName.Trim(), item.CarNumber, item.CarModel, item.DriverInfo),
                              Name = item.Name,
                              Code = item.Code,
                              ContactInfo = item.ContactInfo,
                              RepresentativeName = item.RepresentativeName,
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

        public List<CustomerSearchItem> FindByName(string findText, int customerType)
        {
            var result = new List<CustomerSearchItem>();

            try
            {
                Connect();

                findText = findText.Trim();

                var searchItemSource = (from customerRepresentative in Context.CustomerRepresentatives
                                        join customer in Context.Customers on customerRepresentative.CustomerId equals customer.Id
                                        join representative in Context.Representatives on customerRepresentative.RepresentativeId equals representative.Id
                                        join transporter in Context.Transporters on customerRepresentative.TransporterId equals transporter.Id
                                        where customer.Name.Contains(findText) && customer.Type == customerType
                                        select new
                                        {
                                            Name = customer.Name,
                                            Code = customer.Code,
                                            ContactInfo = customer.ContactInfo,
                                            RepresentativeName = representative.Name,
                                            CarNumber = transporter.CarNumber,
                                            CarModel = transporter.CarModel,
                                            DriverInfo = transporter.DriverInfo,
                                        }).ToList();

                result = (from item in searchItemSource
                          select new CustomerSearchItem
                          {
                              Description = String.Format("{0} - {1}, {2}, {3}, {4}, {5} ", item.Code.Trim(), item.Name.Trim(), item.RepresentativeName.Trim(), item.CarNumber, item.CarModel, item.DriverInfo),
                              Name = item.Name,
                              Code = item.Code,
                              ContactInfo = item.ContactInfo,
                              RepresentativeName = item.RepresentativeName,
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

    }
}
