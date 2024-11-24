using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace laboratory_work_10
{
    internal class TickerContext : DbContext
    {
        public virtual DbSet<Ticker> Tickers { get; set; } = null;
        public virtual DbSet<TodayCondition> Companies { get; set; } = null;
        public virtual DbSet<Price> Prices { get; set; } = null;

        public TickerContext() { }

        public TickerContext(DbContextOptions<TickerContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost,1443;Initial Catalog=TickerDataBase;User ID=sa;Password=yourStrong(!)Password");
            }
        }
    }
}
