using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class UserRole
    {
        public long UserRoleId { get; set; }
        public long RoleId { get; set; }
        public string PFNumber { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}
