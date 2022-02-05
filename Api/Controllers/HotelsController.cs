using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using AutoMapper;
using Dal;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelsRepository hotelsRepo;
        private readonly IMapper mapper;

        public HotelsController(IHotelsRepository hotelsRepo, IMapper mapper)
        {
            this.hotelsRepo = hotelsRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels(){
            
            var hotels = await hotelsRepo.GetAllHotelsAsync();
            var hotelsGet = mapper.Map<List<HotelGetDto>>(hotels);
            

            return Ok(hotelsGet);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetHotelById(int id){
            var hotel = await hotelsRepo.GetHotelByIdAsync(id);

            if(hotel is null) return NotFound("Hotel was not found!");

            var hotelGet = mapper.Map<HotelGetDto>(hotel);

            return Ok(hotelGet);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody]HotelCreateDto hotel){
            
            var domainHotel = mapper.Map<Hotel>(hotel);

            await hotelsRepo.CreateHotelAsync(domainHotel);

            var hotelGet = mapper.Map<HotelGetDto>(domainHotel);

            return CreatedAtAction(nameof(GetHotelById), new { Id = domainHotel.Id}, hotelGet);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel([FromBody] Hotel updatedHotel, int id)
        {
            var toUpdate = mapper.Map<Hotel>(updatedHotel);
            toUpdate.Id = id;

            await hotelsRepo.UpdateHotelAsync(toUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await hotelsRepo.DeleteHotelAsync(id);
            
            if(hotel is null) return NotFound("Hotel was not found");
            
            return NoContent();
        }

        [HttpGet("{hotelId}/rooms")]
        public async Task<IActionResult> GetAllHotelRooms(int hotelId)
        {
            var rooms = await hotelsRepo.GetAllHotelRoomsAsync(hotelId);
            var mappedRooms = mapper.Map<List<RoomGetDto>>(rooms);

            return Ok(mappedRooms);
        }

        [HttpGet("{hotelId}/rooms/{roomId}")]
        public async Task<IActionResult> GetHotelRoomById(int hotelId, int roomId){

            var room = await hotelsRepo.GetHotelRoomByIdAsync(hotelId, roomId);

            if(room is null) return NotFound("This room was not found!");
            
            var mappedRoom = mapper.Map<RoomGetDto>(room);

            return Ok(mappedRoom);
        }

        [HttpPost("{hotelId}/rooms")]
        public async Task<IActionResult> AddHotelRoom([FromBody] RoomPostDto newRoom, int hotelId){

            var room = mapper.Map<Room>(newRoom);
            // room.HotelId = hotelId;

            await hotelsRepo.CreateHotelRoomAsync(hotelId, room);
            var mappedRoom = mapper.Map<RoomGetDto>(room);

            return CreatedAtAction(nameof(GetHotelRoomById), new { hotelId = hotelId, roomId = room.Id}, mappedRoom);
        }

        [HttpPut("{hotelId}/rooms/{roomId}")]
        public async Task<IActionResult> UpdateHotelRoom(int hotelId, int roomId, [FromBody] RoomPostDto updatedRoom)
        {
            var roomToUpdate = mapper.Map<Room>(updatedRoom);
            roomToUpdate.Id = roomId;
            roomToUpdate.HotelId = hotelId;
            
            await hotelsRepo.UpdateHotelRoomAsync(hotelId, roomToUpdate);

            return NoContent();
        }

        [HttpDelete("{hotelId}/rooms/{roomId}")]
        public async Task<IActionResult> RemoveHotelRoom(int roomId, int hotelId)
        {
            var room = await hotelsRepo.DeleteHotelRoomAsync(hotelId, roomId);

            if(room is null) return NotFound("Room was not found");

            return NoContent();
        }
    }
}