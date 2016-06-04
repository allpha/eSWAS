namespace Swas.Business.Logic.Classes
{
    using Common;
    using Data.Access.Context;
    using Data.Entity;
    using Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UserBusinessLogic : BusinessLogicBase
    {
        public UserBusinessLogic()
            : base()
        {
        }

        public UserBusinessLogic(DataContext dataContext)
            : base(dataContext)
        {
        }

        private int _maxAttamptPassword = 5;

        public List<UserItem> Load()
        {
            var result = new List<UserItem>();

            try
            {
                Connect();

                result = (from user in Context.Users
                          join role in Context.Roles on user.RoleId equals role.Id
                          select new UserItem
                          {
                              Id = user.Id,
                              UserName = user.UserName,
                              Email = user.Email,
                              UseEmailAsUserName = user.UseEmailAsUserName,
                              IsLocked = user.IsLocked,
                              IsDisabled = user.IsDisabled,
                              FirstName = user.FirstName,
                              LastName = user.LastName,
                              PrivateNumber = user.PrivateNumber,
                              BirthDate = user.BirthDate,
                              JobPosition = user.JobPosition,
                              RoleName = role.Description,
                              ChangePassword = user.ChangePassword
                          }).ToList();

                foreach(var item in result)
                {
                    item.Status = "აქტიური";
                    if (item.IsDisabled)
                        item.Status = "პასიური";
                    else if (item.IsLocked)
                        item.Status = "დაბლოკილი";
                    //else if (item.ChangePassword)
                    //    item.Status = "აქტიური";
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

        public void Create(UserItem item)
        {
            try
            {
                Connect();

                #region Validation

                var checkIfExsistUserName = (from u in Context.Users
                                             where u.UserName == item.UserName
                                             select new
                                             {
                                                 u.Id
                                             }).FirstOrDefault();

                if (checkIfExsistUserName != null)
                    throw new Exception("მომხმარებლის სახელით უკვე არსებობოს სხვა მომხმარებელი");


                if (item.UseEmailAsUserName)
                {
                    var checkIfExistsEmail = (from u in Context.Users
                                              where u.Email == item.Email && u.UseEmailAsUserName
                                              select new
                                              {
                                                  u.Id
                                              }).FirstOrDefault();

                    if (checkIfExistsEmail != null)
                        throw new Exception("არსებული მეილით მომხმარებლის რეგისტრაცია შეუძლებელია. ის გამოიყებება სხვა მომხმარებლისთვის");
                }

                #endregion Validation

                var user = new User
                {
                    BirthDate = item.BirthDate,
                    Email = item.Email,
                    FirstName = item.FirstName,
                    IsDisabled = false,
                    IsLocked = false,
                    JobPosition = item.JobPosition,
                    MaxAttamptPassword = _maxAttamptPassword,
                    LastName = item.LastName,
                    PrivateNumber = item.PrivateNumber,
                    UseEmailAsUserName = item.UseEmailAsUserName,
                    UserName = item.UserName,
                    Password = CryptoProvider.ComputeMD5Hash("123456"),
                    RoleId = item.RoleId,
                    ChangePassword = true
                };

                Context.Users.Add(user);
                Context.SaveChanges();
                if (item.Regions != null)
                    foreach (var region in item.Regions)
                        Context.UserRegions.Add(new UserRegion
                        {
                            RegionId = region,
                            UserId = user.Id
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

        public UserItem Get(int Id)
        {
            var result = (UserItem)null;

            try
            {
                Connect();

                result = (from user in Context.Users
                          where user.Id == Id
                          select new UserItem
                          {
                              UserName = user.UserName,
                              Email = user.Email,
                              UseEmailAsUserName = user.UseEmailAsUserName,
                              IsLocked = user.IsLocked,
                              IsDisabled = user.IsDisabled,
                              FirstName = user.FirstName,
                              LastName = user.LastName,
                              PrivateNumber = user.PrivateNumber,
                              BirthDate = user.BirthDate,
                              JobPosition = user.JobPosition,
                              RoleId = user.RoleId,
                          }).FirstOrDefault();

                if (result != null)
                    result.Regions = (from userRegion in Context.UserRegions
                                      where userRegion.UserId == Id
                                      select userRegion.RegionId).ToList();
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

        public void Edit(UserItem item)
        {
            try
            {
                Connect();

                #region Validation

                var checkIfExsistUserName = (from u in Context.Users
                                             where u.UserName == item.UserName && u.Id != item.Id
                                             select new
                                             {
                                                 u.Id
                                             }).FirstOrDefault();

                if (checkIfExsistUserName != null)
                    throw new Exception("მომხმარებლის სახელით უკვე არსებობოს სხვა მომხმარებელი");


                if (item.UseEmailAsUserName)
                {
                    var checkIfExistsEmail = (from u in Context.Users
                                              where u.Email == item.Email && u.UseEmailAsUserName && u.Id != item.Id
                                              select new
                                              {
                                                  u.Id
                                              }).FirstOrDefault();

                    if (checkIfExistsEmail != null)
                        throw new Exception("არსებული მეილით მომხმარებლის რეგისტრაცია შეუძლებელია. ის გამოიყებება სხვა მომხმარებლისთვის");
                }

                #endregion Validation


                var editItem = (from user in Context.Users
                                where user.Id == item.Id
                                select user).FirstOrDefault();

                if (editItem != null)
                {
                    editItem.UserName = item.UserName;
                    editItem.Email = item.Email;
                    editItem.UseEmailAsUserName = item.UseEmailAsUserName;
                    editItem.FirstName = item.FirstName;
                    editItem.LastName = item.LastName;
                    editItem.PrivateNumber = item.PrivateNumber;
                    editItem.BirthDate = item.BirthDate;
                    editItem.JobPosition = item.JobPosition;
                    editItem.RoleId = item.RoleId;

                    var userRegionSource = (from userRegion in Context.UserRegions
                                            where userRegion.UserId == item.Id
                                            select userRegion).ToList();

                    foreach (var userRegion in userRegionSource)
                        Context.UserRegions.Remove(userRegion);

                    if (item.Regions != null)
                        foreach (var region in item.Regions)
                            Context.UserRegions.Add(new UserRegion
                            {
                                RegionId = region,
                                UserId = item.Id
                            });

                    Context.SaveChanges();
                }
                else
                    throw new Exception("ოპერაცია არ შესრულდა. ვერ მოიძებნა ჩანაწერი");

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

        public void Remove(int id)
        {
            try
            {
                Connect();

                var userRegionDeleteSource = (from userRegion in Context.UserRegions
                                              where userRegion.UserId == id
                                              select userRegion).ToList();

                if (userRegionDeleteSource != null)
                {
                    Context.UserRegions.RemoveRange(userRegionDeleteSource);

                }

                var userDeleteSource = (from user in Context.Users
                                        where user.Id == id
                                        select user).FirstOrDefault();

                if (userDeleteSource != null)
                    Context.Users.Remove(userDeleteSource);
                else
                    throw new Exception("ჩანაწერი ვერ მოიძებნა");

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

        public void Unlock(int id)
        {
            try
            {
                Connect();

                var userItem = (from user in Context.Users
                                where user.Id == id
                                select user).FirstOrDefault();

                if (userItem != null)
                {
                    userItem.IsLocked = false;
                    userItem.IsDisabled = false;
                    userItem.MaxAttamptPassword = _maxAttamptPassword;
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

        public void SetDisible(int id, bool isDisibled)
        {
            try
            {
                Connect();

                var userItem = (from user in Context.Users
                                where user.Id == id
                                select user).FirstOrDefault();

                if (userItem != null)
                {
                    userItem.IsDisabled = isDisibled;
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

        public void ResetPassword(int id)
        {
            try
            {
                Connect();

                var userItem = (from user in Context.Users
                                where user.Id == id
                                select user).FirstOrDefault();

                if (userItem != null)
                {
                    userItem.ChangePassword = true;
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
