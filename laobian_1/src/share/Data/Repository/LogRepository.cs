using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Laobian.Share.Model;
using Laobian.Share.Model.Log;
using MySql.Data.MySqlClient;

namespace Laobian.Share.Data.Repository
{
    public interface ILogRepository<T> where T : LogItem
    {
        Task AddAsync(List<T> logs);
    }

    public abstract class LogRepository<T> : ILogRepository<T> where T : LogItem
    {
        protected string ConnectionString { get; }

        protected LogRepository(Config config)
        {
            ConnectionString = config.MySqlDatabaseConnection;
        }

        public abstract Task AddAsync(List<T> logs);

        protected async Task AddAsync(string tableName, List<List<ColumnItem>> columnItems)
        {
            var columns = columnItems.First().Select(c => c.Name).ToList();
            var query = $"INSERT INTO {tableName}({string.Join(", ", columns)}) VALUES({string.Join(", ", columns.Select(c => $"?{c}"))})";
            var parameters = new List<dynamic>();

            foreach (var columnItem in columnItems)
            {
                dynamic values = new ExpandoObject();
                IDictionary<string, object> underlyingValues = values;

                foreach (var item in columnItem)
                {
                    underlyingValues.Add(item.Name, item.Value);
                }

                parameters.Add(values);
            }

            using (var mysqlConnection = new MySqlConnection(ConnectionString))
            {
                await mysqlConnection.ExecuteAsync(query, parameters);
            }
        }
    }
}
