using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TalktifAPI.Models
{
    [Table("Chat_Room")]
    public partial class ChatRoom
    {
        public ChatRoom()
        {
            Messages = new HashSet<Message>();
            UserChatRooms = new HashSet<UserChatRoom>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("chatRoomName")]
        [StringLength(100)]
        public string ChatRoomName { get; set; }
        [Column("numOfMember")]
        public int NumOfMember { get; set; }
        [Column("createdAt", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }

        [InverseProperty(nameof(Message.ChatRoom))]
        public virtual ICollection<Message> Messages { get; set; }
        [InverseProperty(nameof(UserChatRoom.ChatRoom))]
        public virtual ICollection<UserChatRoom> UserChatRooms { get; set; }
    }
}
