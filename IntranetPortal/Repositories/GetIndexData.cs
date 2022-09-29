using IntranetPortal.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IntranetPortal.Repositories
{
    public class GetIndexData
    {
        private IntranetDBContext myContext = new IntranetDBContext();

        public GetIndexData(IntranetDBContext myContext)
        {
            this.myContext = myContext;
        }
        public List<StaffNotification> GetActiveNotification()
        {
            return myContext.StaffNotifications.OrderByDescending(q => q.NotificationId)
                .Where(t => t.Status == "Activated").Select(notification => new StaffNotification()
                {
                    Subject = notification.Subject,
                    CreatedBy = notification.CreatedBy,
                    Category = notification.Category,

                }).Take(6).ToList();
        }
        public List<Permission> GetActivePermissions(string PFNumber)
        {
            return myContext.Permissions
                .Where(t => t.Pfnumber == PFNumber && t.Status == true).Select(permission => new Permission()
                {
                    PermissionName = permission.PermissionName,
                    Address = permission.Address


                }).ToList();
        }

        public List<Article> GetTopArticles()
        {
            return myContext.Articles.OrderByDescending(a => a.ArticleId).
                Select(article => new Article() 
                {
                    Title = article.Title,
                    Url = article.Url
                }).Take(3).ToList();
        }

        public List<FrontEndSlider> GetActiveSliders()
        {
            List<FrontEndSlider> sliders = myContext.FrontEndSliders.Where(f => f.PublishStatus.Equals("Active")).ToList();
            var path = Path.Combine("wwwroot", "Sliders");

            foreach (var slider in sliders)
            {
                if (slider.ImageFile != null)
                {
                    slider.ImageFile = Path.Combine("Sliders", slider.ImageFile);
                }
            }
            return sliders;
        }
    }
}
