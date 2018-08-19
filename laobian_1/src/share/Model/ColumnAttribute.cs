using System;
using MySql.Data.MySqlClient;

namespace Laobian.Share.Model
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class ColumnAttribute : Attribute
    {
        public string Name { get; set; }

        public MySqlDbType DbType { get; set; } = MySqlDbType.String;
    }
}
