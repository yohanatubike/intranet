using IntranetPortal.Models;
using IntranetPortal.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IntranetPortal.Components
{
    public class ShowPermissionsViewComponent:ViewComponent
    {
        private readonly GetIndexData _getPermissionData;
        private readonly HttpContext hcontext;
        string PFNumber = null;
        public ShowPermissionsViewComponent(GetIndexData getIndexData, IHttpContextAccessor haccess)
        {
            hcontext = haccess.HttpContext;
            PFNumber = hcontext.User.FindFirst(ClaimTypes.SerialNumber).Value;
            _getPermissionData = getIndexData;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            var activeNotification = _getPermissionData.GetActivePermissions(PFNumber);
            return View(activeNotification);

        }
    }
}
