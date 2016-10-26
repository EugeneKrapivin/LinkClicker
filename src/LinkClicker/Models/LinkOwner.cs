using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkClicker.Models
{
    public class LinkOwner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime LastLogin { get; set; }
        public bool Active { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual IEnumerable<TinyLink> TinyLinks { get; set; }
    }
}
