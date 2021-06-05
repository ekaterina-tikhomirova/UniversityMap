using API.Models;
using Infrastructure.Services;
using Infrastructure.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/pathresolver")]
    [ApiController]
    public class PathResolverController : ControllerBase
    {
        private readonly IAlgorithmService algorithmService;
        public PathResolverController(IAlgorithmService algorithmService)
        {
            this.algorithmService = algorithmService;
        }

        [HttpPost]
        public IActionResult FindShortestPath([FromBody] PathResolverRequest model)
        {
            var result = algorithmService.FindShortestPath(model.RoomFromId, model.RoomToId);
            if (result == null)
                result = new ShortestPathModel() { IsPathFound = false };
            else
                result.IsPathFound = true;

            return Ok(result);
        }
    }
}
