using Infrastructure.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IRoomService
    {
        List<RoomModel> GetAll();

        Task<List<RoomModel>> GetByFloor(int floor);

        RoomModel Get(int id);

        Task<List<RoomModel>> GetByNumber(string roomNumber);

        void AddRange(List<RoomModel> models);
    }
}
