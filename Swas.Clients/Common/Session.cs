namespace Swas.Clients.Common
{
    using Business.Logic.Entity;
    using System;
    using System.Web;
    using System.Linq;

    public static class Globals
    {
        public static class Http
        {
            public static class Session
            {
                public const string UserAuthParam = "eSwas.UserAuth";
                public const string PasswordAttamptCount = "eSwas.PasswordAttamptCount";
                public const string Captcha = "eSwas.Captcha";
            }
        }

        public static class Formats
        {
            public static class DateTime
            {
                public const string JavaScriptDate = "yyyy-MM-dd";
                public const string ShortDate = "dd/MM/yyyy";
                public const string LongDate = "dd/MM/yyyy HH:mm:ss";
            }

            public static class Decimal
            {
                public const string DecimalFormat = "#0.00";
                public const string MoneyFormat = "#0.00 GEL";
            }
        }

        public static class UserPrincipal
        {
            public static bool IsAutorized
            {
                get
                {
                    return (SessionContext.Current.User != null);
                }
            }
        }

        public static class SessionContext
        {
            public static class Current
            {
                #region Properties

                public static string Captcha
                {
                    get
                    {
                        return (HttpContext.Current.Session[Http.Session.Captcha] as string);
                    }
                }

                public static UserInfo User
                {
                    get
                    {
                        return (HttpContext.Current.Session[Http.Session.UserAuthParam] as UserInfo);
                    }
                }

                public static int PasswordAttamptCount
                {
                    get
                    {
                        if (HttpContext.Current.Session[Http.Session.PasswordAttamptCount] == null)
                            HttpContext.Current.Session[Http.Session.PasswordAttamptCount] = 0;

                        return Convert.ToInt32(HttpContext.Current.Session[Http.Session.PasswordAttamptCount]);
                    }
                }

                public static Guid SessionId {
                    get
                    {
                        return (HttpContext.Current.Session[Http.Session.UserAuthParam] as UserInfo).SessionId;
                    }
                }
                #endregion
            }

            #region Methods

            public static void Clear()
            {
                HttpContext.Current.Session.Clear();
            }

            public static void SetCaptcha(string value)
            {
                HttpContext.Current.Session[Http.Session.Captcha] = value;
            }

            public static void SetUser(UserInfo user)
            {
                HttpContext.Current.Session[Http.Session.UserAuthParam] = user;
            }

            public static void SetPasswordAttampt()
            {
                HttpContext.Current.Session[Http.Session.PasswordAttamptCount] = Convert.ToInt32(HttpContext.Current.Session[Http.Session.PasswordAttamptCount]) + 1;
            }

            public static bool HasPermission(string permission)
            {
                if (Globals.SessionContext.Current.User == null || Globals.SessionContext.Current.User.Permissions == null)
                    return false;

                var exists = (from p in Globals.SessionContext.Current.User.Permissions
                              where p == permission
                                  select new
                                  {
                                      p
                                  }).FirstOrDefault();

                if (exists != null)
                    return true;

                return false;
            }


            #endregion
        }
    }
}
