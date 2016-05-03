namespace Swas.Business.Logic.Classes
{
    using Common;
    using Data.Entity;
    using Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LandfillBusinessLogic : BusinessLogicBase
    {
        public LandfillBusinessLogic()
            : base()
        {
        }
        public List<LandfillItem> Load()
        {
            var result = new List<LandfillItem>();

            try
            {
                Connect();

                result = (from landfill in Context.Landfills
                          join region in Context.Regions on landfill.RegionId equals region.Id
                          select new LandfillItem
                          {
                              Id = landfill.Id,
                              Name = landfill.Name,
                              RegionId = region.Id,
                              RegionName = region.Name
                              
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

        public void Create(LandfillItem item)
        {
            try
            {
                Connect();
                Context.Landfills.Add(new Landfill() { Name = item.Name, RegionId = item.RegionId });
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

        public LandfillItem Get(int Id)
        {
            var result = (LandfillItem)null;

            try
            {
                Connect();

                result = (from landfill in Context.Landfills
                                where landfill.Id == Id
                                select new LandfillItem
                                {
                                    Id = landfill.Id,
                                    Name = landfill.Name,
                                    RegionId = landfill.RegionId
                                }).FirstOrDefault();

                result.RegionItemSource = (from region in Context.Regions
                                           select new RegionItem
                                           {
                                               Id = region.Id,
                                               Name = region.Name
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

        public void Edit(LandfillItem item)
        {
            try
            {
                Connect();

                var editItem = (from landfill in Context.Landfills
                                where landfill.Id == item.Id
                                select landfill).FirstOrDefault();
                editItem.Name = item.Name;
                editItem.RegionId = item.RegionId;

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

        public void Remove(int LandfillID)
        {
            try
            {
                Connect();

                var deleteItem = (from landfill in Context.Landfills
                                  where landfill.Id == LandfillID
                                  select landfill).FirstOrDefault();

                if (deleteItem != null)
                {
                    Context.Landfills.Remove(deleteItem);
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
