using DevExtreme.AspNet.Mvc.FileManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using System.IO;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace IntranetPortal.Controllers
{
    [Authorize]
    public class ActivitiesAttachmentAPIController : Controller
    {
        IHostingEnvironment _hostingEnvironment;

        public ActivitiesAttachmentAPIController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        
        [Route("api/file-manager-file-system", Name = "FileManagementFileSystemApi")]
        public object FileSystem(FileSystemCommand command, string arguments)
        {
            var ActivityCode = HttpContext.Session.GetString("ActivityCode").ToString();
         
            string attachmentPath = "Attachments/Activities/"+ActivityCode.Trim('"');
            if (!Directory.Exists(attachmentPath))
            {
                DirectoryInfo directory = new DirectoryInfo(attachmentPath);
                Directory.CreateDirectory(attachmentPath);
            }
            var config = new FileSystemConfiguration
            {
                Request = Request,
                FileSystemProvider = new PhysicalFileSystemProvider(_hostingEnvironment.ContentRootPath + attachmentPath),
            
                AllowCopy = true,
                AllowCreate = true,
                AllowMove = true,
                AllowDelete = true,
                AllowRename = true,
                AllowUpload = true,
                AllowDownload = true,
                AllowedFileExtensions = new[] { ".pdf", ".json", ".css" }
            };
            var processor = new FileSystemCommandProcessor(config);
            var result = processor.Execute(command, arguments);
            return result.GetClientCommandResult();
        }
    }
}

