using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Repository
{
    public class UniversityMapContext : DbContext
    {
        public DbSet<Path> Paths { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public UniversityMapContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Path>()
                .HasOne(p => p.FirstRoom)
                .WithMany(b => b.PathsWhenThisFirst)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Path>()
                .HasOne(p => p.SecondRoom)
                .WithMany(b => b.PathsWhenThisSecond)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
