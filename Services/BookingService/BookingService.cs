using coreworking_space_booking_backend.Data;
using coreworking_space_booking_backend.Dtos.Requests.Booking;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Booking;
using coreworking_space_booking_backend.Helpers.Logger;
using coreworking_space_booking_backend.Models.Booking;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace coreworking_space_booking_backend.Services.BookingService
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppLogger _appLogger;
        private readonly string _logSource;

        public BookingService(ApplicationDbContext context, IAppLogger appLogger)
        {
            _context = context;
            _appLogger = appLogger;
            _logSource = GetType().Name;
        }

        public BaseResponse<string> CreateBooking(BookingCreateRequestDto request)
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(CreateBooking), request);

                Booking booking = new Booking
                {
                    BookingId = request.BookingId,
                    UserId = request.UserId,
                    ProductId = request.ProductId,
                    FacilityCode = request.FacilityCode,
                    LocationId = request.LocationId,
                    PaymentId = request.PaymentId,
                    FacilityId = request.FacilityId,
                    StartTime = request.StartTime,
                    EndTime = request.EndTime,

                    IsOnetimeChanged = false,
                    IsCansled = false
                };

                _context.Bookings.Add(booking);
                _context.SaveChanges();

                _appLogger.LogMethodStop(_logSource, nameof(CreateBooking));
                return BaseResponse<string>.CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(CreateBooking), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }

        public BaseResponse<BookingDetailsResponseDto> GetBookingById(BookingDetailsRequestDto request)
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(GetBookingById), request);

                Booking booking = _context.Bookings.FirstOrDefault(b => b.BookingId == request.BookingId && !b.IsCansled);

                if (booking == null)
                {
                    return BaseResponse<BookingDetailsResponseDto>.ErrorResponse(StatusCodes.Status404NotFound, "Bookings not found or has been canseled");
                }

                BookingDetailsResponseDto responseDto = new BookingDetailsResponseDto
                {
                    BookingId = booking.BookingId,
                    UserId = booking.UserId,
                    ProductId = booking.ProductId,
                    FacilityCode = booking.FacilityCode,
                    LocationId = booking.LocationId,
                    PaymentId = booking.PaymentId,
                    FacilityId = booking.FacilityId,
                    IsOnetimeChanged = booking.IsOnetimeChanged,
                };

                _appLogger.LogMethodStop(_logSource, nameof(GetBookingById));
                return BaseResponse<BookingDetailsResponseDto>.SuccessResponse(responseDto, "Bookings retrieved successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(GetBookingById), ex);
                return BaseResponse<BookingDetailsResponseDto>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal error, refer to the internal server logs for more information");
            }
        }

        public BaseResponse<List<BookingListResponseDto>> GetBookingList()
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(GetBookingList));

                List<Booking> bookings = _context.Bookings.Where(b => !b.IsCansled).ToList();

                List<BookingListResponseDto> bookingList = new List<BookingListResponseDto>();
                foreach (Booking b in bookings)
                {
                    BookingListResponseDto dto = new BookingListResponseDto
                    {
                        BookingId = b.BookingId,
                        UserId = b.UserId,
                        ProductId = b.ProductId,
                        FacilityCode = b.FacilityCode,
                        LocationId = b.LocationId,
                        PaymentId = b.PaymentId,
                        FacilityId = b.FacilityId,
                        IsOnetimeChanged = b.IsOnetimeChanged,
                    };
                    bookingList.Add(dto);
                }

                _appLogger.LogMethodStop(_logSource, nameof(GetBookingList));
                return BaseResponse<List<BookingListResponseDto>>.SuccessResponse(bookingList, "Bookings retrieved successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(GetBookingList), ex);
                return BaseResponse<List<BookingListResponseDto>>.ErrorResponse(StatusCodes.Status500InternalServerError, "Failed to retrieve bookings");
            }
        }

        public BaseResponse<string> UpdateBooking(BookingUpdateRequestDto request)
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(UpdateBooking), request);

                Booking booking = _context.Bookings.FirstOrDefault(b => b.BookingId == request.BookingId && !b.IsCansled);
                if (booking == null)
                {
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Bookings not found or has been deleted");
                }

                booking.BookingId = request.BookingId;
                booking.UserId = request.UserId;
                booking.ProductId = request.ProductId;
                booking.FacilityCode = request.FacilityCode;
                booking.LocationId = request.LocationId;
                booking.PaymentId = request.PaymentId;
                booking.FacilityId = request.FacilityId;
                booking.IsOnetimeChanged = request.IsOnetimeChanged;

                _context.SaveChanges();

                _appLogger.LogMethodStop(_logSource, nameof(UpdateBooking));
                return BaseResponse<string>.SuccessResponse("Bookings updated successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(UpdateBooking), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }

        public BaseResponse<string> CanselBooking(BookingCanselRequestDto request)
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(CanselBooking), request);

                Booking booking = _context.Bookings.FirstOrDefault(b => b.BookingId == request.BookingId);
                if (booking == null)
                {
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Bookings not found");
                }

                booking.IsCansled = true;
                _context.SaveChanges();

                _appLogger.LogMethodStop(_logSource, nameof(CanselBooking));
                return BaseResponse<string>.SuccessResponse("Bookings Canseled successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(CanselBooking), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }
    }
}
