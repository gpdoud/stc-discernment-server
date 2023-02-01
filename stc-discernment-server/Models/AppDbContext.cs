using System;
using Microsoft.EntityFrameworkCore;

namespace stc_discernment_server.Models {

    public class AppDbContext : DbContext {

        public DbSet<Parishioner> Parishioners { get; set; }
        public DbSet<Configuration> Configurations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<Parishioner>(e => {
                e.HasKey(p => p.Id);
                e.Property(p => p.Firstname)
                    .HasMaxLength(30)
                    .IsRequired();
                e.Property(p => p.Lastname)
                    .HasMaxLength(30)
                    .IsRequired();
                e.Property(p => p.Email)
                    .HasMaxLength(80);
                e.Property(p => p.Cellphone)
                    .HasMaxLength(20);
                e.Property(p => p.Homephone)
                    .HasMaxLength(20);
                e.Property(p => p.Ministry)
                    .HasMaxLength(30);
                e.Property(p => p.Year);
                e.Property(p => p.Reviewed);
                e.Property(p => p.Status)
                    .HasMaxLength(20);
                e.Property(p => p.SubmittedBy)
                    .HasMaxLength(30);
                e.Property(p => p.Active);
                e.Property(p => p.Created)
                    .HasColumnType("datetime");
                e.Property(p => p.Updated)
                    .HasColumnType("datetime");

                e.Property(p => p.CallerId);
                e.HasOne(p => p.Caller)
                    .WithMany()
                    .HasForeignKey(p => p.CallerId);
            });

            builder.Entity<Configuration>(e => {
                e.ToTable("Configurations");
                e.HasKey(x => x.KeyValue);
                e.Property(x => x.KeyValue).HasMaxLength(50).IsRequired();
                e.Property(x => x.DataValue).HasMaxLength(80);
                e.Property(x => x.Note).HasMaxLength(80);
                e.Property(x => x.System);
                e.Property(x => x.Active).IsRequired();
                e.Property(x => x.Created).IsRequired();
                e.Property(x => x.Updated);
                e.HasIndex(x => x.KeyValue).IsUnique();
            });
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}

