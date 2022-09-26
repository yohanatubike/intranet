using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class ServiceCategory
    {
        public long ServiceCategoryId { get; set; }
        public string ServiceName { get; set; } = null!;
        public string? ServiceCategoryCode { get; set; }
    }
}
