using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using MeterReadingCollector.Data.Entities;

namespace MeterReadingCollector.Data.Context;

public class MeterReadingContext(DbContextOptions<MeterReadingContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<MeterReading> MeterReadings { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MeterReading>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.HasOne<Account>()
                .WithMany()
                .HasForeignKey(m => m.AccountId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
