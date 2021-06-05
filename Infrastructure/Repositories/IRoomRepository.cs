using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        Task<List<Room>> GetRoomsByFloor(int floor);
        Task<List<Room>> GetRoomsByNumber(string number);
    }
}
