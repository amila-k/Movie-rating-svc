using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DomainModels;
using Microsoft.EntityFrameworkCore;
using Repository.Abstractions;

namespace Repository
{
    public class ShowRepository : IShowRepository
    {
        private readonly ShowContext _context;
        
        public ShowRepository(ShowContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Show>> GetShowsAsync(string showType)
        {
            return await _context.Shows
                .Include(x => x.ShowType)
                .Where(x => x.ShowType.Code.ToLower() == showType.ToLower())
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Show> GetShowAsync(int id)
        {
            return await _context.Shows
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Show> UpdateShowAsync(Show show)
        {
            _context.Shows.Update(show);
            await _context.SaveChangesAsync();
            return show;
        }

        public async Task<IEnumerable<Show>> GetShowsAsync(string showType, int pageNumber, int pageSize, string filteringText, List<Expression<Func<Show, bool>>> filters)
        {
            var query = _context.Shows
                .Include(x => x.ShowType)
                .Where(x => x.ShowType.Name.ToLower() == showType.ToLower() || x.ShowType.Code.ToLower() == showType.ToLower())
                .AsNoTracking();

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }

            if (filters != null && filters.Count == 0)
            {
                query = query.Where(x => filteringText == null || x.Title.ToLower().Contains(filteringText.ToLower()) || x.Description.ToLower().Contains(filteringText.ToLower()));
            }

            query = query.OrderByDescending(x => x.AverageRate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Show>> GetShowsWithRatingsAsync(string showType)
        {
            return await _context.Shows
                .Include(x => x.ShowRates)
                .Where(x => x.ShowType.Name.ToLower() == showType.ToLower() || x.ShowType.Code.ToLower() == showType.ToLower())
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
