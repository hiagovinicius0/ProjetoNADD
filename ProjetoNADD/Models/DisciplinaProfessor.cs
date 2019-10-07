using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoNADD.Models
{
    [Table("DisciplinaProfessor")]
    public class DisciplinaProfessor
    {
        [Display(Name = "Professor")]
        public int Professor_id { get; set; }
        Professor Professor { get; set; }
        [Display(Name = "Disciplina")]
        public int Disciplina_id { get; set; }
        Disciplina Disciplina { get; set; }
    }
}
