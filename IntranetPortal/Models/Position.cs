using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class Position
    {
        public long PositionId { get; set; }
        public string PositionCode { get; set; } = null!;
        public string PositionName { get; set; } = null!;
        public string SectionCode { get; set; } = null!;
        public string DepartmentCode { get; set; } = null!;
    }
}
