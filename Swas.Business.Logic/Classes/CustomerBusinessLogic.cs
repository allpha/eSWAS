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

        public List<CustomerRootItem> LoadForSearch()
        {
            var result = new List<CustomerRootItem>();

            try
            {
                Connect();

                var searchItemSource = (from customer in Context.Customers
                                        orderby new { customer.Type, customer.Code }
                                        select new
                                        {
                                            Id = customer.Id,
                                            Name = customer.Code + " - " + customer.Name,
                                            Type = customer.Type
                                        }).ToList();

                var dictionary = new Dictionary<string, List<CustomerChildItem>>();

                foreach (var item in searchItemSource)
                {
                    var customerName = string.Empty;
                    switch ((CustomerType)item.Type)
                    {
                        case CustomerType.Municipal:
                            {
                                customerName = "მუნიციპალიტეტები";
                                break;
                            }
                        case CustomerType.Juridical:
                            {
                                customerName = "იურიდიული პირები";
                                break;
                            }
                        case CustomerType.Personal:
                            {
                                customerName = "ფიზიკური პირები";
                                break;
                            }
                    }

                    if (dictionary.ContainsKey(customerName))
                        dictionary[customerName].Add(new CustomerChildItem
                        {
                            Id = item.Id,
                            Name = item.Name
                        });
                    else
                        dictionary.Add(customerName, new List<CustomerChildItem>() { new CustomerChildItem { Id = item.Id, Name = item.Name } });

                }

                foreach (var item in dictionary)
                    result.Add(new CustomerRootItem
                    {
                        TypeDescription = item.Key,
                        ChildItemSource = item.Value
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

            return result;
        }
        public List<CustomerSearchItem> FindByCode(int customerType)
        {
            var result = new List<CustomerSearchItem>();

            try
            {
                Connect();

                var searchItemSource = (from customerRepresentative in Context.CustomerRepresentatives
                                        join customer in Context.Customers on customerRepresentative.CustomerId equals customer.Id
                                        join representative in Context.Representatives on customerRepresentative.RepresentativeId equals representative.Id
                                        join transporter in Context.Transporters on customerRepresentative.TransporterId equals transporter.Id
                                        where customer.Type == customerType
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
        public List<CustomerSearchItem> FindByName(int customerType)
        {
            var result = new List<CustomerSearchItem>();

            try
            {
                Connect();

                var searchItemSource = (from customerRepresentative in Context.CustomerRepresentatives
                                        join customer in Context.Customers on customerRepresentative.CustomerId equals customer.Id
                                        join representative in Context.Representatives on customerRepresentative.RepresentativeId equals representative.Id
                                        join transporter in Context.Transporters on customerRepresentative.TransporterId equals transporter.Id
                                        where customer.Type == customerType
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

        public List<ComboBoxItem> LoadJuridicalPersons()
        {
            var result = new List<ComboBoxItem>();

            try
            {
                Connect();

                result = (from customer in Context.Customers
                                        where customer.Type == (int)CustomerType.Juridical
                                        orderby customer.Code ascending
                                        select new ComboBoxItem
                                        {
                                            Id = customer.Id,
                                            Name = customer.Code + " - " + customer.Name,
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
