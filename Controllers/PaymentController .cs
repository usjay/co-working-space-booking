using coreworking_space_booking_backend.Dtos.Requests.Payment;
using coreworking_space_booking_backend.Dtos.Responses.Payment;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Services.PaymentService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace coreworking_space_booking_backend.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("create")]
        public BaseResponse<string> Create(PaymentRequest request)
        {
            return _paymentService.CreatePayment(request);
        }

        [HttpPost("update")]
        public BaseResponse<string> Update(UpdatePaymentRequest request)
        {
            return _paymentService.UpdatePayment(request);
        }

        [HttpPost("delete")]
        public BaseResponse<string> Delete(int paymentId)
        {
            return _paymentService.DeletePayment(paymentId);
        }

        [HttpPost("get-all")]
        public BaseResponse<List<PaymentResponse>> GetAll()
        {
            return _paymentService.GetAllPayments();
        }

        [HttpPost("get-by-id")]
        public BaseResponse<PaymentResponse> GetById(GetPaymentByIdRequest request)
        {
            return _paymentService.GetPaymentById(request);
        }
    }
}
