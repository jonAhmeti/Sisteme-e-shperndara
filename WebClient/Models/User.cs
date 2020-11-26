using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebClient.Models
{
    public partial class User
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public DateTime? LastLogin { get; set; }
        public int Id { get; set; }
        public string Token { get; set; }

        public virtual Role Role { get; set; }
    }
}
