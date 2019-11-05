using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjetoNADD.Models
{
    public class Ano
    {
        [Key]
        public string Ano_id { get; set; }
        public virtual ICollection<Disciplina> Disciplina { get; set; }
    }
}
