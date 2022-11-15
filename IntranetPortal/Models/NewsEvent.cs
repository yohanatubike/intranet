using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class NewsEvent
    {
        public long NewsEventsId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Status { get; set; }
        public bool? IsTopNews { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string UpdatedBy { get; set; } 
    }
}
