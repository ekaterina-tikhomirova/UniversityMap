using System;
using System.Collections.Generic;

namespace Infrastructure.Services.Models
{
    public class MapModel
    {
        public List<PathModel> paths { get; set; }
        public List<RoomModel> rooms { get; set; }
    }
}
