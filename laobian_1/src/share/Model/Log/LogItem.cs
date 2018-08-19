using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Laobian.Share.Model.Log
{
    public abstract class LogItem
    {
        public virtual string Domain { get; set; }

        [Column(Name = "message")]
        public string Message { get; set; }

        [Column(Name = "create_time", DbType = MySqlDbType.DateTime)]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public Exception Exception { get; set; }

        [Column(Name = "exception")]
        public string ExceptionStr => Exception?.ToString();

        [Column(Name = "category")]
        public string Category { get; set; }

        [Column(Name = "machine_name")]
        public string MachineName { get; set; } = Environment.MachineName;

        [Column(Name = "level")]
        public string Level { get; set; }

        public List<ColumnItem> GetColumnItems()
        {
            var items = new List<ColumnItem>();
            foreach (var propertyInfo in GetType().GetProperties())
            {
                var attrs = propertyInfo.GetCustomAttributes(true);
                foreach (var attr in attrs)
                {
                    if (attr is ColumnAttribute columnAttr)
                    {
                        items.Add(new ColumnItem
                        {
                            DbType = columnAttr.DbType,
                            Name = columnAttr.Name,
                            Value = propertyInfo.GetValue(this)
                        });
                        break;
                    }
                }
            }

            return items;
        }
    }
}