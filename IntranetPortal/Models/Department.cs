using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class Department
    {
        public long DepartmentId { get; set; }
        public string DepartmentCode { get; set; } = null!;
        public string DepartmentName { get; set; } = null!;
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
