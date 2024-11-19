using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HD.FireTracker.DB.FireTrackerDB.DBFirst;

public partial class FireTrackerDbContext : DbContext
{
    public FireTrackerDbContext()
    {
    }

    public FireTrackerDbContext(DbContextOptions<FireTrackerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<RecurringProcess> RecurringProcess { get; set; }

    public virtual DbSet<RecurringProcessDetail> RecurringProcessDetail { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-ICRQEQT\\SQLEXPRESS;Database=FireTrackerDb;Integrated Security=SSPI;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
