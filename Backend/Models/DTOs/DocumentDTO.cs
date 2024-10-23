namespace Backend.Models.DTOs
{
    public class DocumentDTO
    {
        public string owner { get; set; }
        public string type { get; set; }
        public DateTime CreatedAt { get; set; }
        public int size { get; set; }
        public string base64 { get; set; }
    }
}
