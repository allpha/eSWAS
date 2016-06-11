namespace Swas.Clients.Controllers
{
    using Common;
    using Swas.Business.Logic.Classes;
    using Swas.Business.Logic.Entity;
    using Swas.Clients.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;

    [ClientErrorHandler]
    public class UserController : Controller
    {
        [Authorization("User.View")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorization("User.View")]
        public JsonResult Load()
        {
            var result = new List<UserItem>();
            var bussinessLogic = new UserBusinessLogic();

            try
            {
                result = bussinessLogic.Load();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bussinessLogic = null;
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [Authorization("User.View")]
        public JsonResult LoadRegions()
        {
            var result = new List<ComboBoxItem>();
            var bussinessLogic = new RegionBusinessLogic();

            try
            {
                result = bussinessLogic.LoadForUsers();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bussinessLogic = null;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorization("User.View")]
        public JsonResult LoadRoles()
        {
            var result = new List<ComboBoxItem>();
            var bussinessLogic = new RoleBusinessLogic();

            try
            {
                result = bussinessLogic.LoadForUser();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bussinessLogic = null;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [Authorization("User.Insert")]
        public ActionResult Create()
        {
            return View();
        }

        private void ParseRegions(List<int> regions)
        {
            if (regions != null)
            {
                var existsRegion = false;
                foreach (var it in regions)
                    if (it == 0)
                    {
                        existsRegion = true;
                        break;
                    }

                if (existsRegion)
                    regions.Remove(0);
            }

        }

        [HttpPost]
        [Authorization("User.Insert")]
        public JsonResult Create(string userName, string email, bool useEmailAsUserName, int roleId, string firstName, string lastName, string privateNumber, string birthDate, string jobPosition, List<int> regions)
        {
            var bussinessLogic = new UserBusinessLogic();

            try
            {
                ParseRegions(regions);
                bussinessLogic.Create(new UserItem
                {
                    UserName = userName,
                    Email = email,
                    UseEmailAsUserName = useEmailAsUserName,
                    RoleId = roleId,
                    FirstName = firstName,
                    LastName = lastName,
                    PrivateNumber = privateNumber,
                    BirthDate = (string.IsNullOrEmpty(birthDate) ? (DateTime?)null : DateTime.ParseExact(birthDate, "MM/dd/yyyy", null)),
                    JobPosition = jobPosition,
                    Regions = regions == null ? new List<int>() : regions
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bussinessLogic = null;
            }

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [Authorization("User.Edit")]
        public ActionResult Edit(int id)
        {
            return View(new EditViewModel { Id = id });
        }

        [HttpPost]
        [Authorization("User.Edit")]
        public JsonResult Get(int id)
        {
            var bussinessLogic = new UserBusinessLogic();

            try
            {
                var user = bussinessLogic.Get(id);

                return Json(user, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bussinessLogic = null;
            }
        }

        [HttpPost]
        [Authorization("User.Edit")]
        public JsonResult Edit(int id, string userName, string email, bool useEmailAsUserName, int roleId, string firstName, string lastName, string privateNumber, string birthDate, string jobPosition, List<int> regions)
        {
            var bussinessLogic = new UserBusinessLogic();

            try
            {
                ParseRegions(regions);
                bussinessLogic.Edit(new UserItem
                {
                    Id = id,
                    UserName = userName,
                    Email = email,
                    UseEmailAsUserName = useEmailAsUserName,
                    RoleId = roleId,
                    FirstName = firstName,
                    LastName = lastName,
                    PrivateNumber = privateNumber,
                    BirthDate = (string.IsNullOrEmpty(birthDate) ? (DateTime?)null : DateTime.ParseExact(birthDate, "MM/dd/yyyy", null)),
                    JobPosition = jobPosition,
                    Regions = regions == null ? new List<int>() : regions
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bussinessLogic = null;
            }

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [Authorization("User.Delete")]
        public ActionResult Delete(int id)
        {
            return View(new EditViewModel { Id = id });
        }

        [HttpPost]
        [Authorization("User.Delete")]
        public JsonResult Remove(int Id)
        {
            var bussinessLogic = new UserBusinessLogic();

            try
            {
                bussinessLogic.Remove(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bussinessLogic = null;
            }

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [Authorization("User.Disible")]
        public ActionResult DisiblePromt(int id)
        {
            return View(new EditViewModel { Id = id });
        }


        [HttpPost]
        [Authorization("User.Disible")]
        public JsonResult Disible(int id)
        {
            var bussinessLogic = new UserBusinessLogic();

            try
            {
                bussinessLogic.SetDisible(id, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bussinessLogic = null;
            }

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [Authorization("User.Unlock")]
        public ActionResult UnlockPromt(int id)
        {
            return View(new EditViewModel { Id = id });
        }


        [HttpPost]
        [Authorization("User.Unlock")]
        public JsonResult Unlock(int Id)
        {
            var bussinessLogic = new UserBusinessLogic();

            try
            {
                bussinessLogic.Unlock(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bussinessLogic = null;
            }

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [Authorization("User.ChangePassword")]
        public ActionResult PasswordReset(int id)
        {
            return View(new EditViewModel { Id = id });
        }

        [HttpPost]
        [Authorization("User.ChangePassword")]
        public JsonResult ResetPassword(int Id)
        {
            var bussinessLogic = new UserBusinessLogic();

            try
            {
                bussinessLogic.ResetPassword(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bussinessLogic = null;
            }

            return Json("OK", JsonRequestBehavior.AllowGet);
        }


    }
}