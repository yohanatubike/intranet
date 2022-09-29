using IntranetPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntranetPortal.Controllers
{
    public class ContentManagerController : Controller
    {
        private IntranetDBContext myContext = new IntranetDBContext();

        public IActionResult Index( string parameter)
        {
            switch (parameter)
            {
                case "documentation":
                    return RedirectToAction("Index", "Documentation");
                    break;
                case "activity":
                    return View("ManageActivities");
                    break;
                case "frontslider":
                    return View("ManageFrontSlider");
                    break;
                case "forms":
                    return RedirectToAction("Index", "Forms");
                    break;
                case "systems":
                    return RedirectToAction("Index", "Systems");
                    break;
                case "library":
                    RedirectToAction("Index", "Library");
                    break;
                case "quiz":
                    return View("ManageQuiz");
                    break;
                case "articles":
                    return View("ManageArticles");
                    break;
                case "newsevents":
                    return View("ManageNewsEvents");
                    break;
                case "tips":
                    return View("ManageTips");
                    break;
            }
            return RedirectToAction("Index", "StaffPage");
        }
        public IActionResult ManageActivities()
        {
            return View();
        }
        public IActionResult ManageTips()
        {
            return View();
        }
        public IActionResult ManageArticles()
        {
            return View();
        }
        public IActionResult ManageNewsEvents()
        {
            return View();
        }
        public IActionResult ManageFrontSlider()
        {
            return View();
        }

        public IActionResult UploadArticle()
        {
            return View();
        }


        [HttpPost]
        public ActionResult UploadArticle(IFormFile myFile, [FromQuery(Name = "ArticleId")] long id)
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

        private async void UpdateForm(long articleId, dynamic fileName)
        {
            var result = await myContext.Articles.FirstOrDefaultAsync(item => item.ArticleId == articleId);
            if (result != null)
            {
                result.Url = fileName.ToString();
                await myContext.SaveChangesAsync();
            }
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

        private void SaveFile(IFormFile file, string fileName)
        {
            try
            {
                fileName = fileName + ".pdf";
                var path = Path.Combine("Attachments", "Articles");
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

        [HttpGet]
        public ActionResult GetArticlePdf([FromQuery(Name = "Filename")] string filename)
        {
            var path = Path.Combine("Attachments", "Articles");
            string filePath = Path.Combine(path, filename);
            Response.Headers.Add("Content-Disposition", "inline; filename=test.pdf");
            var stream = new FileStream(filePath, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

    }
}
