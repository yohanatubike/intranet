using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class Documentation
    {
        public long DocumentationId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? DepartmentCode { get; set; }
        public string? Url { get; set; }
        public string? Status { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? DocumentType { get; set; }
    }
}
