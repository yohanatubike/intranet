using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using IntranetPortal.Models.Planning;
using IntranetPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace IntranetPortal.Controllers
{
    public class StrategyController : Controller
    {
        private IntranetDBContext _dbContext = new IntranetDBContext();
        private string? ValidationErrorMessage;

        [HttpGet]
        public async Task<IActionResult> GetStrategies(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(_dbContext.Strategies, loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        [HttpGet]
        public object GetObjectiveStrategies(int id, DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(_dbContext.Strategies.Where(s => s.ObjectiveId == id), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> AddStrategy(string values)
        {
            var strategy = new Strategy();
            JsonConvert.PopulateObject(values, strategy);

            if (!TryValidateModel(strategy))
                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            strategy.CreatedBy = "yohana.tubike@tasac.go.tz";
            strategy.CreatedDate = DateTime.UtcNow;
            try
            {
                _dbContext.Strategies.Add(strategy);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok(strategy);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStrategy(int key, string values)
        {
            var strategy = await _dbContext.Strategies.FirstOrDefaultAsync(item => item.Id == key);
            if (strategy == null)
                throw new ArgumentNullException();
            JsonConvert.PopulateObject(values, strategy);
            if (!TryValidateModel(strategy))
                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            _dbContext.Strategies.Update(strategy);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task RemoveStrategy(int key)
        {
            var strategy = await _dbContext.Strategies.FirstOrDefaultAsync(item => item.Id == key);
            if (strategy == null)
                throw new ArgumentNullException();
            _dbContext.Strategies.Remove(strategy);
            await _dbContext.SaveChangesAsync();
        }
    }
}
