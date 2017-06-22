using Laobian.Infrastuture.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laobian.Infrasture.Entity.User
{
    [Table("userPermission")]
    public class UserPermission : EntityBase
    {
        [Column("userId")]
        public int UserId { get; set; }

        [Column("permission")]
        public string Permission { get; set; }

        [Column("validFrom")]
        public DateTime ValidFrom { get; set; }

        [Column("validTo")]
        public DateTime ValidTo { get; set; }

        public User User { get; set; }
    }
}
