using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TalktifAPI.Models
{
    [Table("City")]
    public partial class City
    {
        public City()
        {
            Users = new HashSet<User>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("countryId")]
        public int CountryId { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Column("createdAt", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }

        [ForeignKey(nameof(CountryId))]
        [InverseProperty("Cities")]
        [JsonIgnore]
        public virtual Country Country { get; set; }
        [InverseProperty(nameof(User.City))]
        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}
