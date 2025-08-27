using coreworking_space_booking_backend.Dtos.Requests.Payment;
using coreworking_space_booking_backend.Dtos.Responses.Payment;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Services.PaymentService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace coreworking_space_booking_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("create")]
        public BaseResponse<string> CreatePayment([FromBody] PaymentRequest request)
        {
            return _paymentService.CreatePayment(request);
        }

        [HttpPost("update")]
        public BaseResponse<string> UpdatePayment([FromBody] UpdatePaymentRequest request)
        {
            return _paymentService.UpdatePayment(request);
        }

        [HttpPost("delete")]
        public BaseResponse<string> DeletePayment([FromBody] int paymentId)
        {
            return _paymentService.DeletePayment(paymentId);
        }

        [HttpPost("get-by-id")]
        public BaseResponse<PaymentResponse> GetPaymentById([FromBody] GetPaymentByIdRequest request)
        {
            return _paymentService.GetPaymentById(request);
        }

        [HttpPost("get-all")]
        public BaseResponse<List<PaymentResponse>> GetAllPayments()
        {
            return _paymentService.GetAllPayments();
        }
    }
}
