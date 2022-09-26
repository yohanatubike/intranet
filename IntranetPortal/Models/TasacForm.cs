using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntranetPortal.Models
{
    [Table("TasacForms")]
    public class TasacForm
    {
        [Column("FormId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int FormId { get; set; }
        public string FormName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? FileUrl { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string DepartmentCode { get; set; } = null!;
    }
}
