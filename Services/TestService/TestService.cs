using coreworking_space_booking_backend.Data;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Helpers.Logger;
using Microsoft.EntityFrameworkCore;

namespace coreworking_space_booking_backend.Services.TestService
{
    public class TestService : ITestService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppLogger _appLogger;
        private const string LogSource = "TestService";

        public TestService(ApplicationDbContext context, IAppLogger appLogger)
        {
            _context = context;
            _appLogger = appLogger;
        }

        public BaseResponse<string> GetConnectionStatus()
        {
            try
            {
                _appLogger.LogMethodStart(LogSource, "GetConnectionStatus");
                _context.Database.OpenConnection();
                _context.Database.CloseConnection();

                string message = $"API is running and DB connection is healthy at {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC";
                return BaseResponse<string>.SuccessResponse(message);
            }
            catch (Exception ex)
            {
                string error = $"API is running but DB connection failed: {ex.Message}";
                return BaseResponse<string>.ErrorResponse(500, error);
            }
        }
    }
}
