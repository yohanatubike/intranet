using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class ServiceSubCategory
    {
        public int ServiceSubCategoryId { get; set; }
        public string ServiceSubCategoryName { get; set; } = null!;
        public string? ServiceCategoryCode { get; set; }
    }
}
