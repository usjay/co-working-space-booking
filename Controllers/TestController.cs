using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Services.TestService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace coreworking_space_booking_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;
        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpPost]
        public IActionResult GetConnectionStatus()
        {
            BaseResponse<string> response = _testService.GetConnectionStatus();
            return StatusCode(response.StatusCode, response);
        }

    }
}
