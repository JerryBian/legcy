using System;
using System.Linq;

namespace Laobian.Shared.Interface.Blog
{
    public class BlogTag
    {
        private string _name;

        public int Id { get; set; }

        public int PostId { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                //if (CheckName(value))
                //{
                //    throw new InvalidOperationException($"Invalid tag [{value}] set.");
                //}

                _name = value.ToLowerInvariant();
            }
        }

        public static bool CheckName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            return !name.Any(n => !char.IsLetterOrDigit(n) && n != '-' );
        }
    }
}
