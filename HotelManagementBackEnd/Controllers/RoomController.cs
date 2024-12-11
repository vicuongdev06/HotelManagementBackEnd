using HotelManagementBackEnd.Data;
using HotelManagementBackEnd.Interface;
using HotelManagementBackEnd.Models.Room;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagementBackEnd.Controllers
{
    [Route("api/rooms")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpGet("GetAllRooms")]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _roomRepository.GetAllRoomsAsync();
            return Ok(rooms);
        }

        [HttpGet("GetRoomById/{id}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var room = await _roomRepository.GetRoomByIdAsync(id);
            if (room == null) return NotFound();
            return Ok(room);
        }

        [HttpPost("CreateRoom")]
        public async Task<IActionResult> CreateRoom([FromBody] RoomRequest request)
        {
            var room = new Room
            {
                RoomNumber = request.RoomNumber,
                Floor = request.Floor,
                Capacity = request.Capacity,
                PricePerNight = request.PricePerNight,
                Description = request.Description,
                Images = string.Join(",", request.Images)
            };

            var createdRoom = await _roomRepository.CreateRoomAsync(room);
            return CreatedAtAction(nameof(GetRoomById), new { id = createdRoom.Id }, createdRoom);
        }

        [HttpPut("UpdateRoom/{id}")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] RoomRequest request)
        {
            var room = new Room
            {
                Id = id,
                RoomNumber = request.RoomNumber,
                Floor = request.Floor,
                Capacity = request.Capacity,
                PricePerNight = request.PricePerNight,
                Description = request.Description,
                Images = string.Join(",", request.Images)
            };

            var updatedRoom = await _roomRepository.UpdateRoomAsync(room);
            if (updatedRoom == null) return NotFound();
            return Ok(updatedRoom);
        }

        [HttpDelete("DeleteRoom/{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var deleted = await _roomRepository.DeleteRoomAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
