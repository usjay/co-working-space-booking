using coreworking_space_booking_backend.Dtos.Requests.UserSubcription;
using coreworking_space_booking_backend.Services.SubscriptionService;
using Microsoft.AspNetCore.Mvc;

namespace coreworking_space_booking_backend.Controllers
{
    [ApiController]
    [Route("api/subscription")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpPost("subscribe")]
        public IActionResult Subscribe([FromBody] SubscribeRequest request)
        {
            var response = _subscriptionService.SubscribeEmail(request);
            return StatusCode(response.StatusCode, response);
        }
    }

}
