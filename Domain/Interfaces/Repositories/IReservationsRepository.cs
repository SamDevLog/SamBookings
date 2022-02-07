using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface IReservationsRepository
    {
        Task<List<Reservation>> GetAllReservationsAsync();
        Task<Reservation> GetReservationByIdAsync(int id);
        Task<List<Reservation>> GetRoomReservationsAsync(int hotelId, int roomId);
        Task<List<Reservation>> GetHotelReservationsAsync(int hotelId);
    }
}