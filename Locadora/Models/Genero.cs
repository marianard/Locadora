using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.Models
{
    public class Genero
    {
        /// <summary>
        /// A chave da tabela é GeneroId
        /// </summary>
        [Key]
        public int GeneroId { get; set; }
        /// <summary>
        /// O campo Nome é obrigatorio e tamanho máximo de 200 caracteres.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }
        [Display(Name = "Data Criação")]
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}
