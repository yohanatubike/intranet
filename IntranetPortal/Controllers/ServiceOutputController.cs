using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using IntranetPortal.Models.Planning;
using IntranetPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace IntranetPortal.Controllers
{
    public class ServiceOutputController : Controller
    {
        private IntranetDBContext _dbContext = new IntranetDBContext();
        private string? ValidationErrorMessage;

        [HttpGet]
        public async Task<IActionResult> GetServiceOutputs(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(_dbContext.ServiceOutputs, loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        [HttpGet]
        public object GetObjectiveServiceOutputs(int id, DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(_dbContext.ServiceOutputs.Where(s => s.Id == id), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> AddServiceOutput(string values)
        {
            var serviceOutput = new ServiceOutput();
            JsonConvert.PopulateObject(values, serviceOutput);

            if (!TryValidateModel(serviceOutput))
                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            serviceOutput.CreatedBy = "yohana.tubike@tasac.go.tz";
            serviceOutput.CreatedDate = DateTime.UtcNow;
            try
            {
                _dbContext.ServiceOutputs.Add(serviceOutput);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok(serviceOutput);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateServiceOutput(int key, string values)
        {
            var serviceOutput = await _dbContext.ServiceOutputs.FirstOrDefaultAsync(item => item.Id == key);
            if (serviceOutput == null)
                throw new ArgumentNullException();
            JsonConvert.PopulateObject(values, serviceOutput);
            if (!TryValidateModel(serviceOutput))
                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            _dbContext.ServiceOutputs.Update(serviceOutput);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task RemoveServiceOutput(int key)
        {
            var serviceOutput = await _dbContext.ServiceOutputs.FirstOrDefaultAsync(item => item.Id == key);
            if (serviceOutput == null)
                throw new ArgumentNullException();
            _dbContext.ServiceOutputs.Remove(serviceOutput);
            await _dbContext.SaveChangesAsync();
        }
    }
}
