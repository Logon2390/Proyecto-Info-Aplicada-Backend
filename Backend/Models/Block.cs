namespace Backend.Models
{
    public class Block
    {
        public int Id { get; set; }
        public string FechaMinado { get;  set; }
        public int Prueba { get;  set; }
        public long Milisegundos { get;  set; }
        public List<string> Documentos { get;  set; }
        public string HashPrevio { get; set; }
        public string Hash { get;  set; }
    }
}
