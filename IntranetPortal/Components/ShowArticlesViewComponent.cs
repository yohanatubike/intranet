using IntranetPortal.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IntranetPortal.Components
{
    public class ShowArticlesViewComponent : ViewComponent
    {
        private readonly GetIndexData _repo;

        public ShowArticlesViewComponent(GetIndexData getIndexData, IHttpContextAccessor haccess)
        {
            _repo = getIndexData;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var activeArticles = _repo.GetTopArticles();
            return View(activeArticles);

        }
    }
}
