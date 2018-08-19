using System.Threading.Tasks;
using Dapper;
using Laobian.Share.Model;
using Laobian.Share.Model.Job;
using MySql.Data.MySqlClient;

namespace Laobian.Share.Data.Repository
{
    public interface IJobTaskLogRepository
    {
        Task AddAsync(JobTaskLog log);
    }

    public class JobTaskLogRepository : IJobTaskLogRepository
    {
        private readonly string _connectionString;

        public JobTaskLogRepository(Config config)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            _connectionString = config.MySqlDatabaseConnection;
        }

        public async Task AddAsync(JobTaskLog log)
        {
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                await sqlConnection.ExecuteAsync(
                    "INSERT INTO job_task_log (task_id, job_id, message, exception,  machine_name, process_id) " +
                    "VALUES (?task_id, ?job_id, ?message, ?exception, ?machine_name, ?process_id)",
                    new
                    {
                        task_id = log.TaskId,
                        job_id = log.JobId,
                        message = log.Message,
                        exception = log.Exception,
                        machine_name = log.MachineName,
                        process_id = log.ProcessId
                    });
            }
        }
    }
}