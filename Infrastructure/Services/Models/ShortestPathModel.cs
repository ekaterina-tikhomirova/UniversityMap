using System;
using System.Collections.Generic;

namespace Infrastructure.Services.Models
{
    public class ShortestPathModel
    {
        public bool IsPathFound { get; set; }
        public List<int> Path { get; set; }
        public double FinalDistance { get; set; }
    }
}
