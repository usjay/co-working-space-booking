using coreworking_space_booking_backend.Data;
using coreworking_space_booking_backend.Dtos.Requests.Payment;
using coreworking_space_booking_backend.Dtos.Responses.Payment;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Helpers.Logger;
using coreworking_space_booking_backend.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace coreworking_space_booking_backend.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppLogger _logger;
        private const string LogSource = "PaymentService";

        public PaymentService(ApplicationDbContext context, IAppLogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public BaseResponse<string> CreatePayment(PaymentRequest request)
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(CreatePayment), request);

                var payment = new Payment
                {
                    UserId = request.UserId,
                    Amount = request.Amount,
                    PaymentMethod = request.PaymentMethod,
                    ReferenceNumber = Guid.NewGuid().ToString(),
                    TransactionId = Guid.NewGuid().ToString(),
                    IsPaid = false
                };

                _context.Payments.Add(payment);
                _context.SaveChanges();

                _logger.LogMethodStop(LogSource, nameof(CreatePayment));
                return BaseResponse<string>.SuccessResponse("Payment created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(CreatePayment), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        public BaseResponse<string> UpdatePayment(UpdatePaymentRequest request)
        {
            try
            {
                var payment = _context.Payments.FirstOrDefault(p => p.PaymentId == request.PaymentId);
                if (payment == null)
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Payment not found");

                payment.IsPaid = request.IsPaid;
                _context.SaveChanges();

                return BaseResponse<string>.SuccessResponse("Payment updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(UpdatePayment), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        public BaseResponse<string> DeletePayment(int paymentId)
        {
            try
            {
                var payment = _context.Payments.FirstOrDefault(p => p.PaymentId == paymentId);
                if (payment == null)
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Payment not found");

                _context.Payments.Remove(payment);
                _context.SaveChanges();

                return BaseResponse<string>.SuccessResponse("Payment deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(DeletePayment), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        public BaseResponse<PaymentResponse> GetPaymentById(GetPaymentByIdRequest request)
        {
            var payment = _context.Payments.FirstOrDefault(p => p.PaymentId == request.PaymentId);
            if (payment == null)
                return BaseResponse<PaymentResponse>.ErrorResponse(StatusCodes.Status404NotFound, "Payment not found");

            var response = new PaymentResponse
            {
                PaymentId = payment.PaymentId,
                UserId = payment.UserId,
                ReferenceNumber = payment.ReferenceNumber,
                TransactionId = payment.TransactionId,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                IsPaid = payment.IsPaid
            };

            return BaseResponse<PaymentResponse>.SuccessResponse(response);
        }

        public BaseResponse<List<PaymentResponse>> GetAllPayments()
        {
            var payments = _context.Payments.Select(payment => new PaymentResponse
            {
                PaymentId = payment.PaymentId,
                UserId = payment.UserId,
                ReferenceNumber = payment.ReferenceNumber,
                TransactionId = payment.TransactionId,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                IsPaid = payment.IsPaid
            }).ToList();

            return BaseResponse<List<PaymentResponse>>.SuccessResponse(payments);
        }
    }
}
