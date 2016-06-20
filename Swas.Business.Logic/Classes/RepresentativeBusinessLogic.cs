namespace Swas.Business.Logic.Classes
{
    using Common;
    using Data.Entity;
    using Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RepresentativeBusinessLogic : BusinessLogicBase
    {
        public RepresentativeBusinessLogic()
            : base()
        {
        }

        public List<RepresentativeItem> Load()
        {
            var result = new List<RepresentativeItem>();

            try
            {
                Connect();

                result = (from representative in Context.Representatives
                          select new RepresentativeItem
                          {
                              Id = representative.Id,
                              Name = representative.Name,
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

        public RepresentativeItem Get(int id)
        {
            var result = new RepresentativeItem();

            try
            {
                Connect();

                result = (from representative in Context.Representatives
                          where representative.Id == id
                          select new RepresentativeItem
                          {
                              Id = representative.Id,
                              Name = representative.Name,
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

        public void Insert(RepresentativeItem item)
        {
            try
            {
                Connect();

                Context.Representatives.Add(new Representative
                {
                    Name = item.Name
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

        public void Edit(RepresentativeItem item)
        {
            try
            {
                Connect();

                var representativeInfo = (from representative in Context.Representatives
                                          where representative.Id == item.Id
                                          select representative).FirstOrDefault();

                if (representativeInfo != null)
                {
                    representativeInfo.Name = item.Name;

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

                var representativeInfo = (from representative in Context.Representatives
                                          where representative.Id == id
                                          select representative).FirstOrDefault();

                if (representativeInfo != null)
                {
                    Context.Representatives.Remove(representativeInfo);

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
