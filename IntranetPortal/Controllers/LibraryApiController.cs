using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using IntranetPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace IntranetPortal.Controllers
{
    public class LibraryApiController : Controller
    {
        private IntranetDBContext _dbContext = new IntranetDBContext();
        string ValidationErrorMessage;

        [HttpGet]
        public async Task<IActionResult> GetLibraries(DataSourceLoadOptions loadOptions)
        {
            var result = DataSourceLoader.Load(_dbContext.TasacLibraries, loadOptions);
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            var resultJson = JsonConvert.SerializeObject(result, settings);
            return Content(resultJson, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> AddLibrary(string values)
        {
            var Library = new TasacLibrary();
            JsonConvert.PopulateObject(values, Library);

            if (!TryValidateModel(Library))
                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            Library.CreatedBy = "yohana.tubike@tasac.go.tz";
            Library.CreatedDate = DateTime.UtcNow;
            try
            {
                _dbContext.TasacLibraries.Add(Library);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            return Ok(Library);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLibrary(int key, string values)
        {
            var Library = await _dbContext.TasacLibraries.FirstOrDefaultAsync(item => item.LibraryId == key);
            if (Library == null)
                throw new ArgumentNullException();
            JsonConvert.PopulateObject(values, Library);
            if (!TryValidateModel(Library))
                return BadRequest(ValidationErrorMessage = "Failed to save details due to validation error");
            _dbContext.TasacLibraries.Update(Library);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task RemoveLibrary(int key)
        {
            var Library = await _dbContext.TasacLibraries.FirstOrDefaultAsync(item => item.LibraryId == key);
            if (Library == null)
                throw new ArgumentNullException();
            _dbContext.TasacLibraries.Remove(Library);
            await _dbContext.SaveChangesAsync();
        }
    }
}
