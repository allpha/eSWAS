namespace Swas.Business.Logic.Classes
{
    using Common;
    using Data.Access.Context;
    using Data.Entity;
    using Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PermissionBusinessLogic : BusinessLogicBase
    {
        public PermissionBusinessLogic()
            : base()
        {
        }

        public PermissionBusinessLogic(DataContext dataContext)
            : base(dataContext)
        {
        }

        public List<PermissionItem> Load()
        {
            var result = new List<PermissionItem>();

            try
            {
                Connect();

                result = (from permission in Context.Permissions
                          select new PermissionItem
                          {
                              Id = permission.Id,
                              Description = permission.Description,
                              Name = permission.Name
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

        public void Create(PermissionItem item)
        {
            try
            {
                Connect();
                Context.Permissions.Add(new Permission() { Description = item.Description, Name = item.Name });
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

        public PermissionItem Get(int Id)
        {
            var result = (PermissionItem)null;

            try
            {
                Connect();

                result = (from permission in Context.Permissions
                          where permission.Id == Id
                          select new PermissionItem
                          {
                              Id = permission.Id,
                              Name = permission.Name,
                              Description = permission.Description
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

        public void Edit(PermissionItem item)
        {
            try
            {
                Connect();

                var editItem = (from permission in Context.Permissions
                                where permission.Id == item.Id
                                select permission).FirstOrDefault();
                editItem.Name = item.Name;
                editItem.Description = item.Description;

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

        public void Remove(int permissionId)
        {
            try
            {
                Connect();

                var deleteItem = (from permission in Context.Permissions
                                  where permission.Id == permissionId
                                  select permission).FirstOrDefault();

                if (deleteItem != null)
                {
                    Context.Permissions.Remove(deleteItem);
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
