namespace HotelManagementBackEnd.Interface
{
    public interface IBookingRepository
    {
        Task<Booking> CreateBookingAsync(Booking booking);
        Task<bool> CancelBookingAsync(int roomId);
        Task<Booking> GetBookingByRoomIdAsync(int roomId);
        Task<List<Booking>> GetAllBookingsAsync();
    }

}
