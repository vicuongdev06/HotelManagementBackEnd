using System;

namespace HotelManagementBackEnd.Models
{
    public class BookingRequest
    {
        public int RoomId { get; set; } 
        public int UserId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
    }
}
