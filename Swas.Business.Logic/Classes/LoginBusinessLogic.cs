namespace Swas.Business.Logic.Classes
{
    using Common;
    using Data.Access.Context;
    using Data.Entity;
    using Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LoginBusinessLogic : BusinessLogicBase
    {
        public LoginBusinessLogic()
            : base()
        {
        }

        public LoginBusinessLogic(DataContext dataContext)
            : base(dataContext)
        {
        }

        public UserInfo Login(string userName, string password)
        {
            var result = (UserInfo)null;

            try
            {
                Connect();
                password = CryptoProvider.ComputeMD5Hash(password);
                userName = userName.ToLower().Trim();
                var userInfo = (from user in Context.Users
                                where user.UserName == userName && user.Password == password && user.IsLocked == false && user.IsDisabled == false
                                select user).FirstOrDefault();

                if (userInfo == null)
                    throw new Exception("ვერ ხერხდება სისტემაში შესვლა. მომხმარებელი არ არის რეგისტრირებული");
                else
                {
                    result = new UserInfo
                    {
                        SessionId = Guid.NewGuid(),
                        UserName = userInfo.UserName,
                        FirstName = userInfo.FirstName,
                        LastName = userInfo.LastName,
                        Email = userInfo.Email,
                    };

                    result.Permissions = (from rolePermission in Context.RolePermissions
                                          join permission in Context.Permissions on rolePermission.PermissionId equals permission.Id
                                          where rolePermission.RoleId == userInfo.RoleId
                                          select permission.Name).ToList();

                    userInfo.SeassionId = result.SessionId;
                    userInfo.LastActivityDate = DateTime.Now;
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

            return result;
        }
    }
}
