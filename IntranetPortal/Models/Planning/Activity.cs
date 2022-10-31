using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntranetPortal.Models.Planning
{
    public class Activity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; } = 0;
        public string Code { get; set; }
        public string Description { get; set; }
        public double AnnualBudget { get; set; }
        public Section Section { get; set; }
        public IEnumerable<ActivitiesDetail> ActivityImplementations { get; set; }
    }
}