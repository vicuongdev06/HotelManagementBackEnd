using HotelManagementBackEnd.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagementBackEnd.Interface
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllRoomsAsync();
        Task<Room> GetRoomByIdAsync(int id);
        Task<Room> CreateRoomAsync(Room room);
        Task<Room> UpdateRoomAsync(Room room);
        Task<bool> DeleteRoomAsync(int id);
        Task<IEnumerable<Room>> GetRoomsByStatusAsync(bool isAvailable);
        Task<Room> UpdateRoomStatusAsync(int id, bool isAvailable);
    }
}
