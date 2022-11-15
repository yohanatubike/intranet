namespace IntranetPortal.Models
{
    public class AuditingDetail
    {
        public long AuditingDetailID { get; set; }
        public string DepartmentCode { get; set; } = null!;
        public string SectionCode { get; set; } = null!;
        public long QMSQueries  { get; set; } 
        public long InternalQueries { get; set; } 
        public long ExternalQueries  { get; set; }
        public long eGAQueries { get; set; }
        public long TRQueries  { get; set; }
        public long IMSASQueries { get; set; }
       
        public string RecordedBy{ get; set; } = null!;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate  { get; set; }
        public DateTime? CreatedDate { get; set; }

        
    }
}
