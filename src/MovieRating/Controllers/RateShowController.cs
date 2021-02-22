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
    public class RateShowController : ControllerBase
    {
        private readonly IShowService _showService;
        private readonly IShowRateService _showRateService;

        public RateShowController(IShowService showService, IShowRateService showRateService)
        {
            _showService = showService;
            _showRateService = showRateService;
        }

        [HttpPost]
        public async Task<IActionResult> RateShowAsync(RateShow rateShowWithoutId)
        {
            // SHOULDN'T BE DONE THIS WAY
            // SHOULD CONFIGURE CLAIMS ON IDENTITY TO RETRIEVE USER ID
            var userId = User.Claims.ToList()[5].Value;
            var rateShow = new RateShowWithUserId
            {
                Rate = rateShowWithoutId.Rate,
                ShowId = rateShowWithoutId.ShowId,
                UserId = new Guid(userId),
            };

            if (rateShow.Rate < 1 || rateShow.Rate > 5)
            {
                throw new ShowException($"{nameof(rateShow.Rate)} has to be between 1 and 5");
            }

            if (rateShow.ShowId <= 0)
            {
                throw new ShowException($"{nameof(rateShow.ShowId)} must be a positive number");
            }

            var show = await _showService.GetShowAsync(rateShow.ShowId);

            if (show == null)
            {
                throw new ShowException($"Show with Id {rateShow.ShowId} does not exist");
            }

            var showRate = await _showRateService.GetShowRateAsync(rateShow.ShowId, rateShow.UserId);

            if (showRate != null)
            {
                var updatedRatedShow = await _showRateService.UpdateRateAsync(rateShow);
                return Ok(updatedRatedShow);
            }

            var ratedShow = await _showRateService.RateShowAsync(rateShow);

            return Ok(ratedShow);
        }
    }
}
