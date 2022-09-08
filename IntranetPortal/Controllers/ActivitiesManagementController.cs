using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntranetPortal.Controllers
{
    [Authorize(Policy ="Supervisor")]
    public class ActivitiesManagementController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult  ActivitisAttachments()
        {
            return View();
        }
    }
}
