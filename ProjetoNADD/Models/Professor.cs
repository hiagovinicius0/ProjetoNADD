using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjetoNADD.Models
{
    public class Professor
    {
        [Key]
        [Display(Name = "Id")]
        public int Id_Professor { get; set; }
        [Display(Name = "Nome")]
        public string Nome_Professor { get; set; }
        public ICollection<DisciplinaProfessor> DisciplinaProfessor { get; set; }
    }
}
