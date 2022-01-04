using System;
using Microsoft.EntityFrameworkCore;

namespace stc_discernment_server.Models {

    public class AppDbContext : DbContext {

        public DbSet<Candidate> Candidates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<Candidate>(e => {
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
            });
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}

