using IntranetPortal.Models.Planning;
using NuGet.Packaging.Signing;
using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public class ActivitiesDetail
    {
        public long ActivityId { get; set; }
        public long ActivityTemplateId { get; set; }
        public string ActivityCode { get; set; } = null!;
        public string ActivityName { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string DepartmentCode { get; set; } = null!;
        public string? SectionCode { get; set; }
        public string ImpelementationStatus { get; set; } = null!;
        public string Priority { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Title { get; set; }
        public string? PublishStatus { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string? PublishdBy { get; set; }
        public string? ExternalDetails { get; set; }
        public bool IsBudgeted { get; set; }

        //For Planning and Monitoring
        public string? Output { get; set; }
        public string? Outcome { get; set; }
        public string? Finding { get; set; }
        public string? Lesson { get; set; }
        public double? Expenditure { get; set; }
        public double? ActualProgress { get; set; }
        public string? Remarks { get; set; }
    }
}