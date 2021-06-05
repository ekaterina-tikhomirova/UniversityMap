using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Room : BaseEntity
    {
        [Required]
        public string Number { get; set; }
        [Required]
        public int Floor { get; set; }
        [Required]
        public double X { get; set; }
        [Required]
        public double Y { get; set; }

        public List<Path> PathsWhenThisFirst { get; set; }
        public List<Path> PathsWhenThisSecond { get; set; }
    }
}
