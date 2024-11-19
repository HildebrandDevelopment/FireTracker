using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HD.FireTracker.Data.Common.Models.FireTrackerDB;

namespace HD.FireTracker.DB.FireTrackerDB
{
    public class FireTrackerDbContext : DbContext
    {
        public FireTrackerDbContext()
        {
        }

        public FireTrackerDbContext(DbContextOptions<FireTrackerDbContext> options)
            : base(options)
        {
        }


        public DbSet<RecurringProcess> RecurringProcess { get; set; }

        public DbSet<RecurringProcessDetail> RecurringProcessDetail { get; set; }

        public DbSet<HangFireEvents> HangFireEvents { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
