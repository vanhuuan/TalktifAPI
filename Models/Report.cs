using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TalktifAPI.Models
{
    [Table("Report")]
    public partial class Report
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("reporter")]
        public int Reporter { get; set; }
        [Column("suspect")]
        public int Suspect { get; set; }
        [Required]
        [Column("reason")]
        [StringLength(200)]
        public string Reason { get; set; }
        [Column("note")]
        [StringLength(1000)]
        public string Note { get; set; }
        [Column("status")]
        public bool Status { get; set; }
        [Column("createdAt", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }

        [ForeignKey(nameof(Reporter))]
        [InverseProperty(nameof(User.Reports))]
        public virtual User ReporterNavigation { get; set; }
    }
}
