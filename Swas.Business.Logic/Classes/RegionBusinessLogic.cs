namespace Swas.Business.Logic.Classes
{
    using Common;
    using Data.Access.Context;
    using Data.Entity;
    using Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RegionBusinessLogic : BusinessLogicBase
    {
        public RegionBusinessLogic()
            : base()
        {
        }

        public RegionBusinessLogic(DataContext dataContext)
            : base(dataContext)
        {
        }

        public List<RegionItem> Load()
        {
            var result = new List<RegionItem>();

            try
            {
                Connect();

                result = (from region in Context.Regions
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

        public List<RegionSearchItem> LoadSearchSource()
        {
            var result = new List<RegionSearchItem>();

            try
            {
                Connect();

                result = (from region in Context.Regions
                          select new RegionSearchItem
                          {
                              Id = region.Id,
                              Name = region.Name
                          }).ToList();


                var landfillItemSource = (from landfill in Context.Landfills
                                          select new LandfillItem
                                          {
                                              Id = landfill.Id,
                                              Name = landfill.Name,
                                              RegionId = landfill.RegionId
                                          }).ToList();
                var dictionary = new Dictionary<int, List<LandfillItem>>();
                foreach (var item in landfillItemSource)
                    if (dictionary.ContainsKey(item.RegionId))
                        dictionary[item.RegionId].Add(item);
                    else
                        dictionary.Add(item.RegionId, new List<LandfillItem>() { item });


                foreach(var item in result)
                {
                    if (item.LandfillItemSource == null)
                        item.LandfillItemSource = new List<LandfillItem>();

                    if (dictionary.ContainsKey(item.Id))
                        item.LandfillItemSource.AddRange(dictionary[item.Id]);
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

            return result;

        }

        public void Create(RegionItem item)
        {
            try
            {
                Connect();
                Context.Regions.Add(new Region() { Name = item.Name });
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

        public RegionItem Get(int Id)
        {
            var result = (RegionItem)null;

            try
            {
                Connect();

                result = (from region in Context.Regions
                          where region.Id == Id
                          select new RegionItem
                          {
                              Id = region.Id,
                              Name = region.Name
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

        public void Edit(RegionItem item)
        {
            try
            {
                Connect();

                var editItem = (from region in Context.Regions
                                where region.Id == item.Id
                                select region).FirstOrDefault();
                editItem.Name = item.Name;

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

        public void Remove(int regionID)
        {
            try
            {
                Connect();

                var deleteItem = (from region in Context.Regions
                                  where region.Id == regionID
                                  select region).FirstOrDefault();

                if (deleteItem != null)
                {
                    Context.Regions.Remove(deleteItem);
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
