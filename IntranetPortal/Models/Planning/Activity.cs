using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntranetPortal.Models.Planning
{
    public class Activity
    {
        [Key]
        public long Id { get; set; } = 0;
        public string? Code { get; set; }
        public string? Description { get; set; }
        public long TargetId { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public double AnnualBudget { get; set; }
        //List of all implemented activities
        public virtual IEnumerable<ActivitiesDetail>? ActivityImplementations { get; set; }
    }
}