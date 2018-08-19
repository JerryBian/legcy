using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laobian.Infrastuture.Entity
{
    public class EntityBase
    {
        [Key]
        [Column("id", Order = 0, TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("createTime", Order = int.MaxValue-2, TypeName = "datetime")]
        public DateTime CreateTime { get; set; }

        [Column("updateTime", Order = int.MaxValue -1, TypeName = "datetime")]
        public DateTime UpdateTime { get; set; }
    }
}
