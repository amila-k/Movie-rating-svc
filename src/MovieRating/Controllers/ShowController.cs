using System;
using System.Linq;
using System.Threading.Tasks;
using ContractModels;
using Infrastructure.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;

namespace MovieRating.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShowController : ControllerBase
    {
        private readonly IShowService _showService;
        private readonly IShowTypeService _showTypeService;

        public ShowController(IShowService showService, IShowTypeService showTypeService)
        {
            _showService = showService;
            _showTypeService = showTypeService;
        }

        [HttpGet("{type}")]
        public async Task<IActionResult> GetShows(string type, [FromQuery] ShowParameters pagingFilteringParams)
        {
            await CheckIfShowTypeExistsAsync(type);

            if (pagingFilteringParams.PageNumber == 0 || pagingFilteringParams.PageSize == 0)
            {
                var allShows = await _showService.GetShowsAsync(type);
                return Ok(allShows);
            }

            var shows = await _showService.GetShowsAsync(type, pagingFilteringParams);
            return Ok(shows);
        }

        [HttpGet("user-rating/{type}")]
        public async Task<IActionResult> GetShows(string type)
        {
            // SHOULDN'T BE DONE THIS WAY
            // SHOULD CONFIGURE CLAIMS ON IDENTITY TO RETRIEVE USER ID
            var userId = User.Claims.ToList()[5].Value;
            await CheckIfShowTypeExistsAsync(type);

            var shows = await _showService.GetShowsWithUserRatingAsync(type, new Guid(userId));

            return Ok(shows);
        }

        private async Task CheckIfShowTypeExistsAsync(string type)
        {
            if (type == null)
            {
                throw new ShowException($"Show type cannot be null");
            }

            var showType = await _showTypeService.GetShowTypeByNameAsync(type);
            if (showType == null)
            {
                throw new ShowException($"Show with type '{type}' does not exist");
            }
        }
    }
}
