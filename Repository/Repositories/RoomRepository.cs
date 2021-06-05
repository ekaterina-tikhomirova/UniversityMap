using Domain.Models;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Repository.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(UniversityMapContext context) : base(context) { }

        public async Task<List<Room>> GetRoomsByFloor(int floor)
        {
            return await entity.Where(r => r.Floor == floor)
                               .ToListAsync();
        }

        public async Task<List<Room>> GetRoomsByNumber(string number)
        {
            return await entity.Where(r => r.Number == number)
                               .ToListAsync();
        }
    }
}
