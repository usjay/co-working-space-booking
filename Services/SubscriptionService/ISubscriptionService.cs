using coreworking_space_booking_backend.Dtos.Requests;
using coreworking_space_booking_backend.Dtos.Requests.UserSubcription;
using coreworking_space_booking_backend.Dtos.Responses;
using System.Collections.Generic;

namespace coreworking_space_booking_backend.Services.SubscriptionService
{
    public interface ISubscriptionService
    {
        BaseResponse<string> SubscribeEmail(SubscribeRequest request);
        BaseResponse<List<string>> GetAllSubscribers();
        BaseResponse<string> UnsubscribeEmail(SubscribeRequest request);
    }
}
