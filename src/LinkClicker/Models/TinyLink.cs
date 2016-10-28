using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkClicker.Models
{
    public class TinyLink
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public bool? Public { get; set; }
        public bool? Active { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual LinkOwner Owner { get; set; }
    }
}
