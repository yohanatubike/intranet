using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class ActivitiesDetail
    {
        public long ActivityId { get; set; }
        public string ActivityCode { get; set; } = null!;
        public string ActivityName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string DepartmentCode { get; set; } = null!;
        public string? SectionCode { get; set; }
        public string ImpelementationStatus { get; set; } = null!;
        public string Priority { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
