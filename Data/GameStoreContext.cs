using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data
{
    public class GameStoreContext : DbContext
    {
        public GameStoreContext(DbContextOptions<GameStoreContext> options) : base(options)
        {
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Instrument> Games{ get; set; }
        public DbSet<Genre> Genres{ get; set; }
        public DbSet<Order> Orders{ get; set; }
        public DbSet<Raiting> Raitings{ get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Screenshot> Screenshots{ get; set; }
        public DbSet<User> Users{ get; set; }
        public DbSet<Requirement> Requirements{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().ToTable("Comment").HasOne(x=>x.User).WithMany(x=>x.Comments).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Instrument>().ToTable("Game");
            modelBuilder.Entity<Genre>().ToTable("Genre");
            modelBuilder.Entity<Order>().ToTable("Order").HasOne(x=>x.User).WithMany(x=>x.Orders).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Raiting>().ToTable("Raiting").HasOne(x => x.User).WithMany(x => x.Raitings).OnDelete(DeleteBehavior.Restrict); ;
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Screenshot>().ToTable("Screenshot");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Requirement>().ToTable("Requirement");
        }

        public DbSet<GameStore.Models.News> News { get; set; }
    }

}
