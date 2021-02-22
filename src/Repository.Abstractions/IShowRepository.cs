using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Abstractions
{
    public interface IShowRepository
    {
        Task<IEnumerable<Show>> GetShowsAsync(string showType);
        Task<Show> GetShowAsync(int id);
        Task<IEnumerable<Show>> GetShowsAsync(string showType, int pageNumber, int pageSize, string filteringText, List<Expression<Func<Show, bool>>> filters);
        Task<IEnumerable<Show>> GetShowsWithRatingsAsync(string showType);
        Task<Show> UpdateShowAsync(Show show);
    }
}
