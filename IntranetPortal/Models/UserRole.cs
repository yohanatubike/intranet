using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntranetPortal.Models
{
    public partial class UserRole
    {
        public long UserRoleId { get; set; }
        public long RoleId { get; set; }
        public string PFNumber { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Roles { get; set; }
    }
}
