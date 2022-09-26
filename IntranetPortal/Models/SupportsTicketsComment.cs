using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class SupportsTicketsComment
    {
        public long CommentId { get; set; }
        public long TicketId { get; set; }
        public string Comment { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}
