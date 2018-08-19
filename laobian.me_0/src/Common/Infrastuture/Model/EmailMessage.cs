using System;
using System.Collections.Generic;
using System.Text;

namespace Laobian.Infrastuture.Model
{
    public class EmailMessage
    {
        public string FromAddress { get; set; }

        public string FromName { get; set; }

        public string ToAddress { get; set; }

        public string ToName { get; set; }

        public string Subject { get; set; }

        public string HtmlContent { get; set; }
    }
}
