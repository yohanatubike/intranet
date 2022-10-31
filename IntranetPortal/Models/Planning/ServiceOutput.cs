using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntranetPortal.Models.Planning
{
    public class ServiceOutput
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int ObjectiveId { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }


        public virtual IEnumerable<Target> Targets { get; set; }
        public virtual Objective Objective { get; set; }
        public virtual string ObjectiveCode { get; set; }

    }
}
