namespace Swas.Clients.Common
{

    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Linq;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class Authorization : FilterAttribute, IActionFilter, IResultFilter
    {
        #region Constructors

        public Authorization() { }

        public Authorization(string permission)
        {
            PermissionName = permission;
        }

        #endregion

        #region Properties

        private string _permissionName = string.Empty;
        private string PermissionName
        {
            get { return _permissionName; }
            set { _permissionName = value; }
        }

        private string AutorizationUrl
        {
            get { return "/Account/LogIn"; }
        }
        private string ErrorUrl
        {
            get { return "/Account/Error"; }
        }

        #endregion

        #region IResultFilter Members

        public void OnResultExecuted(ResultExecutedContext filterContext) { }

        public void OnResultExecuting(ResultExecutingContext filterContext) { }

        #endregion

        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext) { }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!Globals.UserPrincipal.IsAutorized || Globals.SessionContext.Current.User == null || Globals.SessionContext.Current.User.Permissions == null)
            {
                HttpContext.Current.Session.Clear();
                filterContext.HttpContext.Response.Redirect(AutorizationUrl, true);
                return;
            }

            //if (!Role.HasFlag((UserRole)Globals.SessionContext.Current.User.RoleId))
            //    filterContext.HttpContext.Response.Redirect(String.Format("{0}?error={1}", ErrorUrl, "მოთხოვნილ რესურსზე წვდომა თქვენი მომხმარებლისთვის შეზღუდულია."), true);

            var permission = (from p in Globals.SessionContext.Current.User.Permissions
                              where p == PermissionName
                              select new
                              {
                                  p
                              }).FirstOrDefault();

            if (permission == null)
                filterContext.HttpContext.Response.Redirect(String.Format("{0}", ErrorUrl), true);
        }

        #endregion
    }

}
