using IntranetPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntranetPortal.Controllers
{
    public class FormsController : Controller
    {
        private IntranetDBContext _dbContext = new IntranetDBContext();
        string ValidationErrorMessage;
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ITForms()
        {
            return View();
        }
        public IActionResult FAUForms()
        {
            return View();
        }
        public IActionResult HRForms()
        {
            return View();
        }

        public IActionResult UploadForm()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetPdf([FromQuery(Name = "Filename")] string filename)
        {
            var path = Path.Combine("Attachments", "Forms");
            string filePath = Path.Combine(path, filename);
            Response.Headers.Add("Content-Disposition", "inline; filename=test.pdf");
            var stream = new FileStream(filePath, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [HttpPost]
        public ActionResult UploadForm(IFormFile myFile, [FromQuery(Name = "FormId")] long id)
        {
            ViewBag.Id = id;

            if (myFile != null)
            {
                ViewBag.File = GenerateFileName();
                SaveFile(myFile, ViewBag.File.ToString());
                UpdateForm(id, fileName: ViewBag.File + ".pdf");
            }
            return View("SubmissionResult");
        }

        private async void UpdateForm(long formId, dynamic fileName)
        {
            var result = await _dbContext.TasacForms.FirstOrDefaultAsync(item => item.FormId == formId);
            if (result != null)
            {
                result.FileUrl = fileName.ToString();
                await _dbContext.SaveChangesAsync();
            }
        }

        [HttpPost]
        public ActionResult Upload(int DocumentationId)
        {
            IFormFile? file = Request.Form.Files["myFile"];
            string fileName = GenerateFileName();
            SaveFile(file, fileName);
            return new EmptyResult();
        }

        private string GenerateFileName()
        {
            Random res = new Random();
            String str = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int size = 18;
            String ran = "";

            for (int i = 0; i < size; i++)
            {
                int x = res.Next(62);
                ran = ran + str[x];
            }
            return ran;
        }

        void SaveFile(IFormFile file, string fileName)
        {
            try
            {
                fileName = fileName + ".pdf";
                var path = Path.Combine("Attachments", "Forms");
                // save the file
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                using (var fileStream = System.IO.File.Create(Path.Combine(path, fileName)))
                {
                    file.CopyTo(fileStream);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }
        }
    }
}
