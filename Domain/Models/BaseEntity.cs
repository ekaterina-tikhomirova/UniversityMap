using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
