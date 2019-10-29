using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProjetoNADD.Models
{
    public class Usuario: IdentityUser
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        public string Nome_User { get; set; }
    }
}
