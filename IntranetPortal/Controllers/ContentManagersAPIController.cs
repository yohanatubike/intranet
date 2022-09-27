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

        public async Task<IActionResult> GetArticles(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(myContext.Articles.Where(f => f.DepartmentCode.Equals("FAU")), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> AddForm(string values)
        {
            var Form = new TasacForm();
            JsonConvert.PopulateObject(values, Form);

            if (!TryValidateModel(Form))
                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            Form.CreatedBy = "yohana.tubike@tasac.go.tz";
            Form.CreatedDate = DateTime.UtcNow;
            try
            {
                myContext.TasacForms.Add(Form);
                await myContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok(Form);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateForm(int key, string values)
        {
            var Form = await myContext.TasacForms.FirstOrDefaultAsync(item => item.FormId == key);
            if (Form == null)
                throw new ArgumentNullException();
            JsonConvert.PopulateObject(values, Form);
            if (!TryValidateModel(Form))
                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            myContext.TasacForms.Update(Form);
            await myContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task RemoveForm(int key)
        {
            var Form = await myContext.TasacForms.FirstOrDefaultAsync(item => item.FormId == key);
            if (Form == null)
                throw new ArgumentNullException();
            myContext.TasacForms.Remove(Form);
            await myContext.SaveChangesAsync();
        }


    }
}
