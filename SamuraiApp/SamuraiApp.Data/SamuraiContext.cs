using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SamuraiApp.Domain;

namespace SamuraiApp.Data
{
    public class SamuraiContext : DbContext
    {
        //Use First if Running from Console App
        public SamuraiContext()
        {

        }
        //Use Second if running from API and change it to the following DbContextOptions<SamuraiContext>
        public SamuraiContext(DbContextOptions options) : base (options)
        {
            //Don't use while running tests
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }   

        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Clan> Clans { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<SamuraiBattleStat> SamuraiBattleStats { get; set; }


        // Comment out ILoggerFacotry and OnCOnfigure if loading from SamuraiAPI as they handle all of that when creating database

        public static readonly ILoggerFactory ConsoleLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddFilter((category, level) =>
                    category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information)
                .AddConsole();
        });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
               //.UseLoggerFactory(ConsoleLoggerFactory)  Comment these out When running tests
               //.EnableSensitiveDataLogging()
               .UseSqlServer("Data Source = (localdb)\\ProjectsV13; Initial Catalog = SamuraiTestData");
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiBattle>().HasKey(s => new { s.SamuraiId, s.BattleId });
            modelBuilder.Entity<Horse>().ToTable("Horses");
            modelBuilder.Entity<SamuraiBattleStat>().HasNoKey().ToView("SamuraiBattleStats");
        }

    }
}
