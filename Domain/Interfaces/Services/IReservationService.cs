using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces.Services
{
    public interface IReservationService
    {
        Task<Reservation> MakeReservationAsync(Reservation reservation);
    }
}