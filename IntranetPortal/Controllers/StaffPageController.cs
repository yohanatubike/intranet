using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using IntranetPortal.Models;
using Microsoft.EntityFrameworkCore;

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
            UserEmail =   hcontext.User.FindFirst(ClaimTypes.Email).Value;

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
            var getStaffDetails = myContext.Users.Where(t => t.Email == UserEmail).SingleOrDefault();
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
        private string GenerateFileName()
        {
            Random res = new Random();
            String str = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int size = 18;
            String ran = "";

            for (int i = 0; i < size; i++)
            {
                int x = res.Next(62);
                ran = ran + str[x];
            }
            return ran;
        }
        void SaveFile(IFormFile file, string fileName)
        {
            try
            {
                fileName = fileName + ".PNG";
                var path = Path.Combine("wwwroot", "StaffImage");
                // save the file
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                using (var fileStream = System.IO.File.Create(Path.Combine(path, fileName)))
                {
                    file.CopyTo(fileStream);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }
        }

        [HttpPost]
        public ActionResult UploadPicture(IFormFile myFile, [FromQuery(Name = "Pfnumber")] string Pfnumber)
        {
            ViewBag.Id = Pfnumber;


            if (myFile != null)
            {

                ViewBag.File = GenerateFileName();
                SaveFile(myFile, ViewBag.File.ToString());
                UpdatePicture(Pfnumber, fileName: ViewBag.File + ".PNG");
            }
            ViewData["FileUpload"] = "File successfully  uplaoded";
            return View();
        }
        private async void UpdatePicture(string Pfnumber, dynamic fileName)
        {
            var result = await myContext.Users.FirstOrDefaultAsync(item => item.PFNumber == Pfnumber);
            if (result != null)
            {
                result.PictureUrl= "wwwroot/StaffImage/"+fileName.ToString();
                await myContext.SaveChangesAsync();
            }
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
