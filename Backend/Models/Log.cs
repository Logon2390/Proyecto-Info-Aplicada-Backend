using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public required string username { get; set; }
        public required string Message { get; set; }
    }
}
