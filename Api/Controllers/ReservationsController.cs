using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Errors;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService reservationService;
        private readonly IReservationsRepository reservationsRepository;
        private readonly IMapper _mapper;

        public ReservationsController(IReservationService reservationService, IReservationsRepository reservationsRepository, IMapper mapper)
        {
            _mapper = mapper;
            this.reservationService = reservationService;
            this.reservationsRepository = reservationsRepository;
        }

        [HttpPost]
        public async Task<IActionResult> MakeReservation([FromBody] ReservationPostDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);

            var reservationResponse = await reservationService.MakeReservationAsync(reservation);

            if(reservationResponse is null) return BadRequest(new ApiResponse(400));

            var mappedReservationResponse = _mapper.Map<ReservationGetDto>(reservationResponse);

            return Ok(mappedReservationResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await reservationsRepository.GetAllReservationsAsync();
            var mappedReservations = _mapper.Map<List<ReservationGetDto>>(reservations);

            return Ok(mappedReservations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var reservation = await reservationsRepository.GetReservationByIdAsync(id);

            if(reservation is null) return NotFound(new ApiResponse(404));

            var mappedReservation = _mapper.Map<ReservationGetDto>(reservation);

            return Ok(mappedReservation);
        }

        [HttpGet("{hotelId}/rooms/{roomId}")]
        public async Task<IActionResult> GetRoomReservations(int hotelId, int roomId)
        {
            var reservations = await reservationsRepository.GetRoomReservationsAsync(hotelId, roomId);

            var mappedReservations = _mapper.Map<List<ReservationGetDto>>(reservations);
            return Ok(mappedReservations);
        }

        [HttpGet("hotels/{hotelId}")]
        public async Task<IActionResult> GetHotelReservations(int hotelId)
        {
            var reservations = await reservationsRepository.GetHotelReservationsAsync(hotelId);

            var mappedReservations = _mapper.Map<List<ReservationGetDto>>(reservations);

            return Ok(mappedReservations);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelReservation(int id)
        {
            var reservation = await reservationService.CancelReservationAsync(id);
            
            if(reservation is null) return NotFound(new ApiResponse(404));

            return NoContent();
        }

    }
}