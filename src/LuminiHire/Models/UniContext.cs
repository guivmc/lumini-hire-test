using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LuminiHire.Models
{
    public class UniContext : DbContext
    {
        public UniContext( DbContextOptions<UniContext> options ) : base( options )
        {
        }

        public DbSet<UniItem> Student { get; set; }

        protected override void OnModelCreating( ModelBuilder builder )
        {
            builder.Entity<UniItem>().HasKey( m => m.UNITID );
            base.OnModelCreating( builder );
        }
    }
}
