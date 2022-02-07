using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces.Services
{
    public interface IReservationService
    {
        Task<Reservation> MakeReservationAsync(Reservation reservation);
        // Task<List<Reservation>> GetAllReservationsAsync();
        // Task<Reservation> GetReservationByIdAsync(int id);
        // Task<List<Reservation>> GetRoomReservationsAsync(int hotelId, int roomId);
        Task<Reservation> CancelReservationAsync(int id);
    }
}