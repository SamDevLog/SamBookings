using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface IRoomsRepository
    {
        Task<List<Room>> GetAllRoomsAsync();
        Task<List<Room>> GetRoomsByHotelIdAsync(int hotelId);
        Task<Room> GetRoomByIdAsync(int id);
        Task<Room> GetRoomByIdAndHotelIdAsync(int hotelId, int roomId);
    }
}