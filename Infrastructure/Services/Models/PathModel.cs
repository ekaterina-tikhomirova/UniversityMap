using AutoMapper;
using Domain.Models;

namespace Infrastructure.Services.Models
{
    public class PathModel
    {
        public double Distance { get; set; }
        public int FirstRoomId { get; set; }
        public int SecondRoomId { get; set; }
    }

    public class PathModelProfile : Profile
    {
        public PathModelProfile()
        {
            CreateMap<PathModel, Path>().ReverseMap();
        }
    }
}
