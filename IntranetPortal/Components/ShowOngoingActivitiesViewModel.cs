using IntranetPortal.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IntranetPortal.Components
{
    public class ShowOngoingActivitiesViewComponent: ViewComponent
    {
        private readonly GetIndexData _getActivityData;
    
        public ShowOngoingActivitiesViewComponent(GetIndexData getIndexData, IHttpContextAccessor haccess)
        {
            
            
            _getActivityData = getIndexData;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var activeActivities = _getActivityData.GetActiveOngoingActivities();
            return View(activeActivities);

        }
    }
}
