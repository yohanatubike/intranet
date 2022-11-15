using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using IntranetPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace IntranetPortal.Controllers
{
    [Authorize]
    public class SupportTicketAPIController : Controller
    {
        private readonly HttpContext hcontext;
        private IntranetDBContext myContext = new IntranetDBContext();
        string ValidationErrorMessage = null;
        string DepartmentCode = null;
        string SectionCode = null;
        string UserEmail = null;
        string DesignationCode = null;
        string PFNumber = null;
        public SupportTicketAPIController(IHttpContextAccessor haccess)
        {
            hcontext = haccess.HttpContext;
            UserEmail = hcontext.User.FindFirst(ClaimTypes.Email).Value;
            SectionCode = hcontext.User.FindFirst(c => c.Type == "SectionCode").Value;
            DepartmentCode = hcontext.User.FindFirst(c => c.Type == "DepartmentCode").Value;
            DesignationCode = hcontext.User.FindFirst(c => c.Type == "DesignationCode").Value;
            PFNumber = hcontext.User.FindFirst(ClaimTypes.SerialNumber).Value;


        }
              public async Task<IActionResult> GetDepartmentTickets(DataSourceLoadOptions loadOptions)
        {
            LoadResult result = null;
            if (DepartmentCode == "ICTS")
            {
                result = DataSourceLoader.Load(myContext.SupportsTickets.OrderByDescending(t => t.CreatedDate).Where(t => t.Status == "Assigned" || t.Status == "In-Progress" || t.Status == null || t.Status== "Onhold" || t.Status=="Lodged"), loadOptions); 
            }
            else
            {
                result = DataSourceLoader.Load(myContext.SupportsTickets.OrderByDescending(t=>t.CreatedDate).Where(t => t.LodgedBy == UserEmail && ( t.Status == "Assigned" || t.Status == "In-Progress" || t.Status == null || t.Status == "Onhold" || t.Status == "Lodged")), loadOptions);
            }

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }
        public async Task<IActionResult> GetSupportReports(DataSourceLoadOptions loadOptions)
        {
            LoadResult result = null;
           
            result = DataSourceLoader.Load(myContext.SupportsTickets.OrderByDescending(t => t.CreatedDate), loadOptions);
            

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }
        [HttpGet]
        public async Task<IActionResult> GetListofOfficers(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.Users.Where(t => t.Designations.DepartmentCode ==DepartmentCode), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }
        [HttpGet]
        public async Task<IActionResult> GetListofIncharge(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.SupportIncharge, loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateSupportTicket(long key, string values)
        {
            string CheckStaus = null;
            string CloseStatus = null;
            JObject data = JObject.Parse(values);
            CheckStaus =  data["AssignedTo"]?.ToString();
            CloseStatus = data["Status"]?.ToString();

            var getSupportDetails = await myContext.SupportsTickets.FirstOrDefaultAsync(item => item.TicketId == key);
            if (CheckStaus != null)
            {
                getSupportDetails.AssignedDate = DateTime.Now;
                getSupportDetails.AssignedBy = UserEmail;
                getSupportDetails.AssignedStatus = CloseStatus;

            }
            if (CloseStatus == "Resolved" || CloseStatus == "Closed")
            {
                if(getSupportDetails.AttendedDate==null)
                {
                    getSupportDetails.AttendedDate = DateTime.Now;
                    getSupportDetails.AttendedBy = UserEmail;
                }
                getSupportDetails.ClosedDate = DateTime.Now;
                getSupportDetails.ClosedBy = UserEmail;

            }
            if (CloseStatus == "Onhold" || CloseStatus == "In-Progress")
            {
                getSupportDetails.AttendedDate = DateTime.Now;
                getSupportDetails.AttendedBy = UserEmail;

            }
            JsonConvert.PopulateObject(values, getSupportDetails);

            await myContext.SaveChangesAsync();
            return Ok();
        }
        public async Task<IActionResult> GetServices(DataSourceLoadOptions loadOptions)
        {
           
            var  result = DataSourceLoader.Load(myContext.ServiceCategories, loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }
        public async Task<IActionResult> GetSubServices(DataSourceLoadOptions loadOptions)
        {

            var result = DataSourceLoader.Load(myContext.ServiceSubCategories, loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }
        public async Task<IActionResult> UpdateIncharge(long key, string values)
        {
            var getInchargeDetails = await myContext.SupportIncharge.FirstOrDefaultAsync(item => item.SupportInchargeID == key);
            JsonConvert.PopulateObject(values, getInchargeDetails);
            getInchargeDetails.UpdatedDate = DateTime.Now;
                getInchargeDetails.UpdatedBy = UserEmail;


            await myContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> AddNewIncharge(string values)
        {
            var checkIfExists = myContext.SupportIncharge.Where(t => t.Status == true).SingleOrDefault();
            if (checkIfExists == null)
            {
                var newIncharge = new SupportIncharge();
                JsonConvert.PopulateObject(values, newIncharge);
                newIncharge.AssignedBy = UserEmail;
                newIncharge.AssignedDate = DateTime.Now;


                if (!TryValidateModel(newIncharge))

                    return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
                myContext.SupportIncharge.Add(newIncharge);
                await myContext.SaveChangesAsync();

                return Ok(newIncharge);
                
            }

            return BadRequest(ValidationErrorMessage = "Please change status of the existing Support Incharge , before adding a new one");

        }
        [HttpPost]
        public async Task<IActionResult> AddNewTicket(string values)
        {
            
            var newTicket = new  SupportsTicket();
            JsonConvert.PopulateObject(values, newTicket);
            newTicket.LodgedBy = UserEmail;
            newTicket.DepartmentCode = DepartmentCode;
            newTicket.CreatedDate = DateTime.Now;
            newTicket.Status = "Lodged";
         
            //if (!TryValidateModel(newTicket))

            //    return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            myContext.SupportsTickets.Add(newTicket);
            await myContext.SaveChangesAsync();

            return Ok(newTicket);
        }
    }
}
