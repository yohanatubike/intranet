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
                case "library":
                    RedirectToAction("Index", "Library");
                    break;
                case "quiz":
                    return View("ManageQuiz");
                    break;
                case "articles":
                    return View("ManageArticles");
                    break;
                case "newsevents":
                    return View("ManageNewsEvents");
                    break;
                case "tips":
                    return View("ManageTips");
                    break;




            }
            return RedirectToAction("Index", "StaffPage");
        }
        public IActionResult ManageActivities()
        {
            return View();
        }
        public IActionResult ManageTips()
        {
            return View();
        }
        public IActionResult ManageArticles()
        {
            return View();
        }
        public IActionResult ManageNewsEvents()
        {
            return View();
        }
        public IActionResult ManageFrontSlider()
        {
            return View();
        }
       
    }
}
