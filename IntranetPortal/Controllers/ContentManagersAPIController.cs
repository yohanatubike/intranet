using Microsoft.AspNetCore.Mvc;

namespace IntranetPortal.Controllers
{
    public class ContentManagersAPIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
