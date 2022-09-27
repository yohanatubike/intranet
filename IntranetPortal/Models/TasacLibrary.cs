using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class TasacLibrary
    {
        public int LibraryId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? FileUrl { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string DepartmentCode { get; set; } = null!;
    }
}
