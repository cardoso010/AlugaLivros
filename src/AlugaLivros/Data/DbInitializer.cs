using AlugaLivros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlugaLivros.Data
{
    public static  class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            // Se existir algum livro.
            if (context.Livro.Any())
            {
                return; // DB possui registros
            }
            var livros = new Livro[]
            {
                new Livro {Titulo = "Google Android",Quantidade = 50},
                new Livro {Titulo = "Aprendendo pentest com python",Quantidade = 20},
                new Livro {Titulo = "Black Hat Python",Quantidade = 34},
                new Livro {Titulo = "Introdução ao desenvolvimento de jogos com python com pygame",Quantidade = 10},
                new Livro {Titulo = "Django Essencial",Quantidade = 70}
            };

            foreach (Livro l in livros)
            {
                context.Livro.Add(l);
            }

            var autores = new Autor[]
            {
                new Autor {Nome = "Jose"},
                new Autor {Nome = "Kacson"},
                new Autor {Nome = "Valquiria"},
                new Autor {Nome = "ROnaldo"},
                new Autor {Nome = "Fernando"}
            };

            foreach (Autor a in autores)
            {
                context.Autor.Add(a);
            }

            var usuarios = new Usuario[]
            {
                new Usuario {Nome = "ADministrador", Email = "administrator@administrator.com.br", Senha = "admin", Telefone = "313232133"},
                new Usuario {Nome = "Jose", Email = "jose@jose.com.br", Senha = "jose", Telefone = "31313131"},
                new Usuario {Nome = "Gabriel", Email = "gabriel@gabriel.com.br", Senha = "123141", Telefone = "4131323141"},
                new Usuario {Nome = "Felipe", Email = "felipe@felipe.com.br", Senha = "121231", Telefone = "4141989238"},
                new Usuario {Nome = "Antonio", Email = "antonio@antonio.com.br", Senha = "3132131", Telefone = "989989898"}
            };

            foreach (Usuario u in usuarios)
            {
                context.Usuario.Add(u);
            }

            context.SaveChanges();
        }
    }
}
