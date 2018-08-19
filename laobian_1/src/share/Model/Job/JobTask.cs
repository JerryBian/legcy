using System;

namespace Laobian.Share.Model.Job
{
    public class JobTask
    {
        public int JobId { get; set; }

        public int TaskId { get; set; }

        public DateTime RunAt { get; set; }

        public string ClassName { get; set; }

        public string Name { get; set; }

        public int Priority { get; set; }
    }
}
