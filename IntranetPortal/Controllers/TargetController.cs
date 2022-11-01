using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using IntranetPortal.Models.Planning;
using IntranetPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace IntranetPortal.Controllers
{
    public class TargetController : Controller
    {
        private IntranetDBContext _dbContext = new IntranetDBContext();
        private string? ValidationErrorMessage;

        [HttpGet]
        public async Task<IActionResult> GetTargets(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(_dbContext.Targets, loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = "";
            try
            {
                resultJson = JsonConvert.SerializeObject(result, settings);
            } catch(Exception e)
            {
                return Ok(e.Message);
            }
            return Content(resultJson, "application/json");
        }

        public object GetServiceOutputTargets(int id, DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(_dbContext.Targets.Where(s => s.ServiceOutputId == id), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> AddTarget(string values)
        {
            var target = new Target();
            JsonConvert.PopulateObject(values, target);
            target.CreatedBy = "yohana.tubike@tasac.go.tz";
            target.CreatedDate = DateTime.UtcNow;

            if (!TryValidateModel(target))
                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            try
            {
                _dbContext.Targets.Add(target);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok(target);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTarget(int key, string values)
        {
            var target = await _dbContext.Targets.FirstOrDefaultAsync(item => item.Id == key);
            if (target == null)
                throw new ArgumentNullException();
            JsonConvert.PopulateObject(values, target);
            if (!TryValidateModel(target))
                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            _dbContext.Targets.Update(target);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task RemoveTarget(int key)
        {
            var target = await _dbContext.Targets.FirstOrDefaultAsync(item => item.Id == key);
            if (target == null)
                throw new ArgumentNullException();
            _dbContext.Targets.Remove(target);
            await _dbContext.SaveChangesAsync();
        }
    }
}