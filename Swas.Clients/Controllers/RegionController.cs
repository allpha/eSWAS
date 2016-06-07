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
    public class RegionController : Controller
    {
        [Authorization("Region.View")]
        public ActionResult Index()
        {
            return View(new List<RegionViewModel>());
        }

        [HttpPost]
        [Authorization("Region.View")]
        public JsonResult LoadRegions()
        {
            var result = new List<RegionViewModel>();
            var bussinessLogic = new RegionBusinessLogic();

            try
            {
                var regionItemSource = bussinessLogic.Load();
                foreach (var item in regionItemSource)
                    result.Add(new RegionViewModel
                    {
                        Id = item.Id,
                        Name = item.Name
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

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [Authorization("Region.Insert")]
        public ActionResult Create()
        {
            var NewItem = new RegionViewModel();

            return View(NewItem);
        }


        [HttpPost]
        [Authorization("Region.Insert")]
        public JsonResult Create(string name)
        {
            var bussinessLogic = new RegionBusinessLogic();

            try
            {
                bussinessLogic.Create(new RegionItem
                {
                    Name = name
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

        [Authorization("Region.Edit")]
        public ActionResult Edit(int Id)
        {
            var bussinessLogic = new RegionBusinessLogic();

            try
            {
                var regionItem = bussinessLogic.Get(Id);

                var model = new RegionViewModel
                {
                    Id = regionItem.Id,
                    Name = regionItem.Name
                };

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
        [Authorization("Region.Edit")]
        public JsonResult Edit(int id, string name)
        {
            var bussinessLogic = new RegionBusinessLogic();

            try
            {
                bussinessLogic.Edit(new RegionItem { Id = id, Name = name });

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

        [Authorization("Region.Delete")]
        public ActionResult Delete(int id)
        {

            var bussinessLogic = new RegionBusinessLogic();

            try
            {
                var regionItem = bussinessLogic.Get(id);
                var model = new RegionViewModel
                {
                    Id = regionItem.Id,
                    Name = regionItem.Name
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
        [Authorization("Region.Delete")]
        public JsonResult Remove(int Id)
        {
            var bussinessLogic = new RegionBusinessLogic();

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