using IntranetPortal.Models;
using IntranetPortal.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IntranetPortal.Components
{
    public class ShowStaffNotificationsViewComponent:ViewComponent 
    {
        private readonly GetIndexData _getIndexData;
        public ShowStaffNotificationsViewComponent(GetIndexData getIndexData)
        {
          _getIndexData = getIndexData;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var activeNotification  =  _getIndexData.GetActiveNotification();
            return View(activeNotification);
        }
    }
}
