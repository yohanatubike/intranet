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
            var result = DataSourceLoader.Load(myContext.ActivitiesDetails.Where(t => t.SectionCode == SectionCode), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        [HttpGet]
        public async Task<IActionResult> GetArticles(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.Articles, loadOptions);
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

        [HttpDelete]
        public async Task RemoveArticle(int key)
        {
            var Article = await myContext.Articles.FirstOrDefaultAsync(item => item.ArticleId == key);
            if (Article == null)
                throw new ArgumentNullException();
            myContext.Articles.Remove(Article);
            await myContext.SaveChangesAsync();
        }

        [HttpGet]
        public ActionResult GetArticlePdf([FromQuery(Name = "Filename")] string filename)
        {
            var path = Path.Combine("Attachments", "Articles");
            string filePath = Path.Combine(path, filename);
            Response.Headers.Add("Content-Disposition", "inline; filename=test.pdf");
            var stream = new FileStream(filePath, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
    }
}
