using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntranetPortal.Controllers
{
    [Authorize]
    public class MeetingManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
