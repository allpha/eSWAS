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


    public class RegionController : Controller
    {
        // GET: Region
        public ActionResult Index()
        {
            var bussinessLogic = new RegionBusinessLogic();

            try
            {
                var regionItemSource = bussinessLogic.Load();
                var model = new List<RegionViewModel>();

                foreach (var item in regionItemSource)
                    model.Add(new RegionViewModel
                    {
                        Id = item.Id,
                        Name = item.Name
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

        //Get Region/Create
        public ActionResult Create()
        {
            var NewItem = new RegionViewModel();

            return View(NewItem);
        }

        // POST: Region/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Name")]RegionViewModel model)
        {
            var bussinessLogic = new RegionBusinessLogic();

            try
            {
                bussinessLogic.Create(new RegionItem
                {
                    Name = model.Name
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

        // GET: Region/Edit/5
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
                return View();
            }
            finally
            {
                bussinessLogic = null;
            }
        }

        // POST: Region/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, [Bind(Include = "Id,Name")]RegionViewModel model)
        {
            var bussinessLogic = new RegionBusinessLogic();

            try
            {
                bussinessLogic.Edit(new RegionItem { Id = model.Id, Name = model.Name });

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

        // GET: Region/Delete/5
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

        // POST: Region/Delete/5
        [HttpPost]
        public ActionResult Delete(int Id, FormCollection collection)
        {
            var bussinessLogic = new RegionBusinessLogic();

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