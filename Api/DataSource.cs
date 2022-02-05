using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Api
{
    public class DataSource
    {
        public DataSource()
        {
            Hotels = GetHotels();
        }

        public List<Hotel> Hotels {get; set;}

        public List<Hotel> GetHotels(){
            return new List<Hotel>{
                new Hotel{
                    Id= 1,
                    Name= "Hilton Marrakech",
                    Stars= 5,
                    Description= "The famous 5 stars hotel chaine among the best in the world",
                    Address="Moulay Youssef",
                    City="Marrakech",
                    Country="Morocco"  
                },
                new Hotel{
                    Id= 2,
                    Name= "Mamouna",
                    Stars= 5,
                    Description= "The currently best hotel in the world! Ideal destination for filming in exquisite Moroccan architecture and design",
                    Address="Daoudiyat",
                    City="Marrakech",
                    Country="Morocco"  
                },
            };
        }
    }
}