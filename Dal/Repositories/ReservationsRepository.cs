using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories
{
    public class ReservationsRepository : IReservationsRepository
    {
        private readonly DataContext ctx;

        public ReservationsRepository(DataContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<List<Reservation>> GetAllReservationsAsync()
        {
            return await ctx.Reservations
                .Include(h => h.Hotel)
                .Include(r => r.Room)
                .ToListAsync();
        }

        public async Task<List<Reservation>> GetHotelReservationsAsync(int hotelId)
        {
            return await ctx.Reservations
                .Include(r => r.Hotel)
                .Include(r => r.Room)
                .Where(r => r.HotelId == hotelId)
                .ToListAsync();
        }

        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
            return await ctx.Reservations
                .Include(h => h.Hotel)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Reservation>> GetRoomReservationsAsync(int hotelId, int roomId)
        {
            return await ctx.Reservations
                .Include(r => r.Hotel)
                .Include(r => r.Room)
                .Where(r => r.RoomId == roomId).ToListAsync();
        }
    }
}