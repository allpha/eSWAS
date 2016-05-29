namespace Swas.Business.Logic.Classes
{
    using Common;
    using Data.Access.Context;
    using Data.Entity;
    using Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RoleBusinessLogic : BusinessLogicBase
    {
        public RoleBusinessLogic()
            : base()
        {
        }

        public RoleBusinessLogic(DataContext dataContext)
            : base(dataContext)
        {
        }

        public List<RoleItem> Load()
        {
            var result = new List<RoleItem>();

            try
            {
                Connect();

                result = (from role in Context.Roles
                          select new RoleItem
                          {
                              Id = role.Id,
                              Description = role.Description,
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

        public void Create(RoleItem item)
        {
            try
            {
                Connect();

                if (item != null)
                {
                    var role = new Role() { Description = item.Description };
                    Context.Roles.Add(role);
                    Context.SaveChanges();

                    if (item.RolePermissions != null)
                        foreach (var permission in item.RolePermissions)
                            Context.RolePermissions.Add(new RolePermission { RoleId = item.Id, PermissionId = permission.PermissionId });
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

        public RoleItem Get(int Id)
        {
            var result = (RoleItem)null;

            try
            {
                Connect();

                result = (from role in Context.Roles
                          where role.Id == Id
                          select new RoleItem
                          {
                              Id = role.Id,
                              Description = role.Description,
                          }).FirstOrDefault();


                if (result != null)
                {
                    result.RolePermissions = (from rolePermission in Context.RolePermissions
                                              join permission in Context.Permissions on rolePermission.PermissionId equals permission.Id
                                              where rolePermission.RoleId == Id
                                              select new RolePermissionItem
                                              {
                                                  PermissionId = rolePermission.PermissionId,
                                                  PermissionDescription = permission.Description
                                              }).ToList();
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

        public void Edit(RoleItem item)
        {
            try
            {
                Connect();

                var editItem = (from role in Context.Roles
                                where role.Id == item.Id
                                select role).FirstOrDefault();

                if (editItem != null)
                {
                    editItem.Description = item.Description;

                    var rolePermissions = (from rolePermission in Context.RolePermissions
                                           where rolePermission.RoleId == item.Id
                                           select rolePermission).ToList();

                    foreach (var rolePermission in rolePermissions)
                        Context.RolePermissions.Remove(rolePermission);

                    if (item.RolePermissions != null)
                        foreach (var rolePermission in item.RolePermissions)
                            Context.RolePermissions.Add(new RolePermission
                            {
                                RoleId = item.Id,
                                PermissionId = rolePermission.PermissionId
                            });

                    Context.SaveChanges();
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

        public void Remove(int roleId)
        {
            try
            {
                Connect();

                var deleteItem = (from role in Context.Roles
                                  where role.Id == roleId
                                  select role).FirstOrDefault();

                if (deleteItem != null)
                {

                    var rolePermissions = (from rolePermission in Context.RolePermissions
                                           where rolePermission.RoleId == roleId
                                           select rolePermission).ToList();

                    foreach (var rolePermission in rolePermissions)
                        Context.RolePermissions.Remove(rolePermission);

                    Context.Roles.Remove(deleteItem);
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
