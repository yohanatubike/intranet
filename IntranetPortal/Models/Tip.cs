using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class Tip
    {
        public long TipsId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }
}
