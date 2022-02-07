using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Services.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IHotelsRepository hotelsRepo;
        private readonly DataContext ctx;

        public ReservationService(IHotelsRepository hotelsRepo, DataContext ctx)
        {
            this.hotelsRepo = hotelsRepo;
            this.ctx = ctx;
        }

        public async Task<Reservation> MakeReservationAsync(Reservation reservation)
        {
            // var reservation = new Reservation
            // {
            //     HotelId = hotelId,
            //     RoomId = roomId,
            //     Customer = customer,
            //     CheckInDate = checkIn,
            //     CheckOutDate = checkOut
            // };

            var hotel = await hotelsRepo.GetHotelByIdAsync(reservation.HotelId);

            var room = hotel.Rooms.Where(r => r.Id == reservation.RoomId).FirstOrDefault();

            if(room is null) return null;

            var roomBusyFrom = room.BusyFrom == null ? default(DateTime) : room.BusyFrom;
            var roomBusyTo = room.BusyTo == null ? default(DateTime) : room.BusyTo;

            var isBusy = reservation.CheckInDate > roomBusyFrom && reservation.CheckInDate < roomBusyTo;

            if(isBusy && room.NeedsRepair) return null;

            room.BusyFrom = reservation.CheckInDate;
            room.BusyTo = reservation.CheckOutDate;

            ctx.Rooms.Update(room);
            ctx.Reservations.Add(reservation);

            await ctx.SaveChangesAsync();

            return reservation;
        }
    }
}