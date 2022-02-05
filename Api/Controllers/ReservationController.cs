using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using AutoMapper;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService reservationService;
        private readonly IMapper _mapper;

        public ReservationController(IReservationService reservationService, IMapper mapper)
        {
            _mapper = mapper;
            this.reservationService = reservationService;
        }

        [HttpPost]
        public async Task<IActionResult> MakeReservation([FromBody] ReservationPostDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);

            await reservationService.MakeReservationAsync(reservation);

            return Ok();
        }

    }
}