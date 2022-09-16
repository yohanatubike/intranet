using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using IntranetPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace IntranetPortal.Controllers
{
    [Authorize]
    public class StaffNotificationAPIController : Controller
    {
        private readonly HttpContext hcontext;
        private IntranetDBContext myContext = new IntranetDBContext();
        string ValidationErrorMessage = null;
        string DepartmentCode = null;
        string SectionCode = null;
        string UserEmail = null;
        string DesignationCode = null;
        string PFNumber = null;
        public StaffNotificationAPIController(IHttpContextAccessor haccess)
        {
            hcontext = haccess.HttpContext;
            UserEmail = hcontext.User.FindFirst(ClaimTypes.Email).Value;
            SectionCode = hcontext.User.FindFirst(c => c.Type == "SectionCode").Value;
            DepartmentCode = hcontext.User.FindFirst(c => c.Type == "DepartmentCode").Value;
            DesignationCode = hcontext.User.FindFirst(c => c.Type == "DesignationCode").Value;
            PFNumber = hcontext.User.FindFirst(ClaimTypes.SerialNumber).Value;


        }

        [HttpGet]
        public async Task<IActionResult> GetActiveNotification(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.StaffNotifications.Where(t => t.Pfnumber == PFNumber), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }
        [HttpPost]
        public async Task<IActionResult> AddNewNotification(string values)
        {
            var newNotification = new StaffNotification();
            JsonConvert.PopulateObject(values, newNotification);
            newNotification.CreatedBy = UserEmail;
            newNotification.CreatedDate = DateTime.Now;
            newNotification.UpdatedDate = DateTime.Now;
            newNotification.Pfnumber = PFNumber;

           
            //if (!TryValidateModel(newNotification))

            //return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            myContext.StaffNotifications.Add(newNotification);
            await myContext.SaveChangesAsync();

            return Ok(newNotification);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
