using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class FrontEndSlider
    {
        public string Description { get; set; } = null!;
        public string? ImageFile { get; set; }
        public string PublishStatus { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public long SliderId { get; set; }
    }
}
