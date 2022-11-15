using IntranetPortal.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IntranetPortal.Components
{
   
    public class ShowTopNewsViewComponent : ViewComponent
    {
        private readonly GetIndexData _getIndexData;
        public ShowTopNewsViewComponent(GetIndexData getIndexData)
        {
            _getIndexData = getIndexData;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var activeNews = _getIndexData.GetActiveTopNews();
            return View(activeNews);
        }
    }
}
