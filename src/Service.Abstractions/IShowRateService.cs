using ContractModels;
using System;
using System.Threading.Tasks;

namespace Service.Abstractions
{
    /// <summary>
    /// An implementation would provide access to ShowRate logic.
    /// </summary>
    public interface IShowRateService
    {
        /// <summary>
        /// Rate a show.
        /// </summary>
        /// <param name="rateShow">The rate show model.</param>
        /// <returns>Show that is rated.</returns>
        Task<Show> RateShowAsync(RateShow rateShow);

        /// <summary>
        /// Get user's rate for a particular show.
        /// </summary>
        /// <param name="showId">The showId.</param>
        /// <param name="userId">The userId.</param>
        /// <returns><see cref="RateShow"/> containing rate of a user for a particular show.</returns>
        Task<RateShow> GetShowRateAsync(int showId, Guid userId);

        /// <summary>
        /// Update rate of a show.
        /// </summary>
        /// <param name="rateShow">The show rate.</param>
        /// <returns>Updated show.</returns>
        Task<Show> UpdateRateAsync(RateShowWithUserId rateShow);
    }
}
