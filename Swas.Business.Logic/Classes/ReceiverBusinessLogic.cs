namespace Swas.Business.Logic.Classes
{
    using Common;
    using Data.Entity;
    using Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ReceiverBusinessLogic : BusinessLogicBase
    {
        public ReceiverBusinessLogic()
            : base()
        {
        }

        public List<ReceiverItem> Load()
        {
            var result = new List<ReceiverItem>();

            try
            {
                Connect();

                result = (from reciever in Context.Receivers
                          select new ReceiverItem
                          {
                              Id = reciever.Id,
                              Name = reciever.Name,
                              LastName = reciever.LastName,
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

        public ReceiverItem Get(int id)
        {
            var result = new ReceiverItem();

            try
            {
                Connect();

                result = (from reciever in Context.Receivers
                          where reciever.Id == id
                          select new ReceiverItem
                          {
                              Id = reciever.Id,
                              Name = reciever.Name,
                              LastName = reciever.LastName,
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

        public void Insert(ReceiverItem item)
        {
            try
            {
                Connect();

                Context.Receivers.Add(new Receiver
                {
                    LastName = item.LastName,
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

        public void Edit(ReceiverItem item)
        {
            try
            {
                Connect();

                var recieverInfo = (from reciever in Context.Receivers
                                    where reciever.Id == item.Id
                                    select reciever).FirstOrDefault();

                if (recieverInfo != null)
                {
                    recieverInfo.Name = item.Name;
                    recieverInfo.LastName = item.LastName;
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

                var recieverInfo = (from reciever in Context.Receivers
                                    where reciever.Id == id
                                    select reciever).FirstOrDefault();

                if (recieverInfo != null)
                {
                    Context.Receivers.Remove(recieverInfo);
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
