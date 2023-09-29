using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using labRecurso.Data;
using labRecurso.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace labRecurso.Controllers
{
    public class JogosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostEnvironment _environment;
       
        public JogosController(ApplicationDbContext context, IHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Jogos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Jogos.Include(j => j.categoria).Include(j => j.plataforma);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Homepage()
        {
            var applicationDbContext = _context.Jogos.Include(j => j.categoria).Include(j => j.plataforma);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: Jogos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Jogos == null)
            {
                return NotFound();
            }

            var jogos = await _context.Jogos
                .Include(j => j.categoria)
                .Include(j => j.plataforma)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jogos == null)
            {
                return NotFound();
            }
            var comentarios = new List<Comentario>();

            comentarios=_context.Comentario.Where(c=>c.JogoId==id).ToList();
            jogos.Comentarios=comentarios;
            float media = 0;
            int auxiliar = 0;
            foreach(var p in _context.Avalicao.Where(c => c.JogoId == id).ToList())
            {
                media += p.pontuacao;
                auxiliar++;
                
            }
            if (auxiliar != 0) { media = media / auxiliar; }
            else
            {
                media = 0;
            }
            jogos.Pontuacao = (int)media;
            return View(jogos);
        }

        // GET: Jogos/Create
        public IActionResult Create()
        {
            ViewData["categoriaId"] = new SelectList(_context.Categoria, "Id", "Nome");
            ViewData["plataformaId"] = new SelectList(_context.Set<Plataforma>(), "Id", "Name");
            return View();
        }

        // POST: Jogos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Foto,Preco,plataformaId,Pontuacao,categoriaId")] Jogos jogos, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                if (foto != null)
                {
                    string distination = Path.Combine(_environment.ContentRootPath, "wwwroot/Fotos/Jogos/", Path.GetFileName(foto.FileName));
                    FileStream fs = new FileStream(distination, FileMode.Create);
                    foto.CopyTo(fs);
                    fs.Close();
                    jogos.Foto = foto.FileName;
                }
                _context.Add(jogos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["categoriaId"] = new SelectList(_context.Categoria, "Id", "Nome", jogos.categoriaId);
            ViewData["plataformaId"] = new SelectList(_context.Set<Plataforma>(), "Id", "Name", jogos.plataformaId);
            return View(jogos);
        }

        // GET: Jogos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Jogos == null)
            {
                return NotFound();
            }

            var jogos = await _context.Jogos.FindAsync(id);
            if (jogos == null)
            {
                return NotFound();
            }
            ViewData["categoriaId"] = new SelectList(_context.Categoria, "Id", "Nome", jogos.categoriaId);
            ViewData["plataformaId"] = new SelectList(_context.Set<Plataforma>(), "Id", "Name", jogos.plataformaId);
            return View(jogos);
        }

        // POST: Jogos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Foto,Preco,plataformaId,Pontuacao,categoriaId")] Jogos jogos)
        {
            if (id != jogos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jogos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JogosExists(jogos.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["categoriaId"] = new SelectList(_context.Categoria, "Id", "Nome", jogos.categoriaId);
            ViewData["plataformaId"] = new SelectList(_context.Set<Plataforma>(), "Id", "Name", jogos.plataformaId);
            return View(jogos);
        }

        // GET: Jogos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Jogos == null)
            {
                return NotFound();
            }

            var jogos = await _context.Jogos
                .Include(j => j.categoria)
                .Include(j => j.plataforma)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jogos == null)
            {
                return NotFound();
            }

            return View(jogos);
        }

        // POST: Jogos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Jogos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Jogos'  is null.");
            }
            var jogos = await _context.Jogos.FindAsync(id);
            if (jogos != null)
            {
                _context.Jogos.Remove(jogos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JogosExists(int id)
        {
          return _context.Jogos.Any(e => e.Id == id);
        }
    }
}
