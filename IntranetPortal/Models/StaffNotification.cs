using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class StaffNotification
    {
        public long NotificationId { get; set; }
        public string Pfnumber { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Details { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? EnableComment { get; set; }
        public string? UpdatedBy { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string Subject { get; set; } = null!;
    }
}
