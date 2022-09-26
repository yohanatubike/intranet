using Microsoft.AspNetCore.Mvc;
using IntranetPortal.Models;
using Newtonsoft.Json;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntranetPortal.Controllers
{
    public class SystemsApiController : Controller
    {
        private readonly IntranetDBContext _context;

        public SystemsApiController(IntranetDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetSystems(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(_context.TasacSystems, loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        [HttpGet]
        public async Task<IActionResult> GetCoreSystems(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(_context.TasacSystems.Where(s => s.SystemType == "Core"), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        [HttpGet]
        public async Task<IActionResult> GetSupportSystems(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(_context.TasacSystems.Where(s => s.SystemType == "Support"), loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }
            
        [HttpPost]
        public async Task<IActionResult> Create(string values)
        {
            var System = new TasacSystem();
            JsonConvert.PopulateObject(values, System);
            if (!TryValidateModel(System))
                return BadRequest("Failed to save details");
            try
            {
                _context.TasacSystems.Add(System);
                await _context.SaveChangesAsync();
            } catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
            return Ok(values);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int key, string values)
        {
            var System = await _context.TasacSystems.FirstOrDefaultAsync(item => item.SystemId == key);            
            if (System == null)
                throw new ArgumentNullException();
            
            JsonConvert.PopulateObject(values, System);            
            if (!TryValidateModel(System))
                return BadRequest("Failed to save details due to validation error");
            
            try { 
            _context.TasacSystems.Update(System);
            await _context.SaveChangesAsync();
            } catch(Exception e)
            {
                return Ok(e.Message);
            }
            return Ok();
        }

        [HttpDelete]
        public async Task DeleteSystem(int key)
        {
            var System = await _context.TasacSystems.FirstOrDefaultAsync(item => item.SystemId == key);
            if (System == null)
                throw new ArgumentNullException();
            _context.TasacSystems.Remove(System);
            await _context.SaveChangesAsync();
        }

        private bool TasacSystemExists(int key)
        {
          return _context.TasacSystems.Any(e => e.SystemId == key);
        }
    }
}
