using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using IntranetPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace IntranetPortal.Controllers
{
    [Authorize]
    public class MeetingManagementAPIController : Controller
    {
        private readonly HttpContext hcontext;
        private IntranetDBContext myContext = new IntranetDBContext();
        string ValidationErrorMessage = null;
        string DepartmentCode = null;
        string SectionCode = null;
        string UserEmail = null;
        string DesignationCode = null;
        string PFNumber = null;
        public MeetingManagementAPIController(IHttpContextAccessor haccess)
        {
            hcontext = haccess.HttpContext;
            UserEmail = hcontext.User.FindFirst(ClaimTypes.Email).Value;
            SectionCode = hcontext.User.FindFirst(c => c.Type == "SectionCode").Value;
            DepartmentCode = hcontext.User.FindFirst(c => c.Type == "DepartmentCode").Value;
            DesignationCode = hcontext.User.FindFirst(c => c.Type == "DesignationCode").Value;
            PFNumber = hcontext.User.FindFirst(ClaimTypes.SerialNumber).Value;


        }
        [HttpGet]
        public async Task<IActionResult> GetMeetings(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.Meetings.Where(t => t.CreatedBy == UserEmail), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }



        [HttpPut]
        public async Task<IActionResult> UpdateMeeting(int key, string values)
        {
            var  meetingDetails = await myContext.Meetings.FirstOrDefaultAsync(item => item.MeetingId == key);
            JsonConvert.PopulateObject(values, meetingDetails);

            if (!TryValidateModel(meetingDetails))
                return BadRequest(ValidationErrorMessage);

            await myContext.SaveChangesAsync();
            return Ok();
        }
        public string GenerateMeetingCode()
        {
            Random Ino = new Random();
            long ran_no = Ino.Next(1, 10000);
            DateTime now = DateTime.Now;
            return DepartmentCode + now.Year + "-M" + ran_no.ToString() + now.Month + now.Day;
        }
        [HttpPost]
        public async Task<IActionResult> AddNewMeeting(string values)
        {
            var newMeeting = new Meeting();
            JsonConvert.PopulateObject(values, newMeeting);
            newMeeting.CreatedBy = UserEmail;
            newMeeting.StartDate = DateTime.Now;
            newMeeting.EndDate = DateTime.Now;
            newMeeting.MeetingCode = GenerateMeetingCode();
            newMeeting.DepartmentCode = DepartmentCode;
            newMeeting.SectionCode = SectionCode;
            if (!TryValidateModel(newMeeting))

                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            myContext.Meetings.Add(newMeeting);
            await myContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetListofAssignedOfficersMeeting(DataSourceLoadOptions loadOptions, long MeetingId)
        {
            var result = DataSourceLoader.Load(myContext.MeetingInvitations.Where(t => t.MeetingId == MeetingId), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }
        [HttpGet]
        public async Task<IActionResult> GetOfficersInvitedMeetings(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.MeetingInvitations.Include("Meetings").Where(m => m.Pfnumber == PFNumber), loadOptions);

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }
        [HttpPost]
        public async Task<IActionResult> InviteOfficers(string values)
        {
            var newMeetingInvitation = new MeetingInvitation();

            JsonConvert.PopulateObject(values, newMeetingInvitation);

            newMeetingInvitation.InvitedBy = UserEmail;
            newMeetingInvitation.InvitedDate = DateTime.Now;
            newMeetingInvitation.UpdateDate = DateTime.Now;

            myContext.MeetingInvitations.Add(newMeetingInvitation);
            await myContext.SaveChangesAsync();

            return Ok(newMeetingInvitation);
        }
        [HttpGet]
        public async Task<IActionResult> GetListofOfficers(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.Users, loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInviteOfficers(int key, string values)
        {
            var  meetingData = await myContext.MeetingInvitations.FirstOrDefaultAsync(item => item.MeetingInvitationId == key);
            JsonConvert.PopulateObject(values, meetingData);
            meetingData.UpdatedBy = UserEmail;

            //if (!TryValidateModel(meetingData))
            //    return BadRequest(ValidationErrorMessage);

            await myContext.SaveChangesAsync();
            return Ok();
        }

    }
}
