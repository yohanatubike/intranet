using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntranetPortal.Models
{

    [Table("Documentations")]
    public class Documentation
    {
        [Column("DocumentationId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long DocumentationId { get; set; }
        [Column("Title")]
        public string? Title { get; set; }
        [Column("Description")]
        public string? Description { get; set; }
        [Column("Type")]
        public string? DocumentType { get; set; }
        [Column("DepartmentCode")]
        public string? DepartmentCode { get; set; }
        [Column("Url")]
        public string? Url { get; set; }
        [Column("status")]
        public string? Status { get; set; }
        [Column("CreatedBy")]
        public string? CreatedBy { get; set; }
        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; } 

    }
}
