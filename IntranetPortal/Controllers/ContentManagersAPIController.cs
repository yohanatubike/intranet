using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using IntranetPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Security.Claims;

namespace IntranetPortal.Controllers
{
    [Authorize]
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
            UserEmail = hcontext?.User?.FindFirst(ClaimTypes.Email)?.Value;
            SectionCode = hcontext?.User?.FindFirst(c => c.Type == "SectionCode")?.Value;
            DepartmentCode = hcontext?.User?.FindFirst(c => c.Type == "DepartmentCode")?.Value;
            DesignationCode = hcontext?.User?.FindFirst(c => c.Type == "DesignationCode")?.Value;
            PFNumber = hcontext?.User?.FindFirst(ClaimTypes.SerialNumber)?.Value;
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

        #region Sliders

        [HttpGet]
        public ActionResult GetAllSliders(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.FrontEndSliders.Where(f => f.PublishStatus.Equals("Active")), loadOptions);
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
            newNews.CreatedDate = DateTime.UtcNow;

            if (!TryValidateModel(newNews))
                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");

            try
            {
                myContext.NewsEvents.Add(newNews);
                await myContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok(newNews);
        }
        [HttpPost]
        public async Task<IActionResult> AddFrontSlider(string values)
        {
            var Slider = new FrontEndSlider();
            JsonConvert.PopulateObject(values, Slider);
            Slider.CreatedBy = "yohana.tubike@tasac.go.tz";
            Slider.CreatedDate = DateTime.UtcNow;

            if (!TryValidateModel(Slider))
                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");

            try
            {
                myContext.FrontEndSliders.Add(Slider);
                await myContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok(Slider);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSlider(int key, string values)
        {
            var Slider = await myContext.FrontEndSliders.FirstOrDefaultAsync(item => item.SliderId == key);
            if (Slider == null)
                throw new ArgumentNullException();
            JsonConvert.PopulateObject(values, Slider);
            if (!TryValidateModel(Slider))
                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            myContext.FrontEndSliders.Update(Slider);
            await myContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task RemoveSlider(int key)
        {
            var Slider = await myContext.FrontEndSliders.FirstOrDefaultAsync(item => item.SliderId == key);
            if (Slider == null)
                throw new ArgumentNullException();
            myContext.FrontEndSliders.Remove(Slider);
            await myContext.SaveChangesAsync();
        }
        #endregion

        #region Articles

        [HttpGet]
        public async Task<IActionResult> GetArticles(string? Category, DataSourceLoadOptions loadOptions)
        {
            LoadResult? result = null;

            if (Category != null)
            {
                result = DataSourceLoader.Load(myContext.Articles.Where(a => a.Category.Equals(Category)).Take(3), loadOptions);
            } else
            {
                result = DataSourceLoader.Load(myContext.Articles, loadOptions);
            }
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> AddArticle(string values)
        {
            var Article = new Article();
            JsonConvert.PopulateObject(values, Article);
            Article.CreatedBy = "yohana.tubike@tasac.go.tz";
            Article.CreatedDate = DateTime.UtcNow;

            if (!TryValidateModel(Article))
                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            
            try
            {
                myContext.Articles.Add(Article);
                await myContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok(Article);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateArticle(int key, string values)
        {
            var Article = await myContext.Articles.FirstOrDefaultAsync(item => item.ArticleId == key);
            if (Article == null)
                throw new ArgumentNullException();
            JsonConvert.PopulateObject(values, Article);
            if (!TryValidateModel(Article))
                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            myContext.Articles.Update(Article);
            await myContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> AddOfficersActivity(string values)
        {
            var newActivityAssignment = new AssignedOfficersDetail();
            JsonConvert.PopulateObject(values, newActivityAssignment);
            newActivityAssignment.CreatedBy = UserEmail;
            newActivityAssignment.CreatedDate = DateTime.Now;
            newActivityAssignment.UpdatedDate = DateTime.Now;
            //if (!TryValidateModel(newActivityAssignment))

            //    return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            myContext.AssignedOfficersDetails.Add(newActivityAssignment);
            await myContext.SaveChangesAsync();

            return Ok(newActivityAssignment);
        }

        [HttpGet]
        public async Task<IActionResult> GetOfficersAssignedActivitiesExternal(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.ActivitiesDetails.Where(m => m.ImpelementationStatus == "Started"), loadOptions);

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateActivity(int key, string values)
        {
            var ActivityData = await myContext.ActivitiesDetails.FirstOrDefaultAsync(item => item.ActivityId == key);
            JsonConvert.PopulateObject(values, ActivityData);
            ActivityData.UpdatedBy = UserEmail;
            ActivityData.UpdateDate = DateTime.Now;
            ActivityData.DepartmentCode = DepartmentCode;
            ActivityData.SectionCode = SectionCode;

            if (!TryValidateModel(ActivityData))
                return BadRequest(ValidationErrorMessage);

            await myContext.SaveChangesAsync();
            return Ok();
        }



        [HttpDelete]
        public async Task RemoveArticle(int key)
        {
            var Article = await myContext.Articles.FirstOrDefaultAsync(item => item.ArticleId == key);
            if (Article == null)
                throw new ArgumentNullException();
            myContext.Articles.Remove(Article);
            await myContext.SaveChangesAsync();
        }
        #endregion
    }
}
