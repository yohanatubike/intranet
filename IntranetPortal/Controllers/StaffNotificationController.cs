using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntranetPortal.Controllers
{
    [Authorize]
    public class StaffNotificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
