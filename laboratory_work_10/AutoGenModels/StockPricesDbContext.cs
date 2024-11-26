using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace laboratory_work_10;

public partial class StockPricesDbContext : DbContext
{
    public StockPricesDbContext()
    {
    }

    public StockPricesDbContext(DbContextOptions<StockPricesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Price> Prices { get; set; }

    public virtual DbSet<Ticker> Tickers { get; set; }

    public virtual DbSet<TodaysCondition> TodaysConditions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=StockPrices;User ID=sa;Password=HelloWorld10;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Price>(entity =>
        {
            entity.HasOne(d => d.Ticker).WithMany(p => p.Prices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prices_Tickers");
        });

        modelBuilder.Entity<TodaysCondition>(entity =>
        {
            entity.HasOne(d => d.Ticker).WithMany(p => p.TodaysConditions).HasConstraintName("FK_TodaysConditions_Tickers");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
