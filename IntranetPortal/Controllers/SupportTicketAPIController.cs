﻿using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using IntranetPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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
            if (DepartmentCode == "HICT")
            {
                result = DataSourceLoader.Load(myContext.SupportsTickets.Where(t => t.DepartmentCode == "HICT" && t.Status!="Closed"), loadOptions);
            }
            else
            {
                result = DataSourceLoader.Load(myContext.SupportsTickets.Where(t => t.LodgedBy == UserEmail), loadOptions);
            }

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }
        [HttpPost]
        public async Task<IActionResult> AddNewTicket(string values)
        {
            var newTicket = new  SupportsTicket();
            JsonConvert.PopulateObject(values, newTicket);
            newTicket.LodgedBy = UserEmail;
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