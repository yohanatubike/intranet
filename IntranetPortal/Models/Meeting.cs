using System;
using System.Collections.Generic;
using System.Collections;

namespace IntranetPortal.Models
{
    public partial class Meeting
    {
        public long MeetingId { get; set; }
        public BitArray MeetingCode { get; set; } = null!;
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
    }
}
