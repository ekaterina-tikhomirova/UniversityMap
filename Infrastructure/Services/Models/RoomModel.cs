using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Models
{
    public class RoomModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int Floor { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class RoomModelProfile : Profile
    {
        public RoomModelProfile()
        {
            CreateMap<RoomModel, Room>().ReverseMap();
        }
    }
}
