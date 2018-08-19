namespace Laobian.Share.Model.Job
{
    public class JobDefinition
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ClassName { get; set; }

        public bool IsActive { get; set; }

        public int Priority { get; set; }

        public JobType JobType { get; set; }
    }
}
