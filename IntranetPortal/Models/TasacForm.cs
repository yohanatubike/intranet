using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class TasacForm
    {
        public int FormId { get; set; }
        public string FormName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? FileUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string DepartmentCode { get; set; } = null!;
        public string? Status { get; set; }
    }
}
