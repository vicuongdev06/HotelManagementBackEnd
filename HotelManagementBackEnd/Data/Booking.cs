using System.Text.Json.Serialization;

public class Booking
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public int UserId { get; set; }
    public DateTime BookingDate { get; set; }
    public DateTime? CheckInDate { get; set; }
    public DateTime? CheckOutDate { get; set; }

    [JsonIgnore]  
    public Room Room { get; set; }
}
