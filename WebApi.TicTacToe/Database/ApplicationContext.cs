using Microsoft.EntityFrameworkCore;
using WebApi.TicTacToe.Models;

namespace WebApi.TicTacToe.Database
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) :
            base(options) { }
        
        public DbSet<User> Users => Set<User>();
        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<RoomPlayers> RoomPlayers => Set<RoomPlayers>();
        public DbSet<Game> Games => Set<Game>();
        public DbSet<PlayersMakeMove> PlayersMakeMoves => Set<PlayersMakeMove>();
        public DbSet<ResultGamePlayer> ResultGamePlayers => Set<ResultGamePlayer>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasOne(u => u.Room)
                .WithOne(r => r.Host)
                .HasForeignKey<Room>(u => u.HostId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder
                .Entity<User>()
                .HasOne(u => u.RoomPlayer)
                .WithOne(p => p.Player)
                .HasForeignKey<RoomPlayers>(p => p.PlayerId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
