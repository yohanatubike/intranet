using IntranetPortal.Models;
using IntranetPortal.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

using System.Text;

namespace IntranetPortal.Controllers
{
    public class HomeController : Controller

    {
        private static IntranetDBContext myContext = new IntranetDBContext();
        private UserInitializationService userInitializationService = new UserInitializationService(myContext);
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult NotificationDetails(string NotificationID)
        {
            var getNotificationDetails = myContext.StaffNotifications.Where(t => t.NotificationId.ToString() == NotificationID).SingleOrDefault();
            HttpContext.Session.SetString("NotificationID", NotificationID.ToString());
            ViewBag["NotificationDetails"] = getNotificationDetails.Details;
            return View();
        }

        public IActionResult Index()
        {
            //userInitializationService.OnApplicationStarted();
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public static string getHashedMD5Password(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Processlogin(LoginViewModel model, string ReturnUrl)
        {
            //if (ModelState.IsValid)
            //{
                  var ContentManager = "0";
                  var Auditor = "0";
                  var Planner = "0";
               
                var encryptedPass = getHashedMD5Password(model.Password);
                var getUser = myContext.Users.Include("Designations").SingleOrDefault(t => t.PFNumber == model.PFNumber && t.Password == encryptedPass );
                var isContentManager = myContext.UserRoles.Include("Roles").SingleOrDefault(t => t.PFNumber == model.PFNumber && t.Roles.RoleName=="ContentManagers");
                var isAuditor= myContext.UserRoles.Include("Roles").SingleOrDefault(t => t.PFNumber == model.PFNumber && t.Roles.RoleName == "Auditing");
                var isPlanningOfficer = myContext.UserRoles.Include("Roles").SingleOrDefault(t => t.PFNumber == model.PFNumber && t.Roles.RoleName == "Planning");
                        if (isContentManager !=null)
                                {
                                   ContentManager = isContentManager.Roles.RoleName;
                                }
                                    if (isAuditor != null)
                                    {
                                        Auditor = isAuditor.Roles.RoleName;
                                    }
                            if (isPlanningOfficer != null)
                            {
                                Planner = isPlanningOfficer.Roles.RoleName;
                            }

            if (getUser != null)
            {
                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {

                    var userClaims = new List<Claim>()
                {

                    new Claim(ClaimTypes.Name  , getUser.FirstName+" "+ getUser.LastName ),
                    new Claim(ClaimTypes.SerialNumber  , getUser.PFNumber),
                    new Claim(ClaimTypes.Email, getUser.Email),
                    new Claim(ClaimTypes.DateOfBirth, getUser.DateOfBirth?.ToString()),
                    new Claim("SectionCode", getUser.Designations.SectionCode),
                    new Claim("DesignationCode", getUser.DesignationCode),
                    new Claim("DepartmentCode", getUser.Designations.DepartmentCode),
                    new Claim("IsSupervisor", getUser.Designations.SupervisoryPostion.ToString()),
                    new Claim("IsAdmin", getUser.Designations.SectionCode.ToString()),
                    new Claim("IsAContentManager",ContentManager),
                    new Claim("IsAuditor", Auditor),
                     new Claim("IsPlanner", Planner),

                 };
                    var userIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });
                    await HttpContext.SignInAsync(userPrincipal);
                    return RedirectToAction("Dashboard", "StaffPage");
                }
                // }
            }
            ViewBag.Error = "Failed to login ! Invalid login credentials";
            return View("Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //[ChildActionOnly]
        //public ActionResult StudentList()
        //{
        //    return PartialView(students);
        //}
    }
}