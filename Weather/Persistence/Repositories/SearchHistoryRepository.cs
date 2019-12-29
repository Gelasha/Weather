using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Interfaces;
using Weather.Model;

namespace Weather.Repositories
{
    public class SearchHistoryRepository : ISearchHistoryRepository
    {
        private readonly WeatherDbContext _context;
        public SearchHistoryRepository(WeatherDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SearchHistory searchHistory)
        {
            await _context.SearchHistories.AddAsync(searchHistory);
            await _context.SaveChangesAsync();           
        }

        public async Task<IEnumerable<SearchHistory>> GetAllAsync()
        {
            return await _context.Set<SearchHistory>().ToListAsync();
        }
    }
}
