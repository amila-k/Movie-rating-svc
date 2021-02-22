using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ContractModels;
using DomainModels;
using Repository.Abstractions;
using Service.Abstractions;

namespace Service
{
    /// <summary>
    /// Implemenatation of ShowRate service.
    /// </summary>
    public class ShowRateService : IShowRateService
    {
        private readonly IShowRateRepository _showRateRepository;
        private readonly IShowRepository _showRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShowRateService"/> class.
        /// </summary>
        /// <param name="showRateRepository">The show rate repository.</param>
        /// <param name="showRepository">The show repository.</param>
        /// <param name="mapper">The mapper.</param>
        public ShowRateService(IShowRateRepository showRateRepository, IShowRepository showRepository, IMapper mapper)
        {
            _showRateRepository = showRateRepository;
            _showRepository = showRepository;
            _mapper = mapper;
        }

        ///<inheritdoc/>
        public async Task<RateShow> GetShowRateAsync(int showId, Guid userId)
        {
            var showRate = await _showRateRepository.GetAsync(showId, userId);
            return _mapper.Map<RateShow>(showRate);
        }

        ///<inheritdoc/>
        public async Task<ContractModels.Show> RateShowAsync(RateShow rateShow)
        {
            ShowRate rate = _mapper.Map<ShowRate>(rateShow);
            var showRate = await _showRateRepository.PostAsync(rate);

            var updatedShow = await UpdateAverageRateAsync(showRate.ShowId);
            return _mapper.Map<ContractModels.Show>(updatedShow);
        }

        ///<inheritdoc/>
        public async Task<ContractModels.Show> UpdateRateAsync(RateShowWithUserId rateShow)
        {
            var showRate = await _showRateRepository.GetAsync(rateShow.ShowId, rateShow.UserId);
            showRate.Rate = rateShow.Rate;
            await _showRateRepository.UpdateAsync(showRate);

            var updatedShow = await UpdateAverageRateAsync(showRate.ShowId);
            return _mapper.Map<ContractModels.Show>(updatedShow);
        }

        private async Task<DomainModels.Show> UpdateAverageRateAsync(int showId)
        {
            var showRates = await _showRateRepository.GetAsync(showId);

            var show = await _showRepository.GetShowAsync(showId);
            show.AverageRate = (double)showRates.Sum(x => x.Rate) / showRates.Count();
            await _showRepository.UpdateShowAsync(show);

            return show;
        }
    }
}
