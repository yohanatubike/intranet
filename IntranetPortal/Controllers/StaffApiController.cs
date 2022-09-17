using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using IntranetPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace IntranetPortal.Controllers
{
    public class StaffApiController : Controller
    {
        private IntranetDBContext myContext = new IntranetDBContext();
        string ValidationErrorMessage = null;

        [HttpGet]
        public async Task<IActionResult> GetUsers(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.Users.Include("Designations"), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        [HttpGet]
        public async Task<IActionResult> GetDesignation(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.Designations, loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        public async Task<IActionResult> GetSupervisoryDesignation(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.Designations.Where(t => t.SupervisoryPostion == true), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }
    }
}
