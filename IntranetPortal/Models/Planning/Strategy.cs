using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntranetPortal.Models.Planning
{
    public class Strategy
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public long ObjectiveId { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }

        public virtual IEnumerable<Target>? Targets { get; set; }
    }
}
