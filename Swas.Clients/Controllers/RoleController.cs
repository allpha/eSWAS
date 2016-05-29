
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
        // GET: Region
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
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

        public ActionResult Create()
        {
            var NewItem = new RoleViewModel
            {
                Description = string.Empty,
            };

            return View(NewItem);
        }

        [HttpPost]
        public JsonResult Create(string descirption, List<PermissionViewModel> permissions)
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
                            PermissionId = permission.Id
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
                    role.RolePermissions = new List<RolePermissionItem>();
                    foreach (var permission in role.RolePermissions)
                        model.Periossions.Add(new PermissionViewModel
                        {
                            Id = permission.Id,
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
        public JsonResult Edit(string descirption, List<PermissionViewModel> permissions)
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
                            PermissionId = permission.Id
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