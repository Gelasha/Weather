using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Model;

namespace Weather
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options) { }

        public DbSet<SearchHistory> SearchHistories { get; set; }
    }
}
