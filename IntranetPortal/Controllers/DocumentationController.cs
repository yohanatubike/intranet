using DevExtreme.AspNet.Data;
using IntranetPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntranetPortal.Controllers
{
    public class DocumentationController : Controller
    {
        private readonly HttpContext? hcontext;
        private IntranetDBContext _dbContext = new IntranetDBContext();

        public DocumentationController(IHttpContextAccessor haccess)
        {
            hcontext = haccess.HttpContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Policies()
        {
            return View();
        }
        public IActionResult Guidelines()
        {
            return View();
        }
        public IActionResult Procedures()
        {
            return View();
        }

        public ActionResult UploadDocument()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetPdf([FromQuery(Name = "Filename")] string filename)
        {
            var path = Path.Combine("Attachments", "Documentations");
            string filePath = Path.Combine(path, filename);
            Response.Headers.Add("Content-Disposition", "inline; filename=test.pdf");
            var stream = new FileStream(filePath, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [HttpPost]
        public ActionResult UploadDocument(IFormFile myFile, [FromQuery(Name = "DocumentId")] long id)
        {
            ViewBag.Id = id;

            if (myFile != null)
            {
                ViewBag.File = GenerateFileName();
                SaveFile(myFile, ViewBag.File.ToString());
                UpdateDocumentation(id, fileName: ViewBag.File + ".pdf");
            }
            return View("SubmissionResult");
        }

        private async void UpdateDocumentation(long documentId, dynamic fileName)
        {
            var result = await _dbContext.Documentations.FirstOrDefaultAsync(item => item.DocumentationId == documentId);
            if (result != null)
            {
                result.Url = fileName.ToString();
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
                var path = Path.Combine("Attachments", "Documentations");
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
