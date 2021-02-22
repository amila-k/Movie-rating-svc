using ContractModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Abstractions
{
    /// <summary>
    /// An implementation would provide access to show logic.
    /// </summary>
    public interface IShowService
    {
        /// <summary>
        /// Get show by Id.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns><see cref="Show"/></returns>
        Task<Show> GetShowAsync(int id);

        /// <summary>
        /// Get shows by type and filtering/paging params.
        /// </summary>
        /// <param name="showType">The show type.</param>
        /// <param name="filteringParams">The filtering params.</param>
        /// <returns>List of <see cref="Show"/></returns>
        Task<IEnumerable<Show>> GetShowsAsync(string showType, ShowParameters filteringParams);

        /// <summary>
        /// Get shows by show type.
        /// </summary>
        /// <param name="showType">The show type.</param>
        /// <returns>List of <see cref="Show"/> with a particular type.</returns>
        Task<IEnumerable<Show>> GetShowsAsync(string showType);

        /// <summary>
        /// Get shows with user's ratings.
        /// </summary>
        /// <param name="showType">The show type.</param>
        /// <param name="userId">The user Id.</param>
        /// <returns>Shows with user's ratings.</returns>
        Task<IEnumerable<Show>> GetShowsWithUserRatingAsync(string showType, Guid userId);
    }
}
