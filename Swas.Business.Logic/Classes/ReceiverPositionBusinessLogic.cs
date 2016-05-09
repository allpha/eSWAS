namespace Swas.Business.Logic.Classes
{
    using Common;
    using Data.Entity;
    using Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ReceiverPositionBusinessLogic : BusinessLogicBase
    {
        public ReceiverPositionBusinessLogic()
            : base()
        {
        }

        public List<ReceiverPositionSearchItem> Find(string findText)
        {
            var result = new List<ReceiverPositionSearchItem>();

            try
            {
                Connect();

               var searchItemSource = (from receiverPosition in Context.ReceiverPositions
                                        join receiver in Context.Receivers on receiverPosition.ReceiverId equals receiver.Id
                                        join position in Context.Positions on receiverPosition.PositionId equals position.Id
                                        select new
                                        {
                                            receiver.Name,
                                            receiver.LastName,
                                            PositionName = position.Name
                                        }).ToList();

                result = (from item in searchItemSource
                          where String.Format("{0} {1} - {2}", item.Name.Trim(), item.LastName.Trim(), item.PositionName.Trim()).Contains(findText)
                          select new ReceiverPositionSearchItem
                          {
                              Description = String.Format("{0} {1} {2}", item.Name.Trim(), item.LastName.Trim(), item.PositionName.Trim()),
                              ReceiverName =  item.Name,
                              ReceiverLastName = item.LastName,
                              Posistion = item.PositionName
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
