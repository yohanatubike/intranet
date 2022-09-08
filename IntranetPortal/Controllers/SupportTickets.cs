using Microsoft.AspNetCore.Mvc;

namespace IntranetPortal.Controllers
{
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
    }
}
