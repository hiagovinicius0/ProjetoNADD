using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjetoNADD.Models
{
    public class Complexidade
    {
        [Key]
        public int Id_Complexidade { get; set; }
        public string Nome_Complexidade { get; set; }
        public ICollection<Avaliacao> Avaliacao { get; set; }
        public ICollection<Questao> Questao { get; set; }
    }
}
