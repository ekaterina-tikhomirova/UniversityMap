using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Path : BaseEntity
    {
        [Required]
        public double Distance { get; set; }

        public int FirstRoomId { get; set; }
        public Room FirstRoom { get; set; }

        public int SecondRoomId { get; set; }
        public Room SecondRoom { get; set; }
    }
}
