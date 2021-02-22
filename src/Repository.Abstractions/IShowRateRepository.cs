using DomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Abstractions
{
    public interface IShowRateRepository
    {
        Task<ShowRate> PostAsync(ShowRate showRate);

        Task<ShowRate> GetAsync(int showId, Guid userId);

        Task<IEnumerable<ShowRate>> GetAsync(int showId);

        Task<ShowRate> UpdateAsync(ShowRate showRate);
    }
}
