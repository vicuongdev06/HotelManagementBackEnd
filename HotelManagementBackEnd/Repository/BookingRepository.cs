using HotelManagementBackEnd.Data;
using HotelManagementBackEnd.Interface;
using Microsoft.EntityFrameworkCore;
using System;

namespace HotelManagementBackEnd.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            var room = await _context.Rooms.FindAsync(booking.RoomId);
            if (room != null)
            {
                room.IsBooked = true;
                _context.Rooms.Update(room);
            }
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<bool> CancelBookingAsync(int roomId)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.RoomId == roomId);
            if (booking == null) return false;

            _context.Bookings.Remove(booking);
            var room = await _context.Rooms.FindAsync(roomId);
            if (room != null)
            {
                room.IsBooked = false;
                _context.Rooms.Update(room);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Booking> GetBookingByRoomIdAsync(int roomId)
        {
            return await _context.Bookings.FirstOrDefaultAsync(b => b.RoomId == roomId);
        }
        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            return await _context.Bookings
                .Include(b => b.Room)
                .ToListAsync();
        }

    }

}
