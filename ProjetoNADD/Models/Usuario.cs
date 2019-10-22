using System.ComponentModel.DataAnnotations;

namespace ProjetoNADD.Models
{
    public class Usuario
    {
        [Key]
        public int Id_Usuario { get; set; }
        [Display(Name = "Login")]
        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        public string Login_Usuario { get; set; }
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        public string Nome_Usuario { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha_Usuario { get; set; }
    }
}
