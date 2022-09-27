using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class QuizeManager
    {
        public long QuizManagerId { get; set; }
        public string Description { get; set; } = null!;
        public string Details { get; set; } = null!;
        public bool? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }
}
