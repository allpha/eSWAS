
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
    public class RoleController : Controller
    {
        [Authorization("Role.View")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorization("Role.View")]
        public JsonResult Load()
        {
            var result = new List<RoleItem>();
            var bussinessLogic = new RoleBusinessLogic();

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
        public JsonResult LoadPermissions()
        {
            var result = new List<ComboBoxItem>();
            var bussinessLogic = new PermissionBusinessLogic();

            try
            {
                result = bussinessLogic.LoadForRole();
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

        [Authorization("Role.Insert")]
        public ActionResult Create()
        {
            var NewItem = new RoleViewModel
            {
                Description = string.Empty,
            };

            return View(NewItem);
        }

        [HttpPost]
        [Authorization("Role.Insert")]
        public JsonResult Create(string descirption, List<int> permissions)
        {
            var bussinessLogic = new RoleBusinessLogic();

            try
            {
                var role = new RoleItem
                {
                    Description = descirption,
                    RolePermissions = new List<RolePermissionItem>(),
                };


                if (permissions != null)
                {
                    foreach (var permission in permissions)
                        role.RolePermissions.Add(new RolePermissionItem
                        {
                            PermissionId = permission
                        });

                }

                bussinessLogic.Create(role);
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

        [Authorization("Role.Edit")]
        public ActionResult Edit(int Id)
        {
            var bussinessLogic = new RoleBusinessLogic();

            try
            {
                var role = bussinessLogic.Get(Id);

                var model = new RoleViewModel
                {
                    Id = role.Id,
                    Description = role.Description,
                    Periossions = new List<PermissionViewModel>()
                };

                if (role.RolePermissions != null)
                {
                    model.Periossions = new List<PermissionViewModel>();
                    foreach (var permission in role.RolePermissions)
                        model.Periossions.Add(new PermissionViewModel
                        {
                            Id = permission.PermissionId,
                            Description = permission.PermissionDescription
                        });
                }

                return View(model);
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
        [Authorization("Role.Edit")]
        public JsonResult Edit(int id, string descirption, List<int> permissions)
        {
            var bussinessLogic = new RoleBusinessLogic();

            try
            {
                var role = new RoleItem
                {
                    Id = id,
                    Description = descirption,
                    RolePermissions = new List<RolePermissionItem>(),
                };


                if (permissions != null)
                {
                    foreach (var permission in permissions)
                        role.RolePermissions.Add(new RolePermissionItem
                        {
                            PermissionId = permission
                        });
                }

                bussinessLogic.Edit(role);
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

        [Authorization("Role.Delete")]
        public ActionResult Delete(int id)
        {
            var bussinessLogic = new RoleBusinessLogic();

            try
            {
                var regionItem = bussinessLogic.Get(id);
                var model = new RoleViewModel
                {
                    Id = regionItem.Id,
                    Description = regionItem.Description
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return View();
            }
            finally
            {
                bussinessLogic = null;
            }
        }

        [HttpPost]
        [Authorization("Role.Delete")]
        public JsonResult Remove(int Id)
        {
            var bussinessLogic = new RoleBusinessLogic();

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
    }
}