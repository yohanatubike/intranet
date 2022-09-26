using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntranetPortal.Controllers
{
    [Authorize]
    public class MeetingManagerController : Controller
    {
        public IActionResult MeetingAttachments(string MeetingCode)
        {

            HttpContext.Session.SetString("MeetingCode", MeetingCode.Trim('"'));
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
