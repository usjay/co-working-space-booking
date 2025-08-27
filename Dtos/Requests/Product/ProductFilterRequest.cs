using System;
using System.Collections.Generic;

namespace coreworking_space_booking_backend.Dtos.Requests.Product
{
    public class ProductFilterRequest
    {
        public string Type { get; set; }
        public int? LocationId { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime? Date { get; set; }
        public int? Capacity { get; set; }

        public decimal? MinDailyRate { get; set; }
        public decimal? MaxDailyRate { get; set; }

        public double? MinRating { get; set; }  // Only declare once

        public List<string> Facilities { get; set; }

        //public string SortBy { get; set; } // optional
    }
}
