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
    public class RepresentativeController : Controller
    {
        [Authorization("Representative.View")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorization("Representative.View")]
        public JsonResult Load()
        {
            var result = new List<RepresentativeItem>();
            var bussinessLogic = new RepresentativeBusinessLogic();

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

        [Authorization("Representative.Insert")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorization("Representative.Insert")]
        public JsonResult Create(string name)
        {
            var bussinessLogic = new RepresentativeBusinessLogic();

            try
            {
                bussinessLogic.Insert(new RepresentativeItem
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

        [Authorization("Representative.Edit")]
        public ActionResult Edit(int id)
        {
            return View(new EditViewModel { Id = id });
        }

        [HttpPost]
        [Authorization("Representative.Edit")]
        public JsonResult Get(int id)
        {
            var bussinessLogic = new RepresentativeBusinessLogic();

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
        [Authorization("Representative.Edit")]
        public JsonResult Edit(int id, string name)
        {
            var bussinessLogic = new RepresentativeBusinessLogic();

            try
            {
                bussinessLogic.Edit(new RepresentativeItem
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

        [Authorization("Representative.Delete")]
        public ActionResult Delete(int id)
        {
            return View(new EditViewModel { Id = id });
        }

        [HttpPost]
        [Authorization("Representative.Delete")]
        public JsonResult Remove(int Id)
        {
            var bussinessLogic = new RepresentativeBusinessLogic();

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