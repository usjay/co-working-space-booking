using coreworking_space_booking_backend.Data;
using coreworking_space_booking_backend.Dtos.Requests.Product;
using coreworking_space_booking_backend.Dtos.Responses;
using coreworking_space_booking_backend.Dtos.Responses.Product;
using coreworking_space_booking_backend.Dtos.Responses.Pricing;
using coreworking_space_booking_backend.Dtos.Responses.Rating;
using coreworking_space_booking_backend.Helpers.Logger;
using coreworking_space_booking_backend.Helpers.ImageUpload;
using coreworking_space_booking_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

using coreworking_space_booking_backend.Models.coreworking_space_booking_backend.Models;

namespace coreworking_space_booking_backend.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppLogger _logger;
        private readonly IConfiguration _configuration;
        private readonly ImageUploadHelper _imageHelper;
        private const string LogSource = "ProductService";

        public ProductService(ApplicationDbContext context, IAppLogger logger, IConfiguration configuration, ImageUploadHelper imageHelper)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _imageHelper = imageHelper;
        }

        private string SaveImage(IFormFile file)
        {
            if (file == null) return null;
            return _imageHelper.SaveFile(file, "ProductImage");
        }

        private ProductResponse MapToResponse(Product product, DateTime? requestedDate = null)
        {
            List<Rating> ratingsFromDb = _context.Ratings
                .Where(r => r.ProductId == product.ProductId && !r.IsDeleted)
                .OrderByDescending(r => r.CreatedAt)
                .ToList();

            double averageRating = ratingsFromDb.Any() ? Math.Round(ratingsFromDb.Average(r => r.Value), 2) : 0;
            int totalReviews = ratingsFromDb.Count;

            List<RatingResponse> recentRatings = ratingsFromDb
                .Take(5)
                .Select(r => new RatingResponse
                {
                    Value = r.Value,
                    ReviewDescription = r.ReviewDescription
                })
                .ToList();

            List<PricingResponse> pricingList = _context.Pricings
                .Where(pr => pr.ProductId == product.ProductId)
                .Select(pr => new PricingResponse
                {
                    Hourly = pr.HourlyRate,
                    Daily = pr.DailyRate,
                    Monthly = pr.MonthlyRate,
                    Yearly = pr.YearlyRate
                })
                .ToList();

            List<string> defaultFacilities = _context.Facilities
                .Where(f => f.ProductId == product.ProductId && !f.IsDeleted && f.IsDefault)
                .Select(f => f.FacilityName)
                .ToList();

            List<string> additionalFacilities = _context.Facilities
                .Where(f => f.ProductId == product.ProductId && !f.IsDeleted && !f.IsDefault)
                .Select(f => f.FacilityName)
                .ToList();

            var location = _context.Locations
                .Where(l => l.Id == product.LocationId)
                .Select(l => new { l.LocationUrl, l.Address })
                .FirstOrDefault();

            List<string> productImages = _context.ImageStores
                .Where(img => img.ProductId == product.ProductId)
                .Select(img => img.ImageUrl)
                .ToList();

          
            List<AvailabilityResponse> availabilityList = new List<AvailabilityResponse>();

            string[] hours = (product.OperationHours ?? "09:00 - 18:00").Split('-');
            TimeSpan opStart = TimeSpan.Parse(hours[0].Trim());
            TimeSpan opEnd = TimeSpan.Parse(hours[1].Trim());

            List<DateTime> dates = requestedDate.HasValue
                ? new List<DateTime> { requestedDate.Value.Date }
                : new List<DateTime> { DateTime.UtcNow.Date, DateTime.UtcNow.Date.AddDays(1) };

            foreach (var date in dates)
            {
                var bookedSlots = _context.Bookings
                    .Where(b => b.ProductId == product.ProductId && !b.IsCansled && b.StartTime.Date == date)
                    .Select(b => new { b.StartTime, b.EndTime })
                    .OrderBy(b => b.StartTime)
                    .ToList();

                List<AvailabilitySlot> slots = new List<AvailabilitySlot>();
                TimeSpan currentStart = opStart;

                foreach (var booking in bookedSlots)
                {
                    TimeSpan bookedStart = booking.StartTime.TimeOfDay;
                    TimeSpan bookedEnd = booking.EndTime.TimeOfDay;

                    if (currentStart < bookedStart)
                    {
                        slots.Add(new AvailabilitySlot
                        {
                            StartTime = currentStart.ToString(@"hh\:mm"),
                            EndTime = bookedStart.ToString(@"hh\:mm")
                        });
                    }

                    currentStart = bookedEnd > currentStart ? bookedEnd : currentStart;
                }

                if (currentStart < opEnd)
                {
                    slots.Add(new AvailabilitySlot
                    {
                        StartTime = currentStart.ToString(@"hh\:mm"),
                        EndTime = opEnd.ToString(@"hh\:mm")
                    });
                }

                availabilityList.Add(new AvailabilityResponse
                {
                    Date = date,
                    Slots = slots
                });
            }

            return new ProductResponse
            {
                Id = product.ProductId,
                Name = product.Name ?? string.Empty,
                Type = product.Type ?? string.Empty,
                ProductDescription = product.Discription ?? string.Empty,
                OperationHours = product.OperationHours ?? string.Empty,
                Capacity = product.Capacity,
                AverageRating = averageRating,
                TotalReviews = totalReviews,
                Pricing = pricingList,
                DefaultFacilities = defaultFacilities,
                AdditionalFacilities = additionalFacilities,
                LocationUrl = location?.LocationUrl ?? string.Empty,
                Address = location?.Address ?? string.Empty,
                RecentRatings = recentRatings,
                Images = productImages,
                Availability = availabilityList
            };
        }

        
        public BaseResponse<string> CreateProduct(ProductRequest request)
        {
            try
            {
                Product product = new Product
                {
                    CompanyId = request.CompanyId,
                    Name = request.Name,
                    Type = request.Type,
                    OperationHours = request.OperationHours,
                    Capacity = request.Capacity,
                    LocationId = request.LocationId,
                    Discription = request.Discription,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Images = new List<ImageStore>()
                };

                if (request.Images != null && request.Images.Any())
                {
                    foreach (var file in request.Images)
                    {
                        string imagePath = SaveImage(file);
                        product.Images.Add(new ImageStore
                        {
                            ImageUrl = imagePath,
                            Description = "Product Image",
                            ImageType = "Product",
                            Product = product,
                            CompanyId = request.CompanyId,
                            LocationId = request.LocationId,
                            CreatedAt = DateTime.UtcNow
                        });
                    }
                }

                _context.Products.Add(product);
                _context.SaveChanges();

                return BaseResponse<string>.SuccessResponse("Product created successfully");
            }
            catch (Exception ex)
            {
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public BaseResponse<List<ProductResponse>> GetAllProducts()
        {
            try
            {
                _logger.LogMethodStart(LogSource, nameof(GetAllProducts));
                var products = _context.Products.Where(p => p.IsActive).ToList()
                    .Select(p => MapToResponse(p)).ToList();

                if (!products.Any())
                    return BaseResponse<List<ProductResponse>>.ErrorResponse(StatusCodes.Status404NotFound, "No products found");

                _logger.LogMethodStop(LogSource, nameof(GetAllProducts));
                return BaseResponse<List<ProductResponse>>.SuccessResponse(products, "Products retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(LogSource, nameof(GetAllProducts), ex);
                return BaseResponse<List<ProductResponse>>.ErrorResponse(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public BaseResponse<ProductResponse> GetProductById(GetProductByIdRequest request)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(p => p.ProductId == request.Id && p.IsActive);
                if (product == null)
                    return BaseResponse<ProductResponse>.ErrorResponse(StatusCodes.Status404NotFound, "Product not found");

                return BaseResponse<ProductResponse>.SuccessResponse(MapToResponse(product), "Product retrieved successfully");
            }
            catch (Exception ex)
            {
                return BaseResponse<ProductResponse>.ErrorResponse(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public BaseResponse<string> UpdateProduct(UpdateProductRequest request)
        {
            try
            {
                var product = _context.Products.Include(p => p.Images).FirstOrDefault(p => p.ProductId == request.Id && p.IsActive);
                if (product == null)
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Product not found");

                product.CompanyId = request.CompanyId;
                product.Name = request.Name;
                product.Type = request.Type;
                product.OperationHours = request.OperationHours;
                product.Capacity = request.Capacity;
                product.LocationId = request.LocationId;
                product.UpdatedAt = DateTime.UtcNow;

                if (request.Images != null && request.Images.Any())
                {
                    foreach (var file in request.Images)
                    {
                        string imagePath = SaveImage(file);
                        product.Images.Add(new ImageStore
                        {
                            ImageUrl = imagePath,
                            Description = "Product Image",
                            ImageType = "Product",
                            Product = product,
                            CompanyId = request.CompanyId,
                            LocationId = request.LocationId,
                            CreatedAt = DateTime.UtcNow
                        });
                    }
                }

                _context.SaveChanges();
                return BaseResponse<string>.SuccessResponse("Product updated successfully");
            }
            catch (Exception ex)
            {
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public BaseResponse<string> DeleteProduct(DeleteProductRequest request)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(p => p.ProductId == request.Id && p.IsActive);
                if (product == null)
                    return BaseResponse<string>.ErrorResponse(StatusCodes.Status404NotFound, "Product not found");

                product.IsActive = false;
                product.UpdatedAt = DateTime.UtcNow;
                _context.SaveChanges();

                return BaseResponse<string>.SuccessResponse("Product deleted successfully");
            }
            catch (Exception ex)
            {
                return BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        public BaseResponse<List<ProductResponse>> GetProductsByFilter(ProductFilterRequest request)
        {
            try
            {
                var query = _context.Products.Where(p => p.IsActive);

                // Filter by type
                if (!string.IsNullOrWhiteSpace(request.Type))
                    query = query.Where(p => p.Type.Contains(request.Type));

                // Filter by location
                if (request.LocationId.HasValue)
                    query = query.Where(p => p.LocationId == request.LocationId.Value);

                // Filter by capacity
                if (request.Capacity.HasValue)
                    query = query.Where(p => p.Capacity >= request.Capacity.Value);

                // Filter by operation hours
                if (!string.IsNullOrWhiteSpace(request.StartTime))
                    query = query.Where(p => p.OperationHours.Contains(request.StartTime));

                if (!string.IsNullOrWhiteSpace(request.EndTime))
                    query = query.Where(p => p.OperationHours.Contains(request.EndTime));

                var products = query.ToList();

                // Filter by date availability
                if (request.Date.HasValue)
                {
                    DateTime requestedDate = request.Date.Value.Date;
                    products = products
                        .Where(p => !_context.Bookings.Any(b =>
                            b.ProductId == p.ProductId &&
                            !b.IsCansled &&
                            b.StartTime.Date <= requestedDate &&
                            b.EndTime.Date >= requestedDate))
                        .ToList();
                }

                // Filter by min/max daily price
                if (request.MinDailyRate.HasValue || request.MaxDailyRate.HasValue)
                {
                    products = products
                        .Where(p => _context.Pricings
                            .Where(pr => pr.ProductId == p.ProductId)
                            .Any(pr =>
                                (!request.MinDailyRate.HasValue || pr.DailyRate >= request.MinDailyRate.Value) &&
                                (!request.MaxDailyRate.HasValue || pr.DailyRate <= request.MaxDailyRate.Value)
                            )
                        ).ToList();
                }

                // Filter by minimum rating
                if (request.MinRating.HasValue)
                {
                    products = products
                        .Where(p =>
                        {
                            var ratings = _context.Ratings
                                .Where(r => r.ProductId == p.ProductId && !r.IsDeleted)
                                .Select(r => r.Value)
                                .ToList();

                            return ratings.Any() && ratings.Average() >= request.MinRating.Value;
                        })
                        .ToList();
                }

                // Filter by facilities (if provided)
                if (request.Facilities != null && request.Facilities.Any())
                {
                    products = products
                        .Where(p =>
                        {
                            var facilities = _context.Facilities
                                .Where(f => f.ProductId == p.ProductId && !f.IsDeleted)
                                .Select(f => f.FacilityName)
                                .ToList();

                            return request.Facilities.All(f => facilities.Contains(f));
                        })
                        .ToList();
                }

                var productResponses = products.Select(p => MapToResponse(p, request.Date)).ToList();

                if (!productResponses.Any())
                    return BaseResponse<List<ProductResponse>>.ErrorResponse(StatusCodes.Status404NotFound, "No products found");

                return BaseResponse<List<ProductResponse>>.SuccessResponse(productResponses, "Products retrieved successfully");
            }
            catch (Exception ex)
            {
                return BaseResponse<List<ProductResponse>>.ErrorResponse(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



    }
}
