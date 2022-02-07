using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories
{
    public class RoomsRepository : IRoomsRepository
    {
        private readonly DataContext _ctx;
        public RoomsRepository(DataContext ctx)
        {
            _ctx = ctx;            
        }

        public async Task<List<Room>> GetAllRoomsAsync()
        {
            return await _ctx.Rooms.ToListAsync();
        }

        public async Task<Room> GetRoomByIdAndHotelIdAsync(int hotelId, int roomId)
        {
            return await _ctx.Rooms.SingleOrDefaultAsync(r => r.Id == roomId && r.HotelId == hotelId);
        }

        public async Task<Room> GetRoomByIdAsync(int id)
        {
            return await _ctx.Rooms.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Room>> GetRoomsByHotelIdAsync(int hotelId)
        {
            return await _ctx.Rooms.Where(r => r.HotelId == hotelId).ToListAsync();
        }
    }
}