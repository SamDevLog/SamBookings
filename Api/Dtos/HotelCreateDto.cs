namespace Api.Dtos
{
    public class HotelCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public int Stars { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}