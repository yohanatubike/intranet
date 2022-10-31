using DevExtreme.AspNet.Mvc.FileManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace IntranetPortal.Controllers
{
    [Authorize]
    public class AuditingAttachmentAPIController : Controller
    {
     
 

    
        IHostingEnvironment _hostingEnvironment;

        public AuditingAttachmentAPIController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        [Route("api/file-manager-file-system-auditing", Name = "AuditingManagementFileSystemApi")]
        public object FileSystem(FileSystemCommand command, string arguments)
        {
            var DepartmentCode = HttpContext.Session.GetString("DepartmentCode").ToString();

            string attachmentPath = "Attachments/Auditing/"+ DepartmentCode.Trim('"');
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
                AllowedFileExtensions = new[] { ".pdf", ".docx" , ".doc" , ".xls", ".xlsx"}
            };
            var processor = new FileSystemCommandProcessor(config);
            var result = processor.Execute(command, arguments);
            return result.GetClientCommandResult();
        }
    }
}
