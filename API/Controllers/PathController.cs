using Infrastructure.Services;
using Infrastructure.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/path")]
    [ApiController]
    public class PathController : ControllerBase
    {
        private readonly IPathService service;

        public PathController(IPathService service)
        {
            this.service = service;
        }

        [HttpPost]
        public void AddRange([FromBody]List<PathModel> paths)
        {
            service.AddRange(paths);
        }
    }
}
