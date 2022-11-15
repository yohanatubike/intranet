using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using IntranetPortal.Models;
using IntranetPortal.Models.Planning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace IntranetPortal.Controllers
{
    public class ObjectiveController : Controller
    {
        private IntranetDBContext _dbContext = new IntranetDBContext();
        private string? ValidationErrorMessage;

        [HttpGet]
        public async Task<IActionResult> GetObjectives(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(_dbContext.Objectives, loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> AddObjective(string values)
        {
            var objective = new Objective();
            JsonConvert.PopulateObject(values, objective);
            objective.CreatedBy = "yohana.tubike@tasac.go.tz";
            objective.CreatedDate = DateTime.UtcNow;

            if (!TryValidateModel(objective))
                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            try
            {
                _dbContext.Objectives.Add(objective);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok(objective);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateObjective(int key, string values)
        {
            var objective = await _dbContext.Objectives.FirstOrDefaultAsync(item => item.Id == key);
            if (objective == null)
                throw new ArgumentNullException();
            JsonConvert.PopulateObject(values, objective);
            if (!TryValidateModel(objective))
                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            _dbContext.Objectives.Update(objective);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task RemoveObjective(int key)
        {
            var objective = await _dbContext.Objectives.FirstOrDefaultAsync(item => item.Id == key);
            if (objective == null)
                throw new ArgumentNullException();
            _dbContext.Objectives.Remove(objective);
            await _dbContext.SaveChangesAsync();
        }
    }
}
