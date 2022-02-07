namespace Api.Dtos
{
    public class ReservationGetDto
    {
        public int Id { get; set; }
        public RoomGetDto Room { get; set; }
        public HotelGetDto Hotel { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Customer { get; set; } = string.Empty;
    }
}