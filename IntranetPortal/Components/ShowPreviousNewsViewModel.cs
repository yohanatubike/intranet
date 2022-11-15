using IntranetPortal.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IntranetPortal.Components
{
    public class ShowPreviousNewsViewComponent:ViewComponent
    {
        private readonly GetIndexData _getIndexData;
        public ShowPreviousNewsViewComponent(GetIndexData getIndexData)
        {
            _getIndexData = getIndexData;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var activePrevious = _getIndexData.GetActivePreviousNews();
            return View(activePrevious);
        }
    }
}
