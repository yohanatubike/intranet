namespace IntranetPortal.Models.Planning
{
    public class ServiceOutput
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public IEnumerable<Target> Targets { get; set; }
    }
}
