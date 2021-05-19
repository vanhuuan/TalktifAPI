using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TalktifAPI.Models
{
    [Table("User")]
    [Index(nameof(Email), Name = "UQ__User__AB6E6164565CBAC3", IsUnique = true)]
    public partial class User
    {
        public User()
        {
            Reports = new HashSet<Report>();
            UserChatRooms = new HashSet<UserChatRoom>();
            UserRefreshTokens = new HashSet<UserRefreshToken>();
        }

public User(string Name,string Email,String Password,bool gender,String hobbies,int cityid)
        {
            Reports = new HashSet<Report>();
            UserChatRooms = new HashSet<UserChatRoom>();
            UserRefreshTokens = new HashSet<UserRefreshToken>();
            this.Name = Name;
            this.Email = Email;
            this.Password = Password;
            Gender = gender;
            Hobbies = hobbies;
            CityId = cityid;
            IsActive = true;
            ConfirmedEmail = true;
            IsAdmin = false;
            CreatedAt = DateTime.Now;
        }
        public User(string Name,string Email,String Password,bool gender,String hobbies,int cityid ,bool isAdmin)
        {
            Reports = new HashSet<Report>();
            UserChatRooms = new HashSet<UserChatRoom>();
            UserRefreshTokens = new HashSet<UserRefreshToken>();
            this.Name = Name;
            this.Email = Email;
            this.Password = Password;
            Gender = gender;
            Hobbies = hobbies;
            CityId = cityid;
            IsActive = true;
            ConfirmedEmail = true;
            IsAdmin = isAdmin;
            CreatedAt = DateTime.Now;
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [Column("password")]
        [StringLength(100)]
        public string Password { get; set; }
        [Column("gender")]
        public bool? Gender { get; set; }
        [Column("hobbies")]
        [StringLength(1000)]
        public string Hobbies { get; set; }
        [Column("cityId")]
        public int CityId { get; set; }
        [Column("isAdmin")]
        public bool? IsAdmin { get; set; }
        [Column("confirmedEmail")]
        public bool? ConfirmedEmail { get; set; }
        [Column("isActive")]
        public bool? IsActive { get; set; }
        [Column("createdAt", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }

        [ForeignKey(nameof(CityId))]
        [InverseProperty("Users")]
        public virtual City City { get; set; }
        [InverseProperty(nameof(Report.ReporterNavigation))]
        public virtual ICollection<Report> Reports { get; set; }
        [InverseProperty(nameof(UserChatRoom.UserNavigation))]
        public virtual ICollection<UserChatRoom> UserChatRooms { get; set; }
        [InverseProperty(nameof(UserRefreshToken.UserNavigation))]
        public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; }
    }
}
