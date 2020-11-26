using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiTest.Models
{
    public partial class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public DateTime? LastLogin { get; set; }
        public int Id { get; set; }
        public string Token { get; set; }

        public virtual Role Role { get; set; }
    }
}
