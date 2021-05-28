using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.Models
{
    public class Filme
    {
        /// <summary>
        /// A chave primaria é FilmeId
        /// </summary>
        [Key]
        public int FilmeId { get; set; }
        /// <summary>
        /// O campo Nome é obrigatorio e tamanho máximo de 200 caracteres.
        /// </summary>
        [Required]
        [MaxLength (200)]
        public string Nome { get; set; }
        [Display (Name = "Data Criação")]
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
        public int GeneroId { get; set; }
        public virtual Genero Genero { get; set; }
        public List<Locacao> ListaLocacao { get; set; }
    }
}
