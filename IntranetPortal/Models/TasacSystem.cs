using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IntranetPortal.Models
{
    [Table("TasacSystems")]
    public class TasacSystem
    {
        [Column("SystemId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int SystemId { get; set; }
        public string? SystemName { get; set; }
        public string? Description { get; set; }
        public string? SystemUrl { get; set; }
        public string? SystemType { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? DepartmentCode { get; set; }
    }
}
