using IntranetPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevExtreme.AspNet.Data;
using Newtonsoft.Json;
using System.Net;
using System.Collections;
using DevExtreme.AspNet.Mvc;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace IntranetPortal.Controllers
{
    [Authorize]
    public class ActivitiesAPIController : Controller
    {
        private readonly HttpContext hcontext;
        private IntranetDBContext myContext = new IntranetDBContext();
        string ValidationErrorMessage = null;
        string DepartmentCode = null;
        string SectionCode = null;
        string UserEmail = null;
        string DesignationCode = null;
        string PFNumber = null;

        
        public ActivitiesAPIController(IHttpContextAccessor haccess)
        {
            hcontext = haccess.HttpContext;
            UserEmail = hcontext.User.FindFirst(ClaimTypes.Email).Value;
            SectionCode= hcontext.User.FindFirst(c=>c.Type=="SectionCode").Value;
            DepartmentCode = hcontext.User.FindFirst(c => c.Type == "DepartmentCode").Value;
            DesignationCode = hcontext.User.FindFirst(c => c.Type == "DesignationCode").Value;
            PFNumber =       hcontext.User.FindFirst(ClaimTypes.SerialNumber).Value;
        }
        
        #region for Department/Section Activities 

        [HttpGet]
        public async Task<IActionResult> GetDepartmentActivities(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.ActivitiesDetails.Where(t=>t.SectionCode==SectionCode), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        [HttpGet]
        public object GetImplementationPerTemplate(int id, DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.ActivitiesDetails.Where(s => s.ActivityTemplateId == id), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        [HttpGet]
        public async Task<IActionResult> GetOfficersAssignedActivities(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.AssignedOfficersDetails.Include("Activity").Where(m=>m.Pfnumber == PFNumber), loadOptions);
           
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }
        [HttpGet]
        public async Task<IActionResult> GetOfficersAssignedActivitiesExternal(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.ActivitiesDetails.Where(m => m.ImpelementationStatus == "Started"), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }
        [HttpGet]
        public async Task<IActionResult> GetListofOfficers(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.Users.Where(t => t.ReportingTo == DesignationCode), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }
        [HttpGet]
        public async Task<IActionResult> GetICTOfficers(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.Users.Where(t => t.Designations.SectionCode== "ICT"), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        [HttpGet]
        public async Task<IActionResult> GetListofAssignedOfficersActivity(DataSourceLoadOptions loadOptions, long ActivityId)
        {
            var result = DataSourceLoader.Load(myContext.AssignedOfficersDetails.Where(t=>t.ActivityId==ActivityId), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }
        [HttpGet]
        public async Task<IActionResult> ActivitiesByOfficer(DataSourceLoadOptions loadOptions, long ActivityId)
        {
            var result = DataSourceLoader.Load(myContext.AssignedOfficersDetails.Where(t => t.ActivityId == ActivityId), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }
        public string GenerateActivityCode()
        {
            Random Ino = new Random();
            long ran_no = Ino.Next(1, 10000);
            DateTime now = DateTime.Now;
            return DepartmentCode + now.Year + "-A" + ran_no.ToString() + now.Month + now.Day;
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOfficerActivities(long key, string values)
        {
            var getActivityDetails = await myContext.AssignedOfficersDetails.FirstOrDefaultAsync(item => item.AssignedOfficerDetailsId == key);
            JsonConvert.PopulateObject(values, getActivityDetails);

            //if (!TryValidateModel(getActivityDetails))

            //    return BadRequest(ValidationErrorMessage);

            await myContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> AddNewActivity(string values)
        {
            var newActivity = new ActivitiesDetail();
            JsonConvert.PopulateObject(values, newActivity);
            newActivity.CreatedBy = UserEmail;
            newActivity.CreatedDate = DateTime.Now;
            newActivity.UpdateDate = DateTime.Now;
            newActivity.ActivityCode = GenerateActivityCode();
            newActivity.DepartmentCode = DepartmentCode;
            newActivity.SectionCode = SectionCode;
            if (!TryValidateModel(newActivity))

            return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            myContext.ActivitiesDetails.Add(newActivity);
            await myContext.SaveChangesAsync();

            return Ok(newActivity);
        }
        [HttpPost]
        public async Task<IActionResult> AddOfficersActivity(string values)
        {
            var newActivityAssignment = new  AssignedOfficersDetail();

            JsonConvert.PopulateObject(values, newActivityAssignment);

            newActivityAssignment.CreatedBy = UserEmail;
            newActivityAssignment.CreatedDate = DateTime.Now;
            newActivityAssignment.UpdatedDate = DateTime.Now;



            //if (!TryValidateModel(newActivityAssignment))

            //    return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            myContext.AssignedOfficersDetails.Add(newActivityAssignment);
            await myContext.SaveChangesAsync();

            return Ok(newActivityAssignment);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateActivity(int key, string values)
        {
            var ActivityData = await myContext.ActivitiesDetails.FirstOrDefaultAsync(item => item.ActivityId == key);
            JsonConvert.PopulateObject(values, ActivityData);
            ActivityData.UpdatedBy = UserEmail;
            ActivityData.UpdateDate = DateTime.Now;
            ActivityData.DepartmentCode = DepartmentCode;
            ActivityData.SectionCode = SectionCode;

            if (!TryValidateModel(ActivityData))
                return BadRequest(ValidationErrorMessage);

            await myContext.SaveChangesAsync();
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> UpdateRole(int key, string values)
        {
            var RoleDetails = await myContext.Roles.FirstOrDefaultAsync(item => item.Id == key);
            JsonConvert.PopulateObject(values, RoleDetails);

            if (!TryValidateModel(RoleDetails))
                return BadRequest(ValidationErrorMessage);

            await myContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        public async Task RemoveRole(int key)
        {
            var RoleDetails = await myContext.Roles.FirstOrDefaultAsync(item => item.Id == key);
            myContext.Roles.Remove(RoleDetails);
            await myContext.SaveChangesAsync();
        }

        #endregion roles management
    }
}
