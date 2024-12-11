using System;
using System.Collections.Generic;

namespace HotelManagementBackEnd.Models.Room
{
    public class RoomRequest
    {
        public string RoomNumber { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; } = new();
    }
}
