using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class BusinessCard
    {
        public string Pfnumber { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public string MobileNumber { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telephone { get; set; } = null!;
        public string? Fax { get; set; }
    }
}
