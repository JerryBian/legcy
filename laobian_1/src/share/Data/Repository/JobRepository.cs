using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Laobian.Share.Model;
using Laobian.Share.Model.Job;
using MySql.Data.MySqlClient;

namespace Laobian.Share.Data.Repository
{
    public interface IJobRepository
    {
        Task UpdateJobTaskAsync();

        Task<List<JobTask>> GetNextTasksAsync();
    }

    public class JobRepository : IJobRepository
    {
        private readonly string _connectionString;

        public JobRepository(Config config)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            _connectionString = config.MySqlDatabaseConnection;
        }

        public async Task<List<JobTask>> GetNextTasksAsync()
        {
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                var result = await sqlConnection.QueryAsync<JobTask>("get_next_tasks", commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task UpdateJobTaskAsync()
        {
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                await sqlConnection.ExecuteAsync(
                    "update_job_task",
                    new { machine_name = Environment.MachineName, process_id = Process.GetCurrentProcess().Id },
                    commandType:CommandType.StoredProcedure);
            }
        }
    }
}
