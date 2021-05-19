using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TalktifAPI.Models
{
    [Table("User_RefreshToken")]
    public partial class UserRefreshToken
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("user")]
        public int User { get; set; }
        [Required]
        [Column("refreshToken")]
        [StringLength(1000)]
        public string RefreshToken { get; set; }
        [Required]
        [Column("device")]
        [StringLength(100)]
        public string Device { get; set; }
        [Column("createAt", TypeName = "datetime")]
        public DateTime? CreateAt { get; set; }

        [ForeignKey(nameof(User))]
        [InverseProperty("UserRefreshTokens")]
        public virtual User UserNavigation { get; set; }
    }
}
