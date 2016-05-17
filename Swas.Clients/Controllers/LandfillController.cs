namespace Swas.Client.Controllers
{
    using Swas.Business.Logic.Classes;
    using Swas.Business.Logic.Entity;
    using Swas.Clients.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class LandfillController : Controller
    {
        // GET: Landfill
        public ActionResult Index()
        {
            return View(new List<LandfillViewModel>());
        }

        [HttpPost]
        public JsonResult LoadLangfills()
        {
            var result = new List<LandfillViewModel>();
            var bussinessLogic = new LandfillBusinessLogic();

            try
            {
                var regionItemSource = bussinessLogic.Load();
                foreach (var item in regionItemSource)
                    result.Add(new LandfillViewModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        RegionName = item.RegionName
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

        //Get Landfill/Create
        public ActionResult Create()
        {
            var bussinessLogic = new RegionBusinessLogic();

            try
            {
                IList<RegionItem> regionDataSource = bussinessLogic.Load();
                var selectedRegionId = (int?)null;
                if (regionDataSource.Count > 0)
                    selectedRegionId = regionDataSource[0].Id;

                LoadRegionItemSource(regionDataSource, selectedRegionId);

                return View(new LandfillViewModel());
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

        private void LoadRegionItemSource(IList<RegionItem> regionDataSource, object selectedRegionId)
        {
            ViewBag.RegionItemSource = new SelectList(regionDataSource, "Id", "Name", selectedRegionId);
        }

        // POST: Landfill/Create
        [HttpPost]
        public JsonResult Create(int regionId, string name)
        {
            var bussinessLogic = new LandfillBusinessLogic();

            try
            {
                bussinessLogic.Create(new LandfillItem
                {
                    Name = name,
                    RegionId = regionId
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


        // GET: Landfill/Edit/5
        public ActionResult Edit(int Id)
        {
            var bussinessLogic = new LandfillBusinessLogic();

            try
            {
                var landfillItem = bussinessLogic.Get(Id);

                var model = new LandfillViewModel
                {
                    Id = landfillItem.Id,
                    Name = landfillItem.Name,
                    RegionID = landfillItem.RegionId
                };

                LoadRegionItemSource(landfillItem.RegionItemSource, landfillItem.RegionId);

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

        // POST: Landfill/Edit/5
        [HttpPost]
        public JsonResult Edit(int id, int regionId, string name)
        {
            var bussinessLogic = new LandfillBusinessLogic();

            try
            {
                bussinessLogic.Edit(new LandfillItem { Id = id, Name = name, RegionId = regionId });

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                bussinessLogic = null;
            }

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        // GET: Landfill/Delete/5
        public ActionResult Delete(int id)
        {
            var bussinessLogic = new LandfillBusinessLogic();

            try
            {
                var regionItem = bussinessLogic.Get(id);

                return View(new LandfillViewModel
                {
                    Id = regionItem.Id,
                    Name = regionItem.Name
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
        }

        // POST: Landfill/Delete/5
        [HttpPost]
        public JsonResult Remove(int Id)
        {
            var bussinessLogic = new LandfillBusinessLogic();

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