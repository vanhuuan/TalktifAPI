using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TalktifAPI.Models
{
    [Table("Country")]
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(30)]
        public string Name { get; set; }
        [Column("createAt", TypeName = "datetime")]
        public DateTime? CreateAt { get; set; }

        [InverseProperty(nameof(City.Country))]
        public virtual ICollection<City> Cities { get; set; }
    }
}
