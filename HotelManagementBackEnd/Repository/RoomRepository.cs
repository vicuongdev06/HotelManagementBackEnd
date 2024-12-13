using HotelManagementBackEnd.Data;
using HotelManagementBackEnd.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagementBackEnd.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> GetRoomByIdAsync(int id)
        {
            return await _context.Rooms.FindAsync(id);
        }

        public async Task<Room> CreateRoomAsync(Room room)
        {
            room.CreatedAt = DateTime.UtcNow;
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<Room> UpdateRoomAsync(Room room)
        {
            var existingRoom = await _context.Rooms.FindAsync(room.Id);
            if (existingRoom == null) return null;

            existingRoom.RoomNumber = room.RoomNumber;
            existingRoom.Floor = room.Floor;
            existingRoom.Capacity = room.Capacity;
            existingRoom.PricePerNight = room.PricePerNight;
            existingRoom.Description = room.Description;
            existingRoom.Images = room.Images;
            existingRoom.IsAvailable = room.IsAvailable;
            existingRoom.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingRoom;
        }

        public async Task<bool> DeleteRoomAsync(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return false;

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<Room>> GetRoomsByStatusAsync(bool isAvailable)
        {
            return await _context.Rooms
                .Where(room => room.IsAvailable == isAvailable)
                .ToListAsync();
        }

        public async Task<Room>UpdateRoomStatusAsync(int id, bool isAvailable)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return null;
            }
            room.IsAvailable = isAvailable;
            room.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return room;
        }
    }
}
