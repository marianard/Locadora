using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.Models
{
    public class Locacao
    {
        public Locacao()
        {
            ListaFilme = new List<Filme>();
        }
        /// <summary>
        /// A chave primaria é LocacaoId
        /// </summary>
        [Key]
        public int LocacaoId { get; set; }
        /// <summary>
        /// O campo CpfCliente é obrigatório e o tamanho máximo é 14 caracteres.
        /// </summary>
        [Required]
        [MaxLength(14)]
        [Display(Name = "Cpf Cliente")]
        public string CpfCliente { get; set; }
        [Display(Name = "Data Locação")]
        public DateTime DataLocacao { get; set; }
        [Required]
        public List<Filme> ListaFilme {get;set;}
    }
}
