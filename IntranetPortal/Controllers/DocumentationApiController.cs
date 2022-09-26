using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using IntranetPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace IntranetPortal.Controllers
{

    public class DocumentationApiController : Controller
    {
        private IntranetDBContext _dbContext = new IntranetDBContext();
        private string? ValidationErrorMessage;

        [HttpGet]
        public async Task<IActionResult> GetDocumentations(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(_dbContext.Documentations, loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        [HttpGet]
        public async Task<IActionResult> GetPolicies(DataSourceLoadOptions loadOptions)
        {
            var results = DataSourceLoader.Load(_dbContext.Documentations.Where(d => d.DocumentType == "Policy"), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultsJson = JsonConvert.SerializeObject(results, settings);
            return Content(resultsJson, "application/json");
        }

        [HttpGet]
        public async Task<IActionResult> GetGuidelines(DataSourceLoadOptions loadOptions)
        {
            var results = DataSourceLoader.Load(_dbContext.Documentations.Where(d => d.DocumentType == "Guideline"), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultsJson = JsonConvert.SerializeObject(results, settings);
            return Content(resultsJson, "application/json");
        }

        [HttpGet]
        public async Task<IActionResult> GetProcedures(DataSourceLoadOptions loadOptions)
        {
            var results = DataSourceLoader.Load(_dbContext.Documentations.Where(d => d.DocumentType == "Procedure"), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultsJson = JsonConvert.SerializeObject(results, settings);
            return Content(resultsJson, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> AddDocumentation(string values)
        {
            var documentation = new Documentation();
            JsonConvert.PopulateObject(values, documentation);

            if (!TryValidateModel(documentation))
                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            documentation.CreatedBy = "mustafa.juma@tasac.go.tz";
            documentation.CreatedDate = DateTime.UtcNow;
            try
            {
                _dbContext.Documentations.Add(documentation);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok(documentation);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDocumentation(int key, string values)
        {
            var documentation = await _dbContext.Documentations.FirstOrDefaultAsync(item => item.DocumentationId == key);
            if (documentation == null)
                throw new ArgumentNullException();
            JsonConvert.PopulateObject(values, documentation);
            if (!TryValidateModel(documentation))
                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            _dbContext.Documentations.Update(documentation);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task RemoveDocumentation(int key)
        {
            var documentation = await _dbContext.Documentations.FirstOrDefaultAsync(item => item.DocumentationId == key);
            if (documentation == null)
                throw new ArgumentNullException();
            _dbContext.Documentations.Remove(documentation);
            await _dbContext.SaveChangesAsync();
        }
    }
}
