﻿using Sadra.Newsletter.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sadra.Newsletter.Domain.Entities
{
    [AuditableAttribute]
    public class NewsLetter
    {
        public NewsLetter()
        {
            this.CreatedDate = DateTime.Now;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; init; }

    }
}
