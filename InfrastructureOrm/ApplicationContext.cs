using InfrastructureOrm.Configuration;
using InfrastructureOrm.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace InfrastructureOrm;

public class ApplicationContext : DbContext
{
    public DbSet<User> users { get; set; } = null!;
    
    public DbSet<Admin> admins { get; set; } = null!;
    public DbSet<Application> applications { get; set; } = null!;
    public ApplicationContext()
    {
        //Database.EnsureCreated();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ApplicationConfiguration());
        modelBuilder.Entity<Admin>().HasData( new Admin() {id = Guid.NewGuid(), login= "admin", password = "admin"});
        base.OnModelCreating(modelBuilder);
        
        // Addd the Postgres Extension for UUID generation
        //modelBuilder.HasPostgresExtension("uuid-ossp");
        
        //modelBuilder.SetDefaultValuesTableName();
        //base.OnModelCreating(modelBuilder);
        
        //modelBuilder.Entity<User>().Property(u => u.id).HasDefaultValue("newsequentialid()");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .SetBasePath(Directory.GetCurrentDirectory())
            .Build();
       //Console.WriteLine("Директория "+Directory.GetCurrentDirectory());
        optionsBuilder.UseNpgsql(config.GetConnectionString("DefaultConnection"));
    }
}