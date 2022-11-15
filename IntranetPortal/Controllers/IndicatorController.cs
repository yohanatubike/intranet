using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using IntranetPortal.Models.Planning;
using IntranetPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace IntranetPortal.Controllers
{
    public class IndicatorController : Controller
    {
        private IntranetDBContext _dbContext = new IntranetDBContext();
        private string? ValidationErrorMessage;

        [HttpGet]
        public async Task<IActionResult> GetIndicators(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(_dbContext.Indicators, loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> AddIndicator(string values)
        {
            var indicator = new Indicator();
            JsonConvert.PopulateObject(values, indicator);
            indicator.CreatedBy = "yohana.tubike@tasac.go.tz";
            indicator.CreatedDate = DateTime.UtcNow;

            if (!TryValidateModel(indicator))
                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            try
            {
                _dbContext.Indicators.Add(indicator);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok(indicator);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateIndicator(int key, string values)
        {
            var indicator = await _dbContext.Indicators.FirstOrDefaultAsync(item => item.Id == key);
            if (indicator == null)
                throw new ArgumentNullException();
            JsonConvert.PopulateObject(values, indicator);
            if (!TryValidateModel(indicator))
                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            _dbContext.Indicators.Update(indicator);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task RemoveIndicator(int key)
        {
            var indicator = await _dbContext.Indicators.FirstOrDefaultAsync(item => item.Id == key);
            if (indicator == null)
                throw new ArgumentNullException();
            _dbContext.Indicators.Remove(indicator);
            await _dbContext.SaveChangesAsync();
        }
    }
}
