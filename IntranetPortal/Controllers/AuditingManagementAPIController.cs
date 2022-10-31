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
    public class AuditingManagementAPIController : Controller
    {
        private readonly HttpContext hcontext;
        private IntranetDBContext myContext = new IntranetDBContext();
        string ValidationErrorMessage = null;
        string DepartmentCode = null;
        string SectionCode = null;
        string UserEmail = null;
        string DesignationCode = null;
        string PFNumber = null;
        public AuditingManagementAPIController(IHttpContextAccessor haccess)
        {
            hcontext = haccess.HttpContext;
            UserEmail = hcontext.User.FindFirst(ClaimTypes.Email).Value;
            SectionCode = hcontext.User.FindFirst(c => c.Type == "SectionCode").Value;
            DepartmentCode = hcontext.User.FindFirst(c => c.Type == "DepartmentCode").Value;
            DesignationCode = hcontext.User.FindFirst(c => c.Type == "DesignationCode").Value;
            PFNumber = hcontext.User.FindFirst(ClaimTypes.SerialNumber).Value;


        }
        [HttpGet]
        public async Task<IActionResult> GetDepartmentalAuditings(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.ActivitiesDetails.Where(t => t.SectionCode == SectionCode), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAuditings(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.AuditingDetail, loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartmentalAuditing(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.AuditingDetail.Where(t=>t.SectionCode==SectionCode), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }
        [HttpPost]
        public async Task<IActionResult> AddAuditDetails(string values)
        {
            var newAudit= new AuditingDetail();
            JsonConvert.PopulateObject(values, newAudit);
            newAudit.RecordedBy = UserEmail;
            newAudit.CreatedDate = DateTime.Now;
            newAudit.UpdatedDate = DateTime.Now;
            myContext.AuditingDetail.Add(newAudit);
            await myContext.SaveChangesAsync();

            return Ok(newAudit);
        }
    }
}
