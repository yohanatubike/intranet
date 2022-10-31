namespace IntranetPortal.Models.Planning
{
    public class Objective
    {
        public long Id { get; set; } = 0;
        public string Code { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public virtual IEnumerable<ServiceOutput>? ServiceOutputs { get; set; }
    }
}
