using Infrastructure.Repositories;
using Infrastructure.Services.Models;
using Infrastructure.Services.PathResolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AlgorithmService : IAlgorithmService
    {
        private readonly IPathService pathService;
        private readonly IRoomService roomService;

        public AlgorithmService(IPathService pathService,
                                IRoomService roomService)
        {
            this.pathService = pathService;
            this.roomService = roomService;
        }

        public ShortestPathModel FindShortestPath(int RoomFromId, int RoomToId)
        {
            MapModel map = new MapModel 
            {
                paths = pathService.GetAll(),
                rooms = roomService.GetAll()
            };
            ShortestPathModel shortestPathResponseDTO = new ShortestPathResolverService()
                .FindShortestPath(map, RoomFromId, RoomToId);
            return shortestPathResponseDTO;
        }
    }
}
