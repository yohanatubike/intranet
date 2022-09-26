using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class NotificationComment
    {
        public long NitificationCommentId { get; set; }
        public long NotificationId { get; set; }
        public string Details { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}
