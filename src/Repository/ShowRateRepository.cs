using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModels;
using Microsoft.EntityFrameworkCore;
using Repository.Abstractions;

namespace Repository
{
    public class ShowRateRepository : IShowRateRepository
    {
        private readonly ShowContext _context;

        public ShowRateRepository(ShowContext context)
        {
            _context = context;
        }

        public async Task<ShowRate> GetAsync(int showId, Guid userId)
        {
            return await _context.ShowRates
                .FirstOrDefaultAsync(x => x.ShowId == showId && x.UserId == userId);
        }

        public async Task<IEnumerable<ShowRate>> GetAsync(int showId)
        {
            return await _context.ShowRates
                .Where(x => x.ShowId == showId)
                .ToListAsync();
        }

        public async Task<ShowRate> PostAsync(ShowRate showRate)
        {
            await _context.ShowRates.AddAsync(showRate);
            await _context.SaveChangesAsync();
            return showRate;
        }

        public async Task<ShowRate> UpdateAsync(ShowRate showRate)
        {
            _context.ShowRates.Update(showRate);
            await _context.SaveChangesAsync();
            return showRate;
        }
    }
}
