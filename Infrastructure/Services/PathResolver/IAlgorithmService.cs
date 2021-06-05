using Infrastructure.Services.Models;

namespace Infrastructure.Services
{
    public interface IAlgorithmService
    {
        ShortestPathModel FindShortestPath(int RoomFromId, int RoomToId);
    }
}
