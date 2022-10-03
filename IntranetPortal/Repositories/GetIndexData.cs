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
        public List<ActivitiesDetail> GetActiveOngoingActivities()
        {
            return myContext.ActivitiesDetails.OrderByDescending(q => q.PublishedDate)
                .Where(t => t.PublishStatus == "Published").Select(activity => new ActivitiesDetail()
                {
                    Title = activity.Title,
                    DepartmentCode = activity.DepartmentCode,
                

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
        public List<NewsEvent> GetActiveTopNews()
        {
            return myContext.NewsEvents.OrderByDescending(q => q.CreatedDate)
                .Where(t => t.Status == "Published" && t.IsTopNews == true).Select(newsDetails => new  NewsEvent()
                {
                    Title =  newsDetails.Title,
                    CreatedBy = newsDetails.CreatedBy,

                    IsTopNews = newsDetails.IsTopNews,
                    Description = newsDetails.Description.Substring(0, Math.Min(newsDetails.Description.Length, 235)),
                   

                }).Take(1).ToList();
        }
        public List<NewsEvent> GetActivePreviousNews()
        {
            return myContext.NewsEvents.OrderByDescending(q => q.CreatedDate)
                .Where(t => t.Status == "Published").Select(newsDetails => new NewsEvent()
                {
                    Title = newsDetails.Title,
                    CreatedBy = newsDetails.CreatedBy,

                    IsTopNews = newsDetails.IsTopNews,
                


                }).Take(6).ToList();
        }
    }
}
