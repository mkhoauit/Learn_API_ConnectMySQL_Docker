using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Practice_API.Classes
{
    public class AnimalContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodDistribution> FoodDistributions { get; set; }

        public AnimalContext(DbContextOptions<AnimalContext> options): base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySQL($"server=localhost;port=3210;userid=root;password=khoa333;database=Animal");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>().HasData
            (
                new Animal { AnimalId = 1, Name = "Tom", Type = "Dog", IsMale = true},
                new Animal { AnimalId = 2, Name = "Jerry", Type = "Mouse", IsMale = true},
                new Animal { AnimalId = 3, Name = "MiMi", Type = "Cat", IsMale = false}
                
            );
            modelBuilder.Entity<Food>().HasData
            (
                new Food {FoodId = 1,FoodName = "Meat",NumberofCans = 50},
                new Food {FoodId = 2,FoodName = "Fish",NumberofCans = 40}
            );
            modelBuilder.Entity<FoodDistribution>().HasData
            (
                new FoodDistribution{AnimalId = 3,FoodId = 2,Quantity = 30, IsEnough = true},
                new FoodDistribution{AnimalId = 1,FoodId = 1,Quantity = 60, IsEnough = false}
            );
            
            modelBuilder.Entity<FoodDistribution>()
                .HasKey(t => new { t.AnimalId, t.FoodId });

            modelBuilder.Entity<FoodDistribution>()
                .HasOne(pt => pt.Animal)
                .WithMany(p => p.FoodDistributions)
                .HasForeignKey(pt => pt.AnimalId);

            modelBuilder.Entity<FoodDistribution>()
                .HasOne(pt => pt.Food)
                .WithMany(t => t.FoodDistributions)
                .HasForeignKey(pt => pt.FoodId);
            
        }
    
    }
}