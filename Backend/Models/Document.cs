namespace Backend.Models
{
    public class Document
    {
        public int Id { get; set; }
        public  required string owner { get; set; }
        public  required string type { get; set; }
        public DateTime CreatedAt { get; set; }
        public int size { get; set; }
        public required string base64 { get; set; }

    }
}
