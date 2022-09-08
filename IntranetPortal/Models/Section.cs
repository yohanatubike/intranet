using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class Section
    {
        public long SectionId { get; set; }
        public string SectionName { get; set; } = null!;
        public string SectionCode { get; set; } = null!;
        public string? CreatedBy { get; set; }
        public string? DepartmentCode { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
