using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TalktifAPI.Models
{
    [Table("User_ChatRoom")]
    public partial class UserChatRoom
    {
        [Key]
        [Column("user")]
        public int User { get; set; }
        [Key]
        [Column("chatRoomId")]
        public int ChatRoomId { get; set; }
        [Required]
        [Column("nickName")]
        [StringLength(30)]
        public String NickName { get; set; }

        [ForeignKey(nameof(ChatRoomId))]
        [InverseProperty("UserChatRooms")]
        public virtual ChatRoom ChatRoom { get; set; }
        [ForeignKey(nameof(User))]
        [InverseProperty("UserChatRooms")]
        public virtual User UserNavigation { get; set; }
    }
}
