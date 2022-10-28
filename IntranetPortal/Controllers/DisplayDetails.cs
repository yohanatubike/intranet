using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using IntranetPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IntranetPortal.Controllers
{
    public class DisplayDetails : Controller
    {
        private IntranetDBContext myContext = new IntranetDBContext();
        public IActionResult Index(string ActivityCode)
        {
            return View();
        }
        public IActionResult NotificationComment(long NotificationID)
        {
            var getNotificationDetails = myContext.StaffNotifications.Where(t => t.NotificationId == NotificationID).FirstOrDefault();
            HttpContext.Session.SetString("NotificationID", NotificationID.ToString());
            ViewData["NotificationDetails"] = getNotificationDetails.Details;
            ViewData["Subject"] = getNotificationDetails.Subject;
            return View();

        }
        public IActionResult NotificationDetails(long  NotificationID)
        {
            var getNotificationDetails = myContext.StaffNotifications.Where(t => t.NotificationId == NotificationID).FirstOrDefault();
            HttpContext.Session.SetString("NotificationID", NotificationID.ToString());
            ViewData["NotificationDetails"] = getNotificationDetails.Details;
            ViewData["Subject"] = getNotificationDetails.Subject;
            return View();
        }
        public IActionResult NewsEventsComment(string NewsID)
        {
            return View();
        }
        public IActionResult  DisplayNotification()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetActiveNotification(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.StaffNotifications.Where(t => t.Status == "Activated"), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }
    }
}
