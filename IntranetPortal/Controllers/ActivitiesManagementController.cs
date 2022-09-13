using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntranetPortal.Controllers
{
    //[Authorize(Policy ="Supervisor")]
    [Authorize]
    public class ActivitiesManagementController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult  ActivitisAttachments(string ActivityCode)
        {

            HttpContext.Session.SetString("ActivityCode", ActivityCode.Trim('"'));
            return View();
        }
    }
}
