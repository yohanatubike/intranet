using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class MeetingComment
    {
        public long MeetingCommentId { get; set; }
        public string Pfnumber { get; set; } = null!;
        public long MeetingId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
    }
}
