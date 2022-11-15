using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntranetPortal.Controllers
{
    [Authorize]
    public class SupportTickets : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult OfficersNotification()
        {
            return View();
        }
        public IActionResult SupportInchargeManager()
        {
            return View();
        }
    }
}
