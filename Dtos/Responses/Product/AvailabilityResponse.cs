using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace coreworking_space_booking_backend.Dtos.Responses.Product
{
    public class AvailabilityResponse
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("slots")] 
        public List<AvailabilitySlot> Slots { get; set; } = new List<AvailabilitySlot>();
    }
}
