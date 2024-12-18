﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Room
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string RoomNumber { get; set; }

    [Required]
    public int Floor { get; set; }

    [Required]
    public int Capacity { get; set; }

    [Required]
    public decimal PricePerNight { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    public string Images { get; set; }

    public bool IsAvailable { get; set; } = true;
    public bool IsBooked { get; set; }    

    [Required]
    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
    [JsonIgnore]
    public ICollection<Booking> Bookings { get; set; }

}
