using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class StaffNotification
    {
        public long NotificationId { get; set; }
        public string Pfnumber { get; set; } = null!;
        public string NotificationType { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime? UpdatedDate { get; set; }
    }
}
