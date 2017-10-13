using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlugaLivros.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioID { get; set; }

        [Display(Name = "Nome")]
        [Required]
        [StringLength(200, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Display(Name = "Email")]
        [Required]
        [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [Required]
        public string Senha { get; set; }

        public virtual ICollection<Emprestimo> Emprestimo { get; set; }
    }
}
