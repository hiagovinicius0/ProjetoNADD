using System.ComponentModel.DataAnnotations;

namespace ProjetoNADD.Models
{
    public class Questao
    {
        [Key]
        [Display(Name = "Id")]
        public int Id_Questao { get; set; }
        [Display(Name = "Número")]
        public int Id_Numero { get; set; }
        [Display(Name = "Avaliação")]
        public int Id_Avaliacao { get; set; }
        public Avaliacao Avaliacao { get; set; }
        [Display(Name = "Contextualização")]
        public bool Contextualizacao_Questao { get; set; }
        [Display(Name = "Clareza")]
        public bool Clareza_Questao { get; set; }
        [Display(Name = "Complexidade")]
        public string Complexidade_Questao { get; set; }
        [Display(Name = "Observações")]
        public string Observacoes_Questao { get; set; }
    }
}
