using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using IntranetPortal.Models;

namespace IntranetPortal.Controllers
{
    [Authorize]
    public class StaffPageController : Controller
    {
        private readonly  HttpContext hcontext;
        private string UserEmail;
        private IntranetDBContext myContext = new IntranetDBContext();

        public StaffPageController(IHttpContextAccessor haccess)
        {
            hcontext = haccess.HttpContext;
    
        }
        
        public IActionResult Index()
        {

            ViewBag.UserEmail = hcontext.User.FindFirst(ClaimTypes.Email).Value;
            return View();
        }

        public IActionResult Groups()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            var GetCurrentIncharge = myContext.SupportIncharge.Where(t => t.Status == true).SingleOrDefault();
            var getStaffDetails = myContext.Users.Where(t => t.Email == GetCurrentIncharge.InchargeName).SingleOrDefault();
            var getActivityDetails = myContext.AssignedOfficersDetails.Where(t => t.Pfnumber == getStaffDetails.PFNumber && t.Activity.PublishStatus != "Closed").Count();
            var getMeetingDetails = myContext.MeetingInvitations.Where(t => t.Pfnumber == getStaffDetails.PFNumber && t.AcceptanceStatus == "Invited").Count();
            ViewData["InchargeName"] = getStaffDetails.FirstName + "  " + getStaffDetails.LastName;
            ViewData["InchargeMobile"] = getStaffDetails.MobileNumber;
            ViewData["NumberofActivities"] = getActivityDetails;
            ViewData["NumberofMeetings"] = getMeetingDetails;
            return View();
        }

        public IActionResult UserRoles()
        {
            return View();
        }

        public IActionResult GenerateSupportReport()
        {
            return View();
        }

        public IActionResult  UsersList()
        {
            return View();
        }

        public IActionResult PositionList()
        {
            return View();
        }

        public IActionResult DepartmentsList()
        {
            return View();
        }
        public IActionResult UploadPicture(string Pfnumber)
        {
            return View();
        }
        public IActionResult SectionList()
        {
            return View();
        }

        public IActionResult UserPermission()
        {
            return View();
        }
    }
}
