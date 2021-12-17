
using System;
using Microsoft.EntityFrameworkCore;

namespace Test_API.Classes
{
       public class BlogContext : DbContext 
    {
       
        public DbSet<Blog> Blogs { get; set; }

        public BlogContext(DbContextOptions<BlogContext> options): base(options)
        {
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySQL($"server=localhost;port=3468;userid=root;password=khoa345;database=Blog");

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Blog>().HasData
                (
                new Blog { BlogId = 11, Url = "test.com", Rating = 4, IsDeleted = true },
                new Blog { BlogId = 12, Url = "dev.com", Rating = 5, IsDeleted = false },
                new Blog { BlogId = 13, Url = "learnAPI.com", Rating = 3, IsDeleted = false }
                );
            
            modelBuilder.Entity<Blog>()
                .Property(b => b.Url)
                .HasMaxLength(25) // maximum length
                .IsRequired();
            
           
        }

    }
}
