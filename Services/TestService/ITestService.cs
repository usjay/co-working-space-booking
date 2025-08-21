using coreworking_space_booking_backend.Dtos.Responses;

namespace coreworking_space_booking_backend.Services.TestService
{
    public interface ITestService
    {
        BaseResponse<string> GetConnectionStatus();
    }
}
