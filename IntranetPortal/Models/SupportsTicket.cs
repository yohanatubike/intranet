using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class SupportsTicket
    {
        public long TicketId { get; set; }
        public string DepartmentCode { get; set; } = null!;
        public string TicketTypes { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string LodgedBy { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? AttendedBy { get; set; }
        public string? OfficersComments { get; set; }
        public DateTime? AttendedDate { get; set; }
        public string? SupervisorsComments { get; set; }
        public string Status { get; set; } = null!;
    }
}
