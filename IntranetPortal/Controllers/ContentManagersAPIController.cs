using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using IntranetPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace IntranetPortal.Controllers
{
    public class ContentManagersAPIController : Controller
    {
        private readonly HttpContext hcontext;
        private IntranetDBContext myContext = new IntranetDBContext();
        string ValidationErrorMessage = null;
        string DepartmentCode = null;
        string SectionCode = null;
        string UserEmail = null;
        string DesignationCode = null;
        string PFNumber = null;
        public ContentManagersAPIController(IHttpContextAccessor haccess)
        {
            hcontext = haccess.HttpContext;
            UserEmail = hcontext.User.FindFirst(ClaimTypes.Email).Value;
            SectionCode = hcontext.User.FindFirst(c => c.Type == "SectionCode").Value;
            DepartmentCode = hcontext.User.FindFirst(c => c.Type == "DepartmentCode").Value;
            DesignationCode = hcontext.User.FindFirst(c => c.Type == "DesignationCode").Value;
            PFNumber = hcontext.User.FindFirst(ClaimTypes.SerialNumber).Value;
        }

        [HttpGet]
        public async Task<IActionResult> GetNewsEvent(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.NewsEvents.Where(t => t.Status != "Closed" ), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }
        [HttpPost]
        public async Task<IActionResult> AddNews(string values)
        {
            var newNews = new  NewsEvent();
            JsonConvert.PopulateObject(values, newNews);
            newNews.CreatedBy = UserEmail;
            newNews.CreatedDate = DateTime.Now;
            newNews.UpdatedDate = DateTime.Now;
            newNews.UpdatedBy = UserEmail;
         
            
            if (!TryValidateModel(newNews))

                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            myContext.NewsEvents.Add(newNews);
            await myContext.SaveChangesAsync();

            return Ok(newNews);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateNewsEvent(int key, string values)
        {
            var newsData = await myContext.NewsEvents.FirstOrDefaultAsync(item => item.NewsEventsId == key);
            JsonConvert.PopulateObject(values, newsData);
             
            newsData.UpdatedDate= DateTime.Now;
            newsData.UpdatedBy = UserEmail;


            if (!TryValidateModel(newsData))
                return BadRequest(ValidationErrorMessage);

            await myContext.SaveChangesAsync();
            return Ok();
        }
    }
}
