using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace IntranetPortal.Controllers
{
    [Authorize]
    public class StaffPageController : Controller
    {
        private readonly  HttpContext hcontext;
        private string UserEmail;
       
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
        public IActionResult UserRoles()
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
