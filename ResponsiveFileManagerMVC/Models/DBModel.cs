namespace ResponsiveFileManagerMVC.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBModel : DbContext
    {
        public DBModel()
            : base("name=DBModel")
        {
        }

        public virtual DbSet<ActionLog> ActionLog { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActionLog>()
                .Property(e => e.Operator)
                .IsUnicode(false);

            modelBuilder.Entity<ActionLog>()
                .Property(e => e.Refer)
                .IsUnicode(false);

            modelBuilder.Entity<ActionLog>()
                .Property(e => e.Destination)
                .IsUnicode(false);

            modelBuilder.Entity<ActionLog>()
                .Property(e => e.Method)
                .IsUnicode(false);

            modelBuilder.Entity<ActionLog>()
                .Property(e => e.IPAddress)
                .IsUnicode(false);
        }
    }
}
