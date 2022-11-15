using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class TasacSystem
    {
        public int SystemId { get; set; }
        public string? SystemName { get; set; }
        public string? Description { get; set; }
        public string? SystemUrl { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? DepartmentCode { get; set; }
        public string? SystemType { get; set; }
    }
}
