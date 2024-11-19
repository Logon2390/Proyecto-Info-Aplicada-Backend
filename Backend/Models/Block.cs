using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Block
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public required string FechaMinado { get; set; }
        public int Prueba { get; set; }
        public long Milisegundos { get; set; }
        public required String Documentos { get; set; }
        public required string HashPrevio { get; set; }
        public required string Hash { get; set; }
    }
}

