using DevExtreme.AspNet.Mvc.FileManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using System.IO;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
namespace IntranetPortal.Controllers
{
    public class MeetingAttachmentsAPIController : Controller
    {
        IHostingEnvironment _hostingEnvironment;

        public MeetingAttachmentsAPIController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        [Route("api/file-manager-file-system_meeting", Name = "FileManagementFileSystemMeetingsApi")]
        public object FileSystem(FileSystemCommand command, string arguments)
        {
            var MeetingCode = HttpContext.Session.GetString("MeetingCode").ToString();

            string attachmentPath = "Attachments/Meetings/" + MeetingCode.Trim('"');
            if (!Directory.Exists(attachmentPath))
            {
                DirectoryInfo directory = new DirectoryInfo(attachmentPath);
                Directory.CreateDirectory(attachmentPath);
            }
            var config = new FileSystemConfiguration
            {
                Request = Request,
                FileSystemProvider = new PhysicalFileSystemProvider(_hostingEnvironment.ContentRootPath + attachmentPath),
                //uncomment the code below to enable file/folder management
                AllowCopy = true,
                AllowCreate = true,
                AllowMove = true,
                AllowDelete = true,
                AllowRename = true,
                AllowUpload = true,
                AllowDownload = true,
                AllowedFileExtensions = new[] { ".pdf", ".docx", ".doc", ".xls", ".xlsx" }
            };
            var processor = new FileSystemCommandProcessor(config);
            var result = processor.Execute(command, arguments);
            return result.GetClientCommandResult();
        }
    }
}
