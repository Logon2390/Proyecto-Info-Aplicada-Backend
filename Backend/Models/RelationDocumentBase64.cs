using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class RelationDocumentBase64
    {
        [Key]
        public int DocumentId { get; set; }
        public required string Base64 { get; set; }
    }
}
