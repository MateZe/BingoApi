using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BingoTestAPI.Models
{
    public class Runda
    {
        [Required]
        [Key]
        public int Id_runde { get; set; }

        [MinLength(95)]
        [MaxLength(104)]
        public  string Izvuceni_brojevi { get; set; }

        public string Pocetak_runde { get; set; }

        public string Kraj_runde { get; set; }
    }
}
