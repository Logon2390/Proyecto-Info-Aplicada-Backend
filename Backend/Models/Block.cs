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

        public string FechaMinado { get; set; }
        public int Prueba { get; set; }
        public long Milisegundos { get; set; }
        public String Documentos { get; set; }
        public string HashPrevio { get; set; }
        public string Hash { get; set; }
    }
}

