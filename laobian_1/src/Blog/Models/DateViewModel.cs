namespace Laobian.Blog.Models
{
    public class DateViewModel
    {
        public DateViewModel(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}
