using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class ActivitiesComment
    {
        public long ActivitiesCommentsId { get; set; }
        public string Comment { get; set; } = null!;
        public long ActivityId { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
    }
}
