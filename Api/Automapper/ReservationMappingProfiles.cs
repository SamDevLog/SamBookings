using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using AutoMapper;
using Domain.Models;

namespace Api.Automapper
{
    public class ReservationMappingProfiles : Profile
    {
        public ReservationMappingProfiles()
        {
            CreateMap<ReservationPostDto, Reservation>();
            CreateMap<Reservation, ReservationGetDto>();            
        }
    }
}