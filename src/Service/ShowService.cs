using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ContractModels;
using Repository.Abstractions;
using Service.Abstractions;
using Service.Helpers.Interfaces;

namespace Service
{
    /// <summary>
    /// Implemenattion of Show service.
    /// </summary>
    public class ShowService : IShowService
    {
        private readonly IShowRepository _showRepository;
        private readonly IMapper _mapper;
        private readonly IImageHelper _imageHelper;
        private readonly ISearchEngineHelper _searchEngineHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShowService"/> class.
        /// </summary>
        /// <param name="showRepository">The show repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="imageHelper">The image helper.</param>
        /// <param name="searchEngineHelper">The search engine helper.</param>
        public ShowService(IShowRepository showRepository, IMapper mapper, IImageHelper imageHelper, ISearchEngineHelper searchEngineHelper)
        {
            _showRepository = showRepository;
            _mapper = mapper;
            _imageHelper = imageHelper;
            _searchEngineHelper = searchEngineHelper;
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<Show>> GetShowsAsync(string showType)
        {
            var shows = await _showRepository.GetShowsAsync(showType);
            var mappedShows = _mapper.Map<IEnumerable<Show>>(shows);
            AppendImages(mappedShows);

            return mappedShows;
        }

        ///<inheritdoc/>
        public async Task<Show> GetShowAsync(int id)
        {
            var show = await _showRepository.GetShowAsync(id);
            return _mapper.Map<Show>(show);
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<Show>> GetShowsAsync(string showType, ShowParameters pagingFilteringParams)
        {
            var expressions = _searchEngineHelper.GetExpressionBasedOnText(pagingFilteringParams.FilterText);
            
            var shows = await _showRepository.GetShowsAsync(
                showType,
                pagingFilteringParams.PageNumber,
                pagingFilteringParams.PageSize,
                pagingFilteringParams.FilterText,
                expressions);

            var mappedShows = _mapper.Map<IEnumerable<Show>>(shows);
            AppendImages(mappedShows);

            return mappedShows;
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<Show>> GetShowsWithUserRatingAsync(string showType, Guid userId)
        {
            var showsWithRatings = await _showRepository.GetShowsWithRatingsAsync(showType);

            foreach (var show in showsWithRatings)
            {
                show.ShowRates = show.ShowRates.Where(x => x.UserId == userId).ToList();
            }

            var mappedShows = _mapper.Map<IEnumerable<ShowWithRating>>(showsWithRatings);
            AppendImages(mappedShows);

            return mappedShows;
        }

        private void AppendImages(IEnumerable<Show> shows)
        {
            foreach (var show in shows)
            {
                show.Image = _imageHelper.GetImageByName($"show{show.Id}");
            }
        }
    }
}
