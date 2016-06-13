namespace Swas.Business.Logic.Classes
{
    using Common;
    using Data.Access.Context;
    using Data.Entity;
    using Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SolidWasteActHistoryBusinessLogic : BusinessLogicBase
    {
        public SolidWasteActHistoryBusinessLogic()
            : base()
        {
        }

        public SolidWasteActHistoryBusinessLogic(DataContext dataContext)
            : base(dataContext)
        {
        }

        public List<SolidWasteActHistoryItem> Load(int solidWasteActId)
        {
            var result = new List<SolidWasteActHistoryItem>();

            try
            {
                Connect();

                var historySource = (from history in Context.SolidWasteActHistories
                                     where history.SolidWasteActId == solidWasteActId
                                     orderby history.CreateDate
                                     select new
                                     {
                                         Id = history.Id,
                                         CreateDate = history.CreateDate
                                     }).ToList();
                var order = 0;
                foreach (var item in historySource)
                    result.Add(new SolidWasteActHistoryItem
                    {
                        Id = item.Id,
                        Order = ++order,
                        CreateDate = item.CreateDate
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

        public string Get(int historyId)
        {
            var result = string.Empty;

            try
            {
                Connect();

                var content = (from history in Context.SolidWasteActHistories
                                     where history.Id == historyId
                                     select new
                                     {
                                         history.Content
                                     }).FirstOrDefault();

                if (content != null)
                    result = content.Content;
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
