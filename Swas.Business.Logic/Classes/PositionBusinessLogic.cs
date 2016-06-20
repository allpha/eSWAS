namespace Swas.Business.Logic.Classes
{
    using Common;
    using Data.Entity;
    using Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PositionBusinessLogic : BusinessLogicBase
    {
        public PositionBusinessLogic()
            : base()
        {
        }

        public List<PositionItem> Load()
        {
            var result = new List<PositionItem>();

            try
            {
                Connect();

                result = (from position in Context.Positions
                          select new PositionItem
                          {
                              Id = position.Id,
                              Name = position.Name,
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

        public PositionItem Get(int id)
        {
            var result = new PositionItem();

            try
            {
                Connect();

                result = (from position in Context.Positions
                          where position.Id == id
                          select new PositionItem
                          {
                              Id = position.Id,
                              Name = position.Name,
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

        public void Insert(PositionItem item)
        {
            try
            {
                Connect();

                Context.Positions.Add(new Position
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

        public void Edit(PositionItem item)
        {
            try
            {
                Connect();

                var positionInfo = (from position in Context.Positions
                                    where position.Id == item.Id
                                    select position).FirstOrDefault();

                if (positionInfo != null)
                {
                    positionInfo.Name = item.Name;

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

                var positionInfo = (from position in Context.Positions
                                    where position.Id == id
                                    select position).FirstOrDefault();

                if (positionInfo != null)
                {
                    Context.Positions.Remove(positionInfo);
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
