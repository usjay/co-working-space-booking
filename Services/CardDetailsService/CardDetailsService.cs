using coreworking_space_booking_backend.Data;
using coreworking_space_booking_backend.Dtos.Requests.CardDetails;
using coreworking_space_booking_backend.Dtos.Requests.Facility;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.CardDetails;
using coreworking_space_booking_backend.Helpers.Logger;
using coreworking_space_booking_backend.Models.CardDetails;
using coreworking_space_booking_backend.Models.Facility;

namespace coreworking_space_booking_backend.Services.CardDetailsService
{
    public class CardDetailsService : ICardDetailsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppLogger _appLogger;
        private readonly string _logSource;

        public CardDetailsService(ApplicationDbContext context, IAppLogger appLogger)
        {
            _context = context;
            _appLogger = appLogger;
            _logSource = GetType().Name;
        }

        public BaseResponse<string> CreateCard(CardCreateRequestDto request)
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(CreateCard), request);

                CardDetail card = new CardDetail
                {
                    UserId = request.UserId,
                    CardToken = request.CardToken,
                    CardHolderName = request.CardHolderName,
                    LastFourDigits = request.LastFourDigits,
                    ExpiryDate = request.ExpiryDate,
                    IsDeleted = false
                };

                _context.CardDetails.Add(card);
                _context.SaveChanges();

                _appLogger.LogMethodStop(_logSource, nameof(CreateCard));
                return BaseResponse<string>.CreateSuccessResponse();

            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(CreateCard), ex);
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal Server Error.");
            }
        }

        public BaseResponse<List<CardListResponseDto>> GetCardsByUser(CardDetailsRequestDto request)
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(GetCardsByUser), request);

                var cards = _context.CardDetails
                    .Where(c => c.UserId == request.UserId && !c.IsDeleted)
                    .ToList();

                if (cards == null || !cards.Any())
                {
                    return BaseResponse<List<CardListResponseDto>>.ErrorResponse(
                        StatusCodes.Status404NotFound,
                        "No active cards found for this user"
                    );
                }

                var responseDtos = cards.Select(card => new CardListResponseDto
                {
                    Id = card.Id,
                    UserId = card.UserId,
                    CardToken = card.CardToken,
                    CardHolderName = card.CardHolderName,
                    LastFourDigits = card.LastFourDigits,
                    ExpiryDate = card.ExpiryDate,
                }).ToList();

                _appLogger.LogMethodStop(_logSource, nameof(GetCardsByUser));
                return BaseResponse<List<CardListResponseDto>>.SuccessResponse(responseDtos, "Cards retrieved successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(GetCardsByUser), ex);
                return BaseResponse<List<CardListResponseDto>>.ErrorResponse(
                    StatusCodes.Status500InternalServerError,
                    "Internal error, refer to the internal server logs for more information"
                );
            }
        }

        public BaseResponse<string> UpdateCard(CardUpdateRequestDto request)
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(UpdateCard), request);

                var card = _context.CardDetails.FirstOrDefault(c => c.Id == request.CardId && !c.IsDeleted);
                if (card == null)
                {
                    return BaseResponse<string>.ErrorResponse(
                        StatusCodes.Status404NotFound,
                        "Card not found or has been deleted"
                    );
                }

                card.Id = request.CardId;
                card.UserId = request.UserId;
                card.CardToken = request.CardToken;
                card.CardHolderName = request.CardHolderName;
                card.LastFourDigits = request.LastFourDigits;
                card.ExpiryDate = request.ExpiryDate;

                _context.SaveChanges();

                _appLogger.LogMethodStop(_logSource, nameof(UpdateCard));
                return BaseResponse<string>.SuccessResponse("Card updated successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(UpdateCard), ex);
                return BaseResponse<string>.ErrorResponse(
                    StatusCodes.Status500InternalServerError,
                    "Internal Server Error."
                );
            }
        }

        public BaseResponse<string> DeleteCard(CardDeleteRequestDto request)
        {
            try
            {
                _appLogger.LogMethodStart(_logSource, nameof(DeleteCard), request);

                var card = _context.CardDetails.FirstOrDefault(c => c.Id == request.CardId);
                if (card == null)
                {
                    return BaseResponse<string>.ErrorResponse(
                        StatusCodes.Status404NotFound,
                        "Card not found"
                    );
                }

                //Inform payment gateway
                //bool gatewayDeleted = _paymentGatewayService.DeleteCard(card.UserId, card.CardToken);
                //if (!gatewayDeleted)
                //{
                //    return BaseResponse<string>.ErrorResponse(
                //        StatusCodes.Status502BadGateway,
                //        "Failed to delete card from payment provider"
                //    );
                //}


                card.IsDeleted = true;
                _context.SaveChanges();

                _appLogger.LogMethodStop(_logSource, nameof(DeleteCard));
                return BaseResponse<string>.SuccessResponse("Card deleted successfully");
            }
            catch (Exception ex)
            {
                _appLogger.LogError(_logSource, nameof(DeleteCard), ex);
                return BaseResponse<string>.ErrorResponse(
                    StatusCodes.Status500InternalServerError,
                    "Internal Server Error."
                );
            }
        }



    }
}
