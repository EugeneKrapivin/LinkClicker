using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkClicker.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkClicker
{
    public class LinkClickerContext : DbContext
    {
        public DbSet<TinyLink> TinyLinks { get; set; }
        public DbSet<LinkOwner> LinkOwners { get; set; }

        public LinkClickerContext(DbContextOptions<LinkClickerContext> options) 
            : base(options)
        {
            
        }
    }
}
