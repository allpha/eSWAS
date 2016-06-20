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
    public class PositionController : Controller
    {
        [Authorization("Position.View")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorization("Position.View")]
        public JsonResult Load()
        {
            var result = new List<PositionItem>();
            var bussinessLogic = new PositionBusinessLogic();

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

        [Authorization("Position.Insert")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorization("Position.Insert")]
        public JsonResult Create(string name)
        {
            var bussinessLogic = new PositionBusinessLogic();

            try
            {
                bussinessLogic.Insert(new PositionItem
                {
                    Name = name,
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

        [Authorization("Position.Edit")]
        public ActionResult Edit(int id)
        {
            return View(new EditViewModel { Id = id });
        }

        [HttpPost]
        [Authorization("Position.Edit")]
        public JsonResult Get(int id)
        {
            var bussinessLogic = new PositionBusinessLogic();

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
        [Authorization("Position.Edit")]
        public JsonResult Edit(int id, string name)
        {
            var bussinessLogic = new PositionBusinessLogic();

            try
            {
                bussinessLogic.Edit(new PositionItem
                {
                    Id = id,
                    Name = name,
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

        [Authorization("Position.Delete")]
        public ActionResult Delete(int id)
        {
            return View(new EditViewModel { Id = id });
        }

        [HttpPost]
        [Authorization("Position.Delete")]
        public JsonResult Remove(int Id)
        {
            var bussinessLogic = new PositionBusinessLogic();

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