using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
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
            var getActivityDetails = myContext.ActivitiesDetails.Where(t => t.ActivityCode == ActivityCode).FirstOrDefault();
          
            ViewData["TitleDetails"] =   getActivityDetails.Title;
            ViewData["Details"] = getActivityDetails.ExternalDetails;
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
        public IActionResult DisplayBirthdays()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetArticles(string? Category, DataSourceLoadOptions loadOptions)
        {
            LoadResult? result = null;

            if (Category != null)
            {
                result = DataSourceLoader.Load(myContext.Articles.Where(a => a.Category.Equals(Category)).Take(3), loadOptions);
            }
            else
            {
                result = DataSourceLoader.Load(myContext.Articles, loadOptions);
            }
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
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
        [HttpGet]
        public async Task<IActionResult> GetActiveBirthDays(DataSourceLoadOptions loadOptions)
        {
            var TheCurrentMonth = DateTime.Now.Month;
            var TheCurrentDay = DateTime.Now.Day;
            var NextWeek = TheCurrentDay + 7;
            var result = DataSourceLoader.Load(myContext.Users.Where(t => t.BirthMonth == TheCurrentMonth && t.BirthDay >= TheCurrentDay && t.BirthDay <= NextWeek), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }
    }
}
