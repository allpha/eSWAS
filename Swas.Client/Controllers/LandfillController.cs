namespace Swas.Client.Controllers
{
    using Swas.Business.Logic.Classes;
    using Swas.Business.Logic.Entity;
    using Swas.Client.Models;
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
            var bussinessLogic = new LandfillBusinessLogic();

            try
            {
                var lendfillItemSource = bussinessLogic.Load();
                var model = new List<LandfillViewModel>();

                foreach (var item in lendfillItemSource)
                    model.Add(new LandfillViewModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        RegionName = item.RegionName
                    });

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

        //Get Landfill/Create
        public ActionResult Create()
        {
            var bussinessLogic = new RegionBusinessLogic();

            try
            {

                //var regionItemSource = 

                IList<RegionItem> regionDataSource = bussinessLogic.Load();
                var selectedRegionId = (int?)null;
                if (regionDataSource.Count > 0)
                    selectedRegionId = regionDataSource[0].Id;

                LoadRegionItemSource(regionDataSource, selectedRegionId);

                var NewItem = new LandfillViewModel();

                return View(NewItem);
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
        public ActionResult Create([Bind(Include = "Id,Name,RegionId")]LandfillViewModel model)
        {
            var bussinessLogic = new LandfillBusinessLogic();

            try
            {
                bussinessLogic.Create(new LandfillItem
                {
                    Name = model.Name,
                    RegionId = model.RegionID
                });

                return RedirectToAction("Index");
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
        public ActionResult Edit(int id, [Bind(Include = "Id,Name,RegionId")]LandfillViewModel model)
        {
            var bussinessLogic = new LandfillBusinessLogic();

            try
            {
                bussinessLogic.Edit(new LandfillItem { Id = model.Id, Name = model.Name, RegionId = model.RegionID });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
            finally
            {
                bussinessLogic = null;
            }
        }

        // GET: Landfill/Delete/5
        public ActionResult Delete(int id)
        {
            var bussinessLogic = new LandfillBusinessLogic();

            try
            {
                var regionItem = bussinessLogic.Get(id);
                var model = new LandfillViewModel
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

        // POST: Landfill/Delete/5
        [HttpPost]
        public ActionResult Delete(int Id, FormCollection collection)
        {
            var bussinessLogic = new LandfillBusinessLogic();

            try
            {
                bussinessLogic.Remove(Id);

                return RedirectToAction("Index");

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
    }
}