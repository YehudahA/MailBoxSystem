using Microsoft.EntityFrameworkCore;

namespace MailBoxSystem.Models;

#nullable disable

public class AppDbContext : DbContext
{
    #region ctor

    public AppDbContext(DbContextOptions options) : base(options) { }

    #endregion

    #region DbSet

    public DbSet<Box> Boxes { get; set; }
    public DbSet<LetterBox> LetterBoxes { get; set; }
    public DbSet<PackageBox> PackageBoxes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<PackageSender> PackageSenders { get; set; }

    public DbSet<LetterBoxStatus> LetterBoxStatuses { get; set; }
    public DbSet<Package> Packages { get; set; }

    #endregion

    #region configuration

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Box>().HasDiscriminator<string>("Discriminator")
            .HasValue<LetterBox>("L")
            .HasValue<PackageBox>("P");

        // design time data

        var user = new User()
        {
            Id = 1,
            Name = "Yehudah",
            PhoneNumber = 0535481815,
            EMail = "ye.altman@gmail.com",
            Password = "1234"
        };
        modelBuilder.Entity<User>().HasData(user);

        modelBuilder.Entity<LetterBox>().HasData(new LetterBox()
        {
            Id = 1,
            OwnerId = user.Id,
            Line1 = "אלטמן",
            Line2 = "קומה 1",
            LocalNumber = 1
        });

        var packBox = new PackageBox()
        {
            Id = 2,
            Size = PackBoxSize.Medium,
            LocalNumber = 2
        };
        modelBuilder.Entity<PackageBox>().HasData(packBox);

        modelBuilder.Entity<LetterBoxStatus>().HasData(new LetterBoxStatus()
        {
            Id = 1,
            BoxId = 1,
            DeliverTime = DateTime.Now.AddDays(-1),
        });

        modelBuilder.Entity<PackageSender>().HasData(new PackageSender()
        {
            Id = 1,
            Name = "Ali Express",
            IconName = "aliexpress.jpg"
        });

        modelBuilder.Entity<Package>().HasData(new Package()
        {
            Id = 1,
            BoxId = packBox.Id,
            Code = "R1234",
            SenderId = 1,
            RecieverName = "יהודה",
            RecieverPhone = 0535481815,
            DeliverTime = DateTime.Now.AddHours(-1),
        });
    }

    #endregion
}
