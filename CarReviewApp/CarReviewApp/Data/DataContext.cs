using CarReviewApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace CarReviewApp.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext>options):base (options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarCategory> CarCategories { get; set; }
        public DbSet<CarOwner> CarOwners { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarCategory>()
                .HasKey(ca => new { ca.CarId, ca.CategoryId });

            modelBuilder.Entity<CarCategory>()
                .HasOne(c => c.Car)
                .WithMany(ca => ca.CarCategories)
                .HasForeignKey(a => a.CarId);

            modelBuilder.Entity<CarCategory>()
                .HasOne(c => c.Category)
                .WithMany(ca => ca.CarCategories)
                .HasForeignKey(a => a.CategoryId);


            modelBuilder.Entity<CarOwner>()
                .HasKey(ca => new { ca.CarId, ca.OwnerId });

            modelBuilder.Entity<CarOwner>()
                .HasOne(c => c.Owner)
                .WithMany(ca => ca.CarOwners)
                .HasForeignKey(a => a.OwnerId);

            modelBuilder.Entity<CarOwner>()
                .HasOne(c => c.Car)
                .WithMany(ca => ca.CarOwners)
                .HasForeignKey(a => a.CarId);
                
        }










    }
}
