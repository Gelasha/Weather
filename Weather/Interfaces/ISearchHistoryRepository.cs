using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Model;

namespace Weather.Interfaces
{
    public interface ISearchHistoryRepository
    {
        Task<IEnumerable<SearchHistory>> GetAllAsync();
        Task AddAsync(SearchHistory searchHistory);
    }
}
