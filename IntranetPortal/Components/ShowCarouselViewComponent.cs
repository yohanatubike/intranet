using IntranetPortal.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IntranetPortal.Components
{
    public class ShowCarouselViewComponent : ViewComponent
    {
        private readonly GetIndexData _repo;

        public ShowCarouselViewComponent(GetIndexData getIndexData, IHttpContextAccessor haccess)
        {
            _repo = getIndexData;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var activeSlider = _repo.GetActiveSliders();
            return View(activeSlider);
        }
    }
}
