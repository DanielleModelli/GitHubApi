using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TestePratico.Models
{
    public partial class GitHubContext : DbContext
    {
        public GitHubContext()
        {
        }

        public GitHubContext(DbContextOptions<GitHubContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Repositorios> Repositorios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-QLSHROO\\MSSQLSERVER01;Database=GitHub;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Repositorios>(entity =>
            {
                entity.Property(e => e.Descricao)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasColumnName("URL")
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });
        }
    }
}
