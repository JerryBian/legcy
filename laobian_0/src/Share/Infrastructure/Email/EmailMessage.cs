using System;
using Laobian.Share.Model;

namespace Laobian.Share.Infrastructure.Email
{
    public class EmailMessage
    {
        public DomainName EmailDomain { get; set; }

        public string Message { get; set; }

        public Exception Exception { get; set; }

        public string RequestHeader { get; set; }

        public string RequestIp { get; set; }
    }
}