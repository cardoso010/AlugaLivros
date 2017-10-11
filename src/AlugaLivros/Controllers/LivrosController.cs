using AlugaLivros.Data;
using AlugaLivros.Models;
using AlugaLivros.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlugaLivros.Controllers
{
    public class LivrosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LivrosController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Livros
        public async Task<IActionResult> Index(string filtroPesquisa)
        {
            ViewBag.filtroPesquisa = filtroPesquisa;
            var livros = from l in _context.Livro
                         select l;
            if (!String.IsNullOrEmpty(filtroPesquisa))
            {
                livros = livros.Where(s => s.Titulo.ToUpper().Contains(filtroPesquisa.ToUpper()));
            }
            return View(await livros.ToListAsync());
        }

        // GET: Livros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro.AsNoTracking()
                                 .Include(l => l.LivroAutor)
                                 .ThenInclude(li => li.Autor)
                                 .SingleOrDefaultAsync(m => m.LivroID == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // GET: Livros/Create
        public IActionResult Create()
        {
            ViewBag.Autores = new Listagens(_context).AutoresCheckBox();

            return View(new Livro());
        }

        // POST: Livros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LivroID,Foto,Quantidade,Titulo,AutorUnico")] Livro livro, string[] selectedAutores)
        {
            if (ModelState.IsValid)
            {

                if (selectedAutores != null)
                {

                    livro.LivroAutor = new List<LivroAutor>();
                    foreach (var idAutor in selectedAutores) { 
                        livro.LivroAutor.Add(new LivroAutor()
                        {
                            AutorID = int.Parse(idAutor),
                            Livro = livro
                        });
                    }
                }
                _context.Add(livro);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(livro);
        }

        // GET: Livros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autoresAux = new Listagens(_context).AutoresCheckBox();

            var livro = await _context.Livro.Include(l => l.LivroAutor)
                .SingleOrDefaultAsync(m => m.LivroID == id);

            autoresAux.ForEach(a => a.Checked = livro.LivroAutor.Any(l => l.AutorID == a.Value));
            ViewBag.Autores = autoresAux;

            if (livro == null)
            {
                return NotFound();
            }
            return View(livro);
        }

        // POST: Livros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LivroID,Foto,Quantidade,Titulo")] Livro livro, string[] selectedAutores)
        {
            if (id != livro.LivroID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var livroAutores =
                        _context.LivroAutor.AsNoTracking().Where(la => la.LivroID == livro.LivroID);
                    _context.LivroAutor.RemoveRange(livroAutores);

                    await _context.SaveChangesAsync();
                    if (selectedAutores != null)
                    { 
                        livro.LivroAutor = new List<LivroAutor>();
                        foreach (var idAutor in selectedAutores)
                        {
                            livro.LivroAutor.Add(new LivroAutor()
                            {
                                AutorID = int.Parse(idAutor),
                                Livro = livro
                            });
                        }
                    }
                    _context.Update(livro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivroExists(livro.LivroID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(livro);
        }

        // GET: Livros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro.SingleOrDefaultAsync(m => m.LivroID == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livro = await _context.Livro.SingleOrDefaultAsync(m => m.LivroID == id);
            _context.Livro.Remove(livro);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool LivroExists(int id)
        {
            return _context.Livro.Any(e => e.LivroID == id);
        }
    }
}
