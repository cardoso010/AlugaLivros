using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlugaLivros.Models
{
    public class Emprestimo
    {
        [Key]
        public int EmprestimoID { get; set; }

        public int UsuarioID { get; set; }
        public virtual Usuario Usuario { get; set; }

        [Display(Name = "Data inicio")]
        public string DataInicio { get; set; }

        [Display(Name = "Data fim")]
        public string DataFim { get; set; }

        [Display(Name = "Data devolução")]
        public string DataDevolucao { get; set; }

        public ICollection<LivroEmprestimo> LivroEmprestimo { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
