using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjetoNADD.Models
{
    public class Curso
    {
        [Key]
        [Display(Name = "Id")]
        public int Id_Curso { get; set; }
        [Display(Name = "Nome")]
        public string Nome_Curso { get; set; }
        [Display(Name = "Área")]
        public int AreaId { get; set; }
        public Area Area { get; set; }
        public ICollection<Disciplina> Disciplinas { get; set; }
    }
}
