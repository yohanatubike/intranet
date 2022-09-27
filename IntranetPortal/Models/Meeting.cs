using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class Meeting
    {
        public long MeetingId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string DepartmentCode { get; set; } = null!;
        public string SectionCode { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string? UpdatedBy { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Location { get; set; } = null!;
        public string MeetingCode { get; set; } = null!;
    }
}
