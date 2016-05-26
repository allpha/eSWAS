namespace Swas.Business.Logic.Classes
{
    using Common;
    using Data.Access.Context;
    using Data.Entity;
    using Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AgreementBusinessLogic : BusinessLogicBase
    {
        public AgreementBusinessLogic()
            : base()
        {
        }

        public AgreementBusinessLogic(DataContext dataContext)
            : base(dataContext)
        {
        }

        public List<AgreementItem> Load()
        {
            var result = new List<AgreementItem>();

            try
            {
                Connect();

                result = (from agreement in Context.Agreements
                          join customer in Context.Customers on agreement.CustomerId equals customer.Id
                          select new AgreementItem
                          {
                              Id = agreement.Id,
                              Code = agreement.Code,
                              CustomerId = customer.Id,
                              CustomerCode = customer.Code,
                              CustomerName = customer.Name,
                              StartDate = agreement.StartDate,
                              EndDate = agreement.EndDate,
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

        private void AgreementValidation(AgreementItem item)
        {
            if (item.StartDate>=item.EndDate)
                throw new Exception(string.Format("ხელშეკრულების შენახვა შეუძლებელია. ხელშეკრულების დაწყების თარიღი ნაკლები უნდა იყოს დასრულების თარიღზე.", item.Code));

            item.Code = item.Code.Trim();
            var validationByCode = (from agreement in Context.Agreements
                                    where agreement.Code.Trim() == item.Code
                                    select new
                                    {
                                        Id = agreement.Id,
                                        Code = agreement.Code,
                                        CustomerId = agreement.CustomerId,
                                        StartDate = agreement.StartDate,
                                        EndDate = agreement.EndDate
                                    }).FirstOrDefault();

            if (validationByCode != null)
                throw new Exception(string.Format("ხელშეკრულების შენახვა შეუძლებელია. ხელშეკრულება '{0}'-ნომრით უკვე დარეგისტრირებულია.", item.Code));

            var validationByCustomerAndDate = (from agreement in Context.Agreements
                                               where agreement.CustomerId == item.CustomerId &&
                                                     ((item.StartDate <= agreement.StartDate && item.EndDate >= agreement.StartDate && item.EndDate <= agreement.EndDate) ||
                                                     (item.StartDate <= agreement.StartDate && item.EndDate >= agreement.EndDate) ||
                                                     (item.StartDate >= agreement.StartDate && item.StartDate <= agreement.EndDate && item.EndDate <= agreement.EndDate) ||
                                                     (item.StartDate >= agreement.StartDate && item.StartDate <= agreement.EndDate && item.EndDate >= agreement.EndDate))
                                               select new
                                               {
                                                   Id = agreement.Id,
                                                   Code = agreement.Code
                                               }).FirstOrDefault();

            if (validationByCustomerAndDate != null)
            {
                throw new Exception(string.Format("ხელშეკრულების შენახვა შეუძლებელია. არსებულ ხელშეკრულებას '{0}' ხელშეკრულებასთან თან აქვს თანაკვეთა.", validationByCustomerAndDate.Code));
            }
        }

        public void Create(AgreementItem item)
        {
            try
            {
                Connect();
                AgreementValidation(item);
                Context.Agreements.Add(new Agreement()
                {
                    Code = item.Code,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    CustomerId = item.CustomerId
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

        public AgreementItem Get(int Id)
        {
            var result = (AgreementItem)null;

            try
            {
                Connect();
                result = (from agreement in Context.Agreements
                          where agreement.Id == Id
                          select new AgreementItem
                          {
                              Id = agreement.Id,
                              Code = agreement.Code,
                              CustomerId = agreement.CustomerId,
                              StartDate = agreement.StartDate,
                              EndDate = agreement.EndDate,
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

        public void Edit(AgreementItem item)
        {
            try
            {
                Connect();
                AgreementValidation(item);
                var editItem = (from agreement in Context.Agreements
                                where agreement.Id == item.Id
                                select agreement).FirstOrDefault();
                editItem.Code = item.Code;
                editItem.StartDate = item.StartDate;
                editItem.EndDate = item.EndDate;
                editItem.CustomerId = item.CustomerId;

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

        public void Remove(int agreementId)
        {
            try
            {
                Connect();

                var deleteItem = (from agreement in Context.Agreements
                                  where agreement.Id == agreementId
                                  select agreement).FirstOrDefault();

                if (deleteItem != null)
                {
                    Context.Agreements.Remove(deleteItem);
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



    }
}
