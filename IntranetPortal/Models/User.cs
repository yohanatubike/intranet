using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntranetPortal.Models
{
    public partial class User
    {
        public long UserId { get; set; }
        public string PFNumber { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public string DesignationCode { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? Email { get; set; }
        public string MobileNumber { get; set; } = null!;
        public string ReportingTo { get; set; } = null!;
        public string DutyStation { get; set; } = null!;
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string? Password { get; set; }

        [ForeignKey("DesignationCode")]
        public virtual Designation Designations  { get; set; }
       
    }
}

