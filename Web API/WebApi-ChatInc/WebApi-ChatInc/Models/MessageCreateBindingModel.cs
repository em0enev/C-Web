﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_ChatInc.Models
{
    public class MessageCreateBindingModel
    {
        public string Content { get; set; }

        public string User { get; set; }
    }
}
