using IntranetPortal.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IntranetPortal.Components
{
    public class ShowUpcomingBirthDaysViewComponent:ViewComponent
    {

        private readonly GetIndexData _repo;

        public ShowUpcomingBirthDaysViewComponent(GetIndexData getIndexData, IHttpContextAccessor haccess)
        {
            _repo = getIndexData;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var upcomingBirthDays = _repo.GetWeeklyBirthDay();
            return View(upcomingBirthDays);

        }
    }
}
