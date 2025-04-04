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

        modelBuilder.Entity<MeterReading>()
            .HasIndex(m => new { m.AccountId, m.MeterReadingDateTime })
            .IsUnique();

        modelBuilder.Entity<MeterReading>()
            .HasOne(m=>m.Account)
            .WithMany()
            .HasForeignKey(m => m.AccountId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}