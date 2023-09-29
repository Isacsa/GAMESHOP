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

namespace labRecurso.Controllers
{
    public class PerfilJogosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public PerfilJogosController(ApplicationDbContext context,UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: PerfilJogos
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                string userId = user.Id;
                var perfil = _context.Perfil.FirstOrDefault(p => p.utilizadorId == userId);
            var applicationDbContext = _context.PerfilJogos.Where(p => p.perfilId==perfil.Id).Include(p => p.jogo).Include(p => p.perfil);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var applicationDbContext = _context.PerfilJogos.Include(p => p.jogo).Include(p => p.perfil);
                return View(await applicationDbContext.ToListAsync());
            }
        }
        [Authorize(Roles = "Admin, Funcionario, Cliente")]
        public async Task<IActionResult> Comprar(int ? id)
        {
          
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                string userId = user.Id;
            var perfil = _context.Perfil.FirstOrDefault(p => p.utilizadorId == userId);
            var jogo = _context.Jogos.Find(id);
                _context.PerfilJogos.Add(new PerfilJogos()
                {
                    jogo = jogo,
                    perfil = perfil
                });
                await _context.SaveChangesAsync();
            }
           
            return RedirectToAction("Index");
        }
        // GET: PerfilJogos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PerfilJogos == null)
            {
                return NotFound();
            }

            var perfilJogos = await _context.PerfilJogos
                .Include(p => p.jogo)
                .Include(p => p.perfil)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perfilJogos == null)
            {
                return NotFound();
            }

            return View(perfilJogos);
        }

        // GET: PerfilJogos/Create
        public IActionResult Create()
        {
            ViewData["jogoId"] = new SelectList(_context.Jogos, "Id", "Nome");
            ViewData["perfilId"] = new SelectList(_context.Perfil, "Id", "utilizadorId");
            return View();
        }

        // POST: PerfilJogos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,perfilId,jogoId")] PerfilJogos perfilJogos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(perfilJogos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["jogoId"] = new SelectList(_context.Jogos, "Id", "Nome", perfilJogos.jogoId);
            ViewData["perfilId"] = new SelectList(_context.Perfil, "Id", "utilizadorId", perfilJogos.perfilId);
            return View(perfilJogos);
        }

        // GET: PerfilJogos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PerfilJogos == null)
            {
                return NotFound();
            }

            var perfilJogos = await _context.PerfilJogos.FindAsync(id);
            if (perfilJogos == null)
            {
                return NotFound();
            }
            ViewData["jogoId"] = new SelectList(_context.Jogos, "Id", "Nome", perfilJogos.jogoId);
            ViewData["perfilId"] = new SelectList(_context.Perfil, "Id", "utilizadorId", perfilJogos.perfilId);
            return View(perfilJogos);
        }

        // POST: PerfilJogos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,perfilId,jogoId")] PerfilJogos perfilJogos)
        {
            if (id != perfilJogos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(perfilJogos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerfilJogosExists(perfilJogos.Id))
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
            ViewData["jogoId"] = new SelectList(_context.Jogos, "Id", "Nome", perfilJogos.jogoId);
            ViewData["perfilId"] = new SelectList(_context.Perfil, "Id", "utilizadorId", perfilJogos.perfilId);
            return View(perfilJogos);
        }

        // GET: PerfilJogos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PerfilJogos == null)
            {
                return NotFound();
            }

            var perfilJogos = await _context.PerfilJogos
                .Include(p => p.jogo)
                .Include(p => p.perfil)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perfilJogos == null)
            {
                return NotFound();
            }

            return View(perfilJogos);
        }

        // POST: PerfilJogos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PerfilJogos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PerfilJogos'  is null.");
            }
            var perfilJogos = await _context.PerfilJogos.FindAsync(id);
            if (perfilJogos != null)
            {
                _context.PerfilJogos.Remove(perfilJogos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerfilJogosExists(int id)
        {
          return _context.PerfilJogos.Any(e => e.Id == id);
        }
    }
}
