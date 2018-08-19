using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Laobian.Shared.Interface.Options;
using Microsoft.Extensions.Options;

namespace Laobian.Shared
{
    public class SharedRepository : ISharedRepository
    {
        private readonly string _connectionString;

        public SharedRepository(IOptions<ConfigurationBase> config)
        {
            _connectionString = config.Value.DbConnectionString;
        }

        public async Task<IDictionary<string, string>> GetSettingsAsync()
        {
            var result = new Dictionary<string, string>();
            using(var connection = new SqlConnection(_connectionString))
            {
                var settings = await connection.QueryAsync("usp_get_settings", commandType: CommandType.StoredProcedure);
                foreach(var setting in settings)
                {
                    result.Add(setting.key, setting.value);
                }
            }

            return result;
        }
    }
}
