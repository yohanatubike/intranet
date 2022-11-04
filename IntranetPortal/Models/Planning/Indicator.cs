using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntranetPortal.Models.Planning
{
    public class Indicator
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public bool LessIsGood { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }
}