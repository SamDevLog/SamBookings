using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IHotelsRepository hotelsRepo;
        private readonly IReservationsRepository reservationsRepository;
        private readonly DataContext ctx;

        public ReservationService(IHotelsRepository hotelsRepo, IReservationsRepository reservationsRepository, DataContext ctx)
        {
            this.hotelsRepo = hotelsRepo;
            this.reservationsRepository = reservationsRepository;
            this.ctx = ctx;
        }

        public async Task<Reservation> CancelReservationAsync(int id)
        {
            var reservation = await reservationsRepository.GetReservationByIdAsync(id);

            if(reservation is not null) ctx.Reservations.Remove(reservation);

            await ctx.SaveChangesAsync();

            return reservation;
        }

        public async Task<Reservation> MakeReservationAsync(Reservation reservation)
        {
            var hotel = await hotelsRepo.GetHotelByIdAsync(reservation.HotelId);

            if(hotel is null) return null;

            var room = hotel.Rooms.Where(r => r.Id == reservation.RoomId).FirstOrDefault();
            
            if(room is null) return null;

            bool isBusy = await ctx.Reservations.AnyAsync(dbRes => 
                (reservation.CheckInDate >= dbRes.CheckInDate && reservation.CheckInDate <= dbRes.CheckOutDate)
                && (reservation.CheckOutDate >= dbRes.CheckInDate && reservation.CheckOutDate <= dbRes.CheckOutDate)
            );

            if(isBusy) return null;

            if(room.NeedsRepair) return null;

            ctx.Rooms.Update(room);
            ctx.Reservations.Add(reservation);

            await ctx.SaveChangesAsync();

            return reservation;
        }
    }
}