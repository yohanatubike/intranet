
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using IntranetPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace IntranetPortal.Controllers
{
    //[Authorize]
    public class FormsApiController : Controller
    {
        private IntranetDBContext _dbContext = new IntranetDBContext();
        string ValidationErrorMessage;

        [HttpGet]
        public async Task<IActionResult> GetForms(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(_dbContext.TasacForms, loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

               

        

    }
}
