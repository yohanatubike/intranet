using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntranetPortal.Models
{
    public partial class AssignedOfficersDetail
    {
        public long AssignedOfficerDetailsId { get; set; }
        public string Pfnumber { get; set; } = null!;
        public bool IsForcalPerson { get; set; }
        public DateTime AssignedDate { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string? UpdateBy { get; set; }
        public long ActivityId { get; set; }
        [ForeignKey("ActivityId")]
        public virtual ActivitiesDetail Activity { get; set; } = null!;
    }
}
