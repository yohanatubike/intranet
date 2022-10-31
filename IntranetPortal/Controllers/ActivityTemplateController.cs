using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using IntranetPortal.Models.Planning;
using IntranetPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace IntranetPortal.Controllers
{
    public class ActivityTemplateController : Controller
    {
        private IntranetDBContext _dbContext = new IntranetDBContext();
        private string? ValidationErrorMessage;

        [HttpGet]
        public async Task<IActionResult> GetActivities(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(_dbContext.Activities, loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> AddActivity(string values)
        {
            var activity = new Activity();
            JsonConvert.PopulateObject(values, activity);
            activity.CreatedBy = "yohana.tubike@tasac.go.tz";
            activity.CreatedDate = DateTime.UtcNow;

            if (!TryValidateModel(activity))
                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            try
            {
                _dbContext.Activities.Add(activity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok(activity);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateActivity(int key, string values)
        {
            var activity = await _dbContext.Activities.FirstOrDefaultAsync(item => item.Id == key);
            if (activity == null)
                throw new ArgumentNullException();
            JsonConvert.PopulateObject(values, activity);
            if (!TryValidateModel(activity))
                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            _dbContext.Activities.Update(activity);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task RemoveActivity(int key)
        {
            var activity = await _dbContext.Activities.FirstOrDefaultAsync(item => item.Id == key);
            if (activity == null)
                throw new ArgumentNullException();
            _dbContext.Activities.Remove(activity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
