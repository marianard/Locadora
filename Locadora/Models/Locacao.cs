using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.Models
{
    public class Locacao
    {
        [Key]
        public int LocacaoId { get; set; }
        [Required]
        [MaxLength(14)]
        public string CpfCliente { get; set; }
        public DateTime DataLocacao { get; set; }
        [Required]
        public List<Filme> ListaFilme {get;set;}
    }
}
