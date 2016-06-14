
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
    public class CustomerController : Controller
    {
        [Authorization("Customer.View")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorization("Customer.View")]
        public JsonResult Load()
        {
            var result = new List<CustomerItem>();
            var bussinessLogic = new CustomerBusinessLogic();

            try
            {
                result = bussinessLogic.Load();

                foreach(var it in result)
                    switch(it.Type)
                    {
                        case CustomerType.Juridical:
                            it.TypeDescription = "იურიდიული პირი";
                            break;
                        case CustomerType.Municipal:
                            it.TypeDescription = "მინუციპალიტეტი";
                            break;
                        case CustomerType.Personal:
                            it.TypeDescription = "ფიზიკური პირი";
                            break;

                    }
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

        [Authorization("Customer.Insert")]
        public ActionResult Create()
        {
            LoadTypeItemSource(new List<LandfillItem>() {
                                        new LandfillItem { Id = 0, Name = "მუნიციპალიტეტი" },
                                        new LandfillItem { Id = 1, Name = "იურიდიული პირი" },
                                        new LandfillItem { Id = 2, Name = "ფიზიკური პირი" },
                                    }, 0);
            return View();
        }

        private void LoadTypeItemSource(IList<LandfillItem> typeDataSource, object selectedType)
        {
            ViewBag.LandTypeItemSource = new SelectList(typeDataSource, "Id", "Name", selectedType);
        }

        [HttpPost]
        [Authorization("Customer.Insert")]
        public JsonResult Create(int type, string code, string name, string contactInfo)
        {
            var bussinessLogic = new CustomerBusinessLogic();

            try
            {
                bussinessLogic.Create(new CustomerItem
                {
                    Code = code,
                    Name = name,
                    Type = (CustomerType)type,
                    ContactInfo = contactInfo
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


        [Authorization("Customer.Edit")]
        public ActionResult Edit(int id)
        {
            var model = new CustomerViewModel();
            var bussinessLogic = new CustomerBusinessLogic();

            try
            {
                var customer = bussinessLogic.Get(id);
                model.Id = customer.Id;
                model.Code = customer.Code;
                model.Type = (int)customer.Type;
                model.Name = customer.Name;
                model.ContactInfo = customer.ContactInfo;

                LoadTypeItemSource(new List<LandfillItem>() {
                                        new LandfillItem { Id = 0, Name = "მუნიციპალიტეტი" },
                                        new LandfillItem { Id = 1, Name = "იურიდიული პირი" },
                                        new LandfillItem { Id = 2, Name = "ფიზიკური პირი" },
                                    }, model.Type);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
            return View(model);
        }


        [HttpPost]
        [Authorization("Customer.Edit")]
        public JsonResult Edit(int id, int type, string code, string name, string contactInfo)
        {
            var bussinessLogic = new CustomerBusinessLogic();

            try
            {
                bussinessLogic.Edit(new CustomerItem
                {
                    Id = id,
                    Code = code,
                    Name = name,
                    Type = (CustomerType)type,
                    ContactInfo = contactInfo
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

        [Authorization("Customer.Delete")]
        public ActionResult Delete(int id)
        {
            var bussinessLogic = new CustomerBusinessLogic();

            try
            {
                var customer = bussinessLogic.Get(id);
                var model = new CustomerViewModel
                {
                    Id = customer.Id,
                    Name = string.Format("{0} - {1}", customer.Code, customer.Name)
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
        [Authorization("Customer.Delete")]
        public JsonResult Remove(int Id)
        {
            var bussinessLogic = new CustomerBusinessLogic();

            try
            {
                bussinessLogic.Delete(Id);
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