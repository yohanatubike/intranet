using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class Designation
    {
        public long DesignationId { get; set; }
        public string DesignationCode { get; set; } = null!;
        public string StaffDesignation { get; set; } = null!;
        public string SectionCode { get; set; } = null!;
        public string DepartmentCode { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public bool SupervisoryPostion { get; set; }
    }
}
