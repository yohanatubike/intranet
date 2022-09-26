using Microsoft.AspNetCore.Mvc;

namespace IntranetPortal.Controllers
{
    public class ContentManagerController : Controller
    {
        public IActionResult Index( string parameter)
        {
            switch (parameter)
            {
                case "documentation":
                 return   RedirectToAction("Index", "Documentation");
                    break;
                case "activity":
                    return View("ManageActivities");
                    break;
                case "frontslider":
                    return View("ManageFrontSlider");
                    break;
                case "forms":
                    return RedirectToAction("Index", "Forms");
                    break;
                case "systems":
                    return RedirectToAction("Index", "Systems");
                    break;
                case "":
                    RedirectToAction("Index", "Documentation");
                    break;




            }
            return RedirectToAction("Index", "StaffPage");
        }
        public IActionResult ManageActivities()
        {
            return View();
        }
    }
}
