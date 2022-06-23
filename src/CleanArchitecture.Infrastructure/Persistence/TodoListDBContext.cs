using System.Reflection;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class TodoListDBContext : DbContext, IApplicationDbContext
    {
        public TodoListDBContext()
        {

        }

        public TodoListDBContext(DbContextOptions<TodoListDBContext> options) : base(options)
        {

        }

        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<TodoList> TodoLists { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("todo");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     // modelBuilder.HasPostgresExtension("uuid-ossp");

        //     // modelBuilder.Entity<RelUserChallengePointsTran>(entity =>
        //     // {
        //     //     entity.ToTable("RelUserChallengePointsTran", "cp_tran");

        //     //     entity.HasKey(e => e.Id).HasName("RelUserChallengePointsTran_pkey");

        //     //     entity.Property(e => e.Id)
        //     //         .HasColumnName("Id")
        //     //         .UseIdentityAlwaysColumn();

        //     //     entity.Property(e => e.UserId).HasColumnName("UserId");

        //     //     entity.Property(e => e.Points).HasColumnName("Points");

        //     //     entity.Property(e => e.PointsType).HasColumnName("PointsType").HasColumnType("smallint");
        //     //     entity.Property(e => e.CDateTime)
        //     //         .HasColumnName("CDateTime")
        //     //         .HasColumnType("timestamp with time zone");

        //     //     entity.Property(e => e.Creator).HasColumnName("Creator");

        //     //     entity.Property(e => e.Remarks).HasColumnName("Remarks").HasColumnType("text");

        //     //     entity.Property(e => e.Status).HasColumnName("Status").HasColumnType("smallint");

        //     //     entity.Property(e => e.ModifiedBy).HasColumnName("ModifiedBy");

        //     //     entity.Property(e => e.ModifiedDt).HasColumnName("ModifiedDt")
        //     //         .HasColumnType("timestamp with time zone");
        //     // });

        //     // modelBuilder.Entity<RelUserSuperheroesScoreTran>(entity =>
        //     // {
        //     //     entity.ToTable("RelUserSuperheroesScoreTran", "cp_tran");

        //     //     entity.HasKey(e => e.Id).HasName("RelUserSuperheroesScoreTran_pkey");

        //     //     entity.Property(e => e.Id)
        //     //         .HasColumnName("Id")
        //     //         .UseIdentityAlwaysColumn();

        //     //     entity.Property(e => e.UserId).HasColumnName("UserId");

        //     //     entity.Property(e => e.Points).HasColumnName("Points");

        //     //     entity.Property(e => e.PointsType).HasColumnName("PointsType").HasColumnType("smallint");
        //     //     entity.Property(e => e.CDateTime)
        //     //         .HasColumnName("CDateTime")
        //     //         .HasColumnType("timestamp with time zone");

        //     //     entity.Property(e => e.Creator).HasColumnName("Creator");

        //     //     entity.Property(e => e.Remarks).HasColumnName("Remarks").HasColumnType("text");

        //     //     entity.Property(e => e.Status).HasColumnName("Status").HasColumnType("smallint");

        //     //     entity.Property(e => e.ModifiedBy).HasColumnName("ModifiedBy");

        //     //     entity.Property(e => e.ModifiedDt).HasColumnName("ModifiedDt")
        //     //         .HasColumnType("timestamp with time zone");
        //     // });



        // }
    }
}
