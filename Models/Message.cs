using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TalktifAPI.Models
{
    [Table("Message")]
    public partial class Message
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("sender")]
        public int Sender { get; set; }
        [Column("chatRoomId")]
        public int ChatRoomId { get; set; }
        [Required]
        [Column("content")]
        [StringLength(1000)]
        public string Content { get; set; }
        [Column("sentAt", TypeName = "datetime")]
        public DateTime? SentAt { get; set; }

        [ForeignKey(nameof(ChatRoomId))]
        [InverseProperty("Messages")]
        public virtual ChatRoom ChatRoom { get; set; }
    }
}
