using System;
using System.Diagnostics;

namespace Laobian.Share.Model.Job
{
    public class JobTaskLog
    {
        public int TaskId { get; set; }

        public int JobId { get; set; }

        public string Message { get; set; }

        public string Exception { get; set; }

        public string MachineName { get; set; } = Environment.MachineName;

        public int ProcessId { get; set; } = Process.GetCurrentProcess().Id;
    }
}