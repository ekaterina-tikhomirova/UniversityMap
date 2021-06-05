using Infrastructure.Services;
using Infrastructure.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace API.Controllers
{
    [Route("api/room")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService service;

        public RoomController(IRoomService service)
        {
            this.service = service;
        }

        [HttpGet("getall")]
        public List<RoomModel> GetAll()
        {
            return service.GetAll();
        }

        [HttpGet("~/api/floor/{floorNumber}/rooms")]
        public async Task<List<RoomModel>> GetByFloor(int floorNumber)
        {
            return await service.GetByFloor(floorNumber);
        }

        [HttpGet("{id}")]
        public RoomModel Get(int id)
        {
            return service.Get(id);
        }

        [HttpGet("~/api/number/{roomNumber}/rooms")]
        public async Task<List<RoomModel>> Get(string roomNumber)
        {
            return await service.GetByNumber(roomNumber);
        }

        [HttpPost]
        public void Post([FromBody] List<RoomModel> rooms)
        {
            service.AddRange(rooms);
        }
    }
}
