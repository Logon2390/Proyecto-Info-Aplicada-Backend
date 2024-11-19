namespace Backend.Models.DTOs
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public required string Password { get; set; }
    }
}
