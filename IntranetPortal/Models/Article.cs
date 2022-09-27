using System;
using System.Collections.Generic;

namespace IntranetPortal.Models
{
    public partial class Article
    {
        public long ArticleId { get; set; }
        public string Title { get; set; } = null!;
        public string? Url { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
        public string? Status { get; set; }
    }
}
