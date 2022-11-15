using Microsoft.AspNetCore.Mvc;

namespace IntranetPortal.Controllers
{
    public class AuditingManagementController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
            
        }
        public IActionResult DepartmentalAuditing()
        {

            return View();

        }
        public IActionResult AuditingAttachments(string DepartmentCode)
        {

            HttpContext.Session.SetString("DepartmentCode", DepartmentCode.Trim('"'));
            return View();
        }
    }
}
