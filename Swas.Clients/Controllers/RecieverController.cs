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
    public class RecieverController : Controller
    {
        [Authorization("Reciever.View")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorization("Reciever.View")]
        public JsonResult Load()
        {
            var result = new List<ReceiverItem>();
            var bussinessLogic = new ReceiverBusinessLogic();

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

        [Authorization("Reciever.Insert")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorization("Reciever.Insert")]
        public JsonResult Create(string name, string lastName)
        {
            var bussinessLogic = new ReceiverBusinessLogic();

            try
            {
                bussinessLogic.Insert(new ReceiverItem
                {
                    Name = name,
                    LastName = lastName,
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

        [Authorization("Reciever.Edit")]
        public ActionResult Edit(int id)
        {
            return View(new EditViewModel { Id = id });
        }

        [HttpPost]
        [Authorization("Reciever.Edit")]
        public JsonResult Get(int id)
        {
            var bussinessLogic = new ReceiverBusinessLogic();

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
        [Authorization("Reciever.Edit")]
        public JsonResult Edit(int id, string name, string lastName)
        {
            var bussinessLogic = new ReceiverBusinessLogic();

            try
            {
                bussinessLogic.Edit(new ReceiverItem
                {
                    Id = id,
                    Name = name,
                    LastName = lastName,
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

        [Authorization("Reciever.Delete")]
        public ActionResult Delete(int id)
        {
            return View(new EditViewModel { Id = id });
        }

        [HttpPost]
        [Authorization("Reciever.Delete")]
        public JsonResult Remove(int Id)
        {
            var bussinessLogic = new ReceiverBusinessLogic();

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