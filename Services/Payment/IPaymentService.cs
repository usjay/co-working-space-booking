using coreworking_space_booking_backend.Dtos.Requests.Payment;
using coreworking_space_booking_backend.Dtos.Responses.Payment;
using coreworking_space_booking_backend.Dtos.Responses;
using System.Collections.Generic;

namespace coreworking_space_booking_backend.Services.PaymentService
{
    public interface IPaymentService
    {
        BaseResponse<string> CreatePayment(PaymentRequest request);
        BaseResponse<string> UpdatePayment(UpdatePaymentRequest request);
        BaseResponse<string> DeletePayment(int paymentId);
        BaseResponse<PaymentResponse> GetPaymentById(GetPaymentByIdRequest request);
        BaseResponse<List<PaymentResponse>> GetAllPayments();
    }
}
