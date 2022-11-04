using DevExtreme.AspNet.Mvc.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntranetPortal.Models.Planning
{
    public class Target
    {
        public long Id { get; set; } = 0;
        public string? Code { get; set; }
        public string? Description { get; set; }
        public long ServiceOutputId { get; set; }
        public long IndicatorId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? Status { get; set; }
        [ForeignKey("IndicatorId")]
        public virtual Indicator? Indicator { get; set; }

        public virtual IEnumerable<Activity>? Activities { get; set; }
    }
}