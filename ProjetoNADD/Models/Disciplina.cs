using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoNADD.Models
{
    public class Disciplina
    {
        [Key]
        [Display(Name = "Id")]
        public int Id_Disciplina { get; set; }
        [Display(Name = "Nome")]
        public string Nome_Disciplina { get; set; }
        [Display(Name = "Período")]
        public int Periodo_Disciplina { get; set; }
        [Display(Name = "Ano")]
        public string Ano_Disciplina { get; set; }
        [Display(Name = "Curso")]
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
        public virtual ICollection<DisciplinaProfessor> DisciplinaProfessor { get; set; }
        public ICollection<Avaliacao> Avaliacao { get; set; }
    }
}
