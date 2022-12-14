using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class Permission
    {
        public long PermissionId { get; set; }
        public string Pfnumber { get; set; } = null!;
        public string PermissionName { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public bool? Status { get; set; }
        public string? Address { get; set; }
    }
}
