using MySql.Data.MySqlClient;

namespace Laobian.Share.Model
{
    public class ColumnItem
    {
        public string Name { get; set; }

        public MySqlDbType DbType { get; set; }

        public object Value { get; set; }
    }
}