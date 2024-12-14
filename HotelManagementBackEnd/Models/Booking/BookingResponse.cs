using System;

namespace HotelManagementBackEnd.Models
{
    public class BookingResponse
    {
        public int BookingId { get; set; }
        public int RoomId { get; set; }
        public string RoomNumber { get; set; } // Thông tin số phòng
        public int UserId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
    }
}
