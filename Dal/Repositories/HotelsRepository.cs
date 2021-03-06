using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories
{
    public class HotelsRepository : IHotelsRepository
    {
        private readonly DataContext _ctx;
        private readonly IRoomsRepository roomsRepository;

        public HotelsRepository(DataContext ctx, IRoomsRepository roomsRepository)
        {
            _ctx = ctx;
            this.roomsRepository = roomsRepository;
        }
        public async Task<Hotel> CreateHotelAsync(Hotel hotel)
        {
            _ctx.Hotels.Add(hotel);
            await _ctx.SaveChangesAsync();
            return hotel;
        }

        public async Task<Room> CreateHotelRoomAsync(int hotelId, Room room)
        {
            var hotel = await _ctx.Hotels.Include(h => h.Rooms).FirstOrDefaultAsync(h => h.Id == hotelId);
            
            hotel.Rooms.Add(room);
            await _ctx.SaveChangesAsync();
            return room; 
        }

        public async Task<Hotel> DeleteHotelAsync(int id)
        {
            var hotel = await _ctx.Hotels.FirstOrDefaultAsync(h => h.Id == id);

            if(hotel is null) return null;

            _ctx.Hotels.Remove(hotel);
            await _ctx.SaveChangesAsync();

            return hotel;
        }

        public async Task<Room> DeleteHotelRoomAsync(int hotelId, int roomId)
        {
            var room = await roomsRepository.GetRoomByIdAndHotelIdAsync(hotelId, roomId);
            

            if(room is null) return null;

            _ctx.Rooms.Remove(room);
            await _ctx.SaveChangesAsync();

            return room;
        }

        public async Task<List<Hotel>> GetAllHotelsAsync()
        {
            return await _ctx.Hotels.ToListAsync();

        }

        public async Task<Hotel> GetHotelByIdAsync(int id)
        {
            var hotel = await _ctx.Hotels
                .Include(h => h.Rooms)
                .FirstOrDefaultAsync(h => h.Id == id);
            
            if(hotel is null) return null;

            return hotel;

        }

        public async Task<Hotel> UpdateHotelAsync(Hotel updatedHotel)
        {
            _ctx.Hotels.Update(updatedHotel);
            await _ctx.SaveChangesAsync();

            return updatedHotel;
        }

        public async Task<Room> UpdateHotelRoomAsync(int hotelId, Room updatedRoom)
        {
            _ctx.Rooms.Update(updatedRoom);
            await _ctx.SaveChangesAsync();

            return updatedRoom;
        }
    }
}