using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using IntranetPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.DependencyResolver;

namespace IntranetPortal.Controllers
{
    public class BusinessCardController : Controller
    {
        private IntranetDBContext myContext = new IntranetDBContext();
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetStaffList(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.BusinessCards, loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }
        public IActionResult Staff()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveDetails(BusinessCard model)
        {
            try
            {
                var CardDetails = new BusinessCard();
                var checkDetails = myContext.BusinessCards.Where(t => t.Pfnumber == model.Pfnumber).SingleOrDefault();
                    if(checkDetails!=null)

                    {
                    ViewBag.Error = "Failed to save company profile";
                    return View("Index");
                    }
                else
                {
                    CardDetails.Pfnumber= model.Pfnumber;
                    CardDetails.FirstName = model.FirstName;
                    CardDetails.MiddleName = model.MiddleName;
                    CardDetails.LastName = model.LastName;
                    CardDetails.MobileNumber = model.MobileNumber;
                    CardDetails.Telephone= model.Telephone;
                    CardDetails.Title = model.Title;
                    CardDetails.Email = model.Email;
                    CardDetails.Fax = model.Fax;
                    myContext.BusinessCards.Add(CardDetails);
                    myContext.SaveChanges();


                    ViewBag.Success = "Details successfully saved";
                    return View("Index");
                }

            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Index");
            }
        }
    }
}
