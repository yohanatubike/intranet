using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class Role
    {
        public long Id { get; set; }
        public string RoleName { get; set; } = null!;
        public DateTime? CretaedDate { get; set; }
        public string? CreatedBy { get; set; }
    }
}
