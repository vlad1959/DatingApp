using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options){}


        public DbSet<Value> Values { get; set;}

        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Like>()
                .HasKey(k => new {k.LikerId, k.LikeeId}); //define a key as a combination of both ids
        
            //fluent API
            builder.Entity<Like>()
             .HasOne(u => u.Likee)
             .WithMany(u => u.Likers) //from users table
             .HasForeignKey(u => u.LikeeId) //from Users table
             .OnDelete(DeleteBehavior.Restrict); //don't want to delete user when row from Like is deleted
     
            builder.Entity<Like>()
             .HasOne(u => u.Liker)
             .WithMany(u => u.Likees) //from users table
             .HasForeignKey(u => u.LikerId) //from Users table
             .OnDelete(DeleteBehavior.Restrict); //don't want to delete user when row from Like is deleted
        }
    }
}