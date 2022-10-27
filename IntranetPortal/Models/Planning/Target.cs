using DevExtreme.AspNet.Mvc.Builders;

namespace IntranetPortal.Models.Planning
{
    public class Target
    {
        public int Id { get; set; } = 0;
        public string Code { get; set; }
        public string Description { get; set; }
        public Indicator Indicator { get; set; }
        public TargetStatus Status { get; set; }
        public IEnumerable<Activity> Activities { get; set; }
    }
}