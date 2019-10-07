using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjetoNADD.Models
{
    public class Area
    {
        [Key]
        [Display(Name = "Id")]
        public int Id_Area { get; set; }
        [Display(Name = "Nome")]
        public string Nome_Area { get; set; }
        public ICollection<Curso> Cursos { get; set; }
    }
}
