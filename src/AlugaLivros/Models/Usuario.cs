using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlugaLivros.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioID { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Telefone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }

        public virtual ICollection<Emprestimo> Emprestimo { get; set; }
    }
}
