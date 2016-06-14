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
                                        where customer.Type == customerType
                                        select new
                                        {
                                            Name = customer.Name,
                                            Code = customer.Code,
                                            ContactInfo = customer.ContactInfo,
                                            RepresentativeName = representative.Name,
                                        }).ToList();

                result = (from item in searchItemSource
                          select new CustomerSearchItem
                          {
                              Description = String.Format("{0} - {1}, {2}", item.Code.Trim(), item.Name.Trim(), item.RepresentativeName.Trim()),
                              Name = item.Name,
                              Code = item.Code,
                              ContactInfo = item.ContactInfo,
                              RepresentativeName = item.RepresentativeName,
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
                                        where customer.Type == customerType
                                        select new
                                        {
                                            Name = customer.Name,
                                            Code = customer.Code,
                                            ContactInfo = customer.ContactInfo,
                                            RepresentativeName = representative.Name,
                                        }).ToList();

                result = (from item in searchItemSource
                          select new CustomerSearchItem
                          {
                              Description = String.Format("{0} - {1}, {2}", item.Code.Trim(), item.Name.Trim(), item.RepresentativeName.Trim()),
                              Name = item.Name,
                              Code = item.Code,
                              ContactInfo = item.ContactInfo,
                              RepresentativeName = item.RepresentativeName,
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

        public List<CustomerItem> Load()
        {
            var result = new List<CustomerItem>();

            try
            {
                Connect();

                result = (from customer in Context.Customers
                          orderby customer.Code ascending
                          select new CustomerItem
                          {
                              Id = customer.Id,
                              Name = customer.Name,
                              Code = customer.Code,
                              Type = (CustomerType)customer.Type,
                              ContactInfo = customer.ContactInfo
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

        public CustomerItem Get(int id)
        {
            var result = new CustomerItem();

            try
            {
                Connect();

                result = (from customer in Context.Customers
                          where customer.Id == id
                          select new CustomerItem
                          {
                              Id = customer.Id,
                              Name = customer.Name,
                              Code = customer.Code,
                              Type = (CustomerType)customer.Type,
                              ContactInfo = customer.ContactInfo
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

        public void Create(CustomerItem item)
        {
            try
            {
                Connect();

                Context.Customers.Add(new Customer
                {
                    Type = (int)item.Type,
                    Code = item.Code,
                    Name = item.Name,
                    ContactInfo = item.ContactInfo
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

        public void Edit(CustomerItem item)
        {
            try
            {
                Connect();

                var customerInfo = (from customer in Context.Customers
                                    where customer.Id == item.Id
                                    select customer).FirstOrDefault();

                customerInfo.Type = (int)item.Type;
                customerInfo.Code = item.Code;
                customerInfo.Name = item.Name;
                customerInfo.ContactInfo = item.ContactInfo;

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

        public void Delete(int id)
        {
            try
            {
                Connect();

                var customerInfo = (from customer in Context.Customers
                                    where customer.Id == id
                                    select customer).FirstOrDefault();

                Context.Customers.Remove(customerInfo);

                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("წაშლა შეუძლებელია. ჩანაწერი უკვე გამოყენებულია");
            }
            finally
            {
                Dispose();
            }
        }

    }
}
