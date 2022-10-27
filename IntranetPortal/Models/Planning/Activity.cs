namespace IntranetPortal.Models.Planning
{
    public class Activity
    {
        public int Id { get; set; } = 0;
        public string Code { get; set; }
        public string Description { get; set; }
        public double AnnualBudget { get; set; }
        public Section Section { get; set; }
        public IEnumerable<ActivitiesDetail> ActivityImplementations { get; set; }
    }
}