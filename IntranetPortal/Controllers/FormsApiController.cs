
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using IntranetPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace IntranetPortal.Controllers
{
    //[Authorize]
    public class FormsApiController : Controller
    {
        private IntranetDBContext _dbContext = new IntranetDBContext();
        string ValidationErrorMessage;

        [HttpGet]
        public async Task<IActionResult> GetForms(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(_dbContext.TasacForms, loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        public async Task<IActionResult> GetITForms(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(_dbContext.TasacForms.Where(f => f.DepartmentCode.Equals("ICTS")), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        public async Task<IActionResult> GetHRForms(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(_dbContext.TasacForms.Where(f => f.DepartmentCode.Equals("DCS")), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        public async Task<IActionResult> GetFAUForms(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(_dbContext.TasacForms.Where(f => f.DepartmentCode.Equals("FAU")), loadOptions);
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
            Form.CreatedDate = DateTime.Now;
            try
            {
                _dbContext.TasacForms.Add(Form);
                await _dbContext.SaveChangesAsync();
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
            var Form = await _dbContext.TasacForms.FirstOrDefaultAsync(item => item.FormId == key);
            if (Form == null)
                throw new ArgumentNullException();
            JsonConvert.PopulateObject(values, Form);
            if (!TryValidateModel(Form))
                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            _dbContext.TasacForms.Update(Form);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task RemoveForm(int key)
        {
            var Form = await _dbContext.TasacForms.FirstOrDefaultAsync(item => item.FormId == key);
            if (Form == null)
                throw new ArgumentNullException();
            _dbContext.TasacForms.Remove(Form);
            await _dbContext.SaveChangesAsync();
        }
    }
}