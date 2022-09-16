using Microsoft.AspNetCore.Mvc;

namespace IntranetPortal.Controllers
{
    public class FormsController : Controller
    {
        public IActionResult ITForms()
        {
            return View();
        }
        public IActionResult FAUForms()
        {
            return View();
        }
        public IActionResult HRForms()
        {
            return View();
        }
    }
}
