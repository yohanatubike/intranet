using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class PermissionCategory
    {
        public long PermissionCategoryId { get; set; }
        public string PermissionName { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}
