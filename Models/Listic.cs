using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BingoTestAPI.Models
{
    public class Listic
    {
        [Required]
        [Key]
        public int Id_listica { get; set; }

        
        [ForeignKey("Korisnik_id")]
        public Korisnici Korisnici { get; set; }
        public int Korisnik_id { get; set; }

        
        [ForeignKey("Runda_id")]
        public Runda Runda { get; set; }
        public int Runda_id { get; set; }

        [MaxLength(35)]
        [MinLength(11)]
        public string Odigrani_brojevi { get; set; }

        [MaxLength(35)]
        public string Izvuceni_brojevi { get; set; }

        public string Ulog { get; set; }

        public string Dobit { get; set; }

    }
}
