using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BingoTestAPI.Models
{
    public class Korisnici
    {
        [Required]
        [Key]
        public int Id_korisnika { get; set; }

        [MaxLength(50)]
        public string Ime_prezime { get; set; }

        public string Balans { get; set; }

    }
}
