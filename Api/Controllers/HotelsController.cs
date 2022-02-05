using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using AutoMapper;
using Dal;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : Controller
    {
        private readonly DataContext ctx;
        private readonly IMapper mapper;

        public HotelsController(DataContext ctx, IMapper mapper)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels(){
            
            var hotels = await ctx.Hotels.ToListAsync();

            var hotelsGet = mapper.Map<List<HotelGetDto>>(hotels);

            return Ok(hotelsGet);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetHotelById(int id){
            var hotel = await ctx.Hotels.FirstOrDefaultAsync(h => h.Id == id);
            
            if(hotel is null) return NotFound("The hotel was not found!");

            var hotelGet = mapper.Map<HotelGetDto>(hotel);

            return Ok(hotelGet);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody]HotelCreateDto hotel){
            
            var domainHotel = mapper.Map<Hotel>(hotel);

            ctx.Hotels.Add(domainHotel);
            await ctx.SaveChangesAsync();

            var hotelGet = mapper.Map<HotelGetDto>(domainHotel);

            return CreatedAtAction(nameof(GetHotelById), new { Id = domainHotel.Id}, hotelGet);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel([FromBody] Hotel updatedHotel, int id){
            
            var toUpdate = mapper.Map<Hotel>(updatedHotel);

            toUpdate.Id = id;

            ctx.Hotels.Update(toUpdate);
            await ctx.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id){

            var hotel = await ctx.Hotels.FirstOrDefaultAsync(h => h.Id == id);

            if(hotel is null) return NotFound("Hotel was not found!");

            ctx.Hotels.Remove(hotel);
            await ctx.SaveChangesAsync();
            
            return NoContent();
        }

        [HttpGet("{hotelId}/rooms")]
        public async Task<IActionResult> GetAllHotelRooms(int hotelId)
        {
            var rooms = await ctx.Rooms.Where(r => r.HotelId == hotelId).ToListAsync();

            var mappedRooms = mapper.Map<List<RoomGetDto>>(rooms);

            return Ok(mappedRooms);
        }

        [HttpGet("{hotelId}/rooms/{roomId}")]
        public async Task<IActionResult> GetHotelRoomById(int hotelId, int roomId){
            var room = await ctx.Rooms.FirstOrDefaultAsync(r => r.HotelId == hotelId && r.Id == roomId);

            if(room is null) return NotFound("Room was not found!");

            var mappedRoom = mapper.Map<RoomGetDto>(room);

            return Ok(mappedRoom);
        }

        [HttpPost("{hotelId}/rooms")]
        public async Task<IActionResult> AddHotelRoom([FromBody] RoomPostDto newRoom, int hotelId){

            var room = mapper.Map<Room>(newRoom);
            room.HotelId = hotelId;

            ctx.Rooms.Add(room);
            await ctx.SaveChangesAsync();

            // var hotel = await ctx.Hotels.Include(h => h.Rooms).FirstOrDefaultAsync(h => h.Id == hotelId);
            // if(hotel is null) return NotFound("hotel was not found!");
            // hotel.Rooms.Add(room);
            // await ctx.SaveChangesAsync();

            var mappedRoom = mapper.Map<RoomGetDto>(room);

            return CreatedAtAction(nameof(GetHotelRoomById), new { hotelId = hotelId, roomId = room.Id}, mappedRoom);
        }

        [HttpPut("{hotelId}/rooms/{roomId}")]
        public async Task<IActionResult> UpdateHotelRoom(int hotelId, int roomId, [FromBody] RoomPostDto updatedRoom)
        {
            var roomToUpdate = mapper.Map<Room>(updatedRoom);
            roomToUpdate.Id = roomId;
            roomToUpdate.HotelId = hotelId;

            ctx.Rooms.Update(roomToUpdate);
            await ctx.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{hotelId}/rooms/{roomId}")]
        public async Task<IActionResult> RemoveHotelRoom(int roomId, int hotelId)
        {
            var room = await ctx.Rooms.SingleOrDefaultAsync(r => r.Id == roomId && r.HotelId == hotelId);

            if(room is null) return NotFound("Room was not found!");

            ctx.Rooms.Remove(room);
            await ctx.SaveChangesAsync();

            return NoContent();
        }
    }
}