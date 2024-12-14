using HotelManagementBackEnd.Interface;
using HotelManagementBackEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementBackEnd.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingController(IBookingRepository bookingRepository, IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
        }

        [HttpPost("CreateBooking")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRequest request)
        {
            var room = await _roomRepository.GetRoomByIdAsync(request.RoomId);
            if (room == null || room.IsBooked)
            {
                return BadRequest("Room is already booked or does not exist.");
            }

            var booking = new Booking
            {
                RoomId = request.RoomId,
                UserId = request.UserId,
                BookingDate = DateTime.UtcNow,
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate
            };

            var createdBooking = await _bookingRepository.CreateBookingAsync(booking);
            return CreatedAtAction(nameof(GetBookingByRoomId), new { roomId = createdBooking.RoomId }, createdBooking);
        }

        [HttpDelete("CancelBooking/{roomId}")]
        public async Task<IActionResult> CancelBooking(int roomId)
        {
            var success = await _bookingRepository.CancelBookingAsync(roomId);
            if (!success) return NotFound("Booking not found.");
            return NoContent();
        }

        [HttpGet("GetBookingByRoomId/{roomId}")]
        public async Task<IActionResult> GetBookingByRoomId(int roomId)
        {
            var booking = await _bookingRepository.GetBookingByRoomIdAsync(roomId);
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        [HttpGet("GetAllBookings")]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _bookingRepository.GetAllBookingsAsync();

            var bookingResponses = bookings.Select(b => new BookingResponse
            {
                BookingId = b.Id,
                RoomId = b.RoomId,
                RoomNumber = b.Room?.RoomNumber, 
                UserId = b.UserId,
                BookingDate = b.BookingDate,
                CheckInDate = b.CheckInDate,
                CheckOutDate = b.CheckOutDate
            }).ToList();

            return Ok(bookingResponses);
        }
    }
}
