using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoNADD.Models
{
    public class Complexidade
    {
        [Key]
        public int Id_Complexidade { get; set; }
        public string Nome_Complexidade { get; set; }
        public ICollection<Questao> Questao { get; set; }
    }
}
