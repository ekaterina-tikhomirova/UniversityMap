using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories;
using Infrastructure.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository repository;
        private readonly IMapper mapper;

        public RoomService(IRoomRepository repository,
                           IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public List<RoomModel> GetAll()
        {
            return mapper.Map<List<RoomModel>>(repository.GetAll());
        }

        public async Task<List<RoomModel>> GetByFloor(int floor)
        {
            return mapper.Map<List<RoomModel>>(await repository.GetRoomsByFloor(floor));
        }

        public async Task<List<RoomModel>> GetByNumber(string roomNumber)
        {
            return mapper.Map<List<RoomModel>>(await repository.GetRoomsByNumber(roomNumber));
        }

        public RoomModel Get(int id)
        {
            return mapper.Map<RoomModel>(repository.Get(id));
        }

        public void AddRange(List<RoomModel> models)
        {
            foreach(var model in models)
                repository.Add(mapper.Map<Room>(model));
        }
    }
}
