using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntranetPortal.Models
{
    public partial class ActivitiesComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long ActivitiesCommentsId { get; set; }
        public string Comment { get; set; } = null!;
        public long ActivityId { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
    }
}
