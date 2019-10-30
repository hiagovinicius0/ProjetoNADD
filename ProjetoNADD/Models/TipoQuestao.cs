using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoNADD.Models
{
    public class TipoQuestao
    {
        [Key]
        public int Id_TipoQuestao { get; set; }
        public string Nome_TipoQuestao { get; set; }
        public ICollection<Questao> Questao { get; set; }
    }
}
