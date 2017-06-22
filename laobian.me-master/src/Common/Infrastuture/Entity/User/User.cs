using Laobian.Infrastuture.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Laobian.Infrasture.Entity.User
{
    [Table("user")]
    public class User : EntityBase
    {
        [Column("userName")]
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [Column("fullName")]
        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("avatar")]
        public string Avatar { get; set; }

        [Column("role")]
        public string Role { get; set; }

        [Column("email")]
        public string Email { get; set; }

        public List<UserPermission> UserPermission { get; set; }
    }
}
