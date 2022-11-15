using NuGet.Packaging.Signing;
using System;

namespace IntranetPortal.Models
{
    public class SupportIncharge
    {
       public long      SupportInchargeID { get; set; }
       public DateTime  AssignedDate { get; set; } 
        public string  InchargeName { get; set; }
        public string? AssignedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? UpdatedBy;
        public bool Status { get; set; } = true;
            }
}
