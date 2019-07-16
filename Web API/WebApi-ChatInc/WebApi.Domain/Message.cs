using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Domain
{
    public class Message
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string User { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
