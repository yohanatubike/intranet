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
        public string? AssignedTo { get; set; }
        public DateTime? AttendedDate { get; set; }
        public string Status { get; set; } = null!;
        public DateTime? ClosedDate { get; set; }
        public string? AssignedStatus { get; set; }
        public string? AssignedBy { get; set; }
        public string? ClosedBy { get; set; }
        public string? ServiceCategoryCode { get; set; }
        public DateTime? AssignedDate { get; set; }
    }
}
