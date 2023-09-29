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

namespace labRecurso.Controllers
{
    public class PerfilCategoriasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public PerfilCategoriasController(ApplicationDbContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: PerfilCategorias
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                string userId = user.Id;
                var perfil = _context.Perfil.FirstOrDefault(p => p.utilizadorId == userId);
                var applicationDbContext = _context.PerfilCategoria.Where(p => p.perfilId == perfil.Id).Include(p => p.Categoria).Include(p => p.perfil);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var applicationDbContext = _context.PerfilCategoria.Include(p => p.Categoria).Include(p => p.perfil);
                return View(await applicationDbContext.ToListAsync());
            }
        }

        // GET: PerfilCategorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PerfilCategoria == null)
            {
                return NotFound();
            }

            var perfilCategoria = await _context.PerfilCategoria
                .Include(p => p.Categoria)
                .Include(p => p.perfil)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perfilCategoria == null)
            {
                return NotFound();
            }

            return View(perfilCategoria);
        }

        // GET: PerfilCategorias/Create
        public IActionResult Create()
        {
            ViewData["categoriaId"] = new SelectList(_context.Categoria, "Id", "Nome");
            ViewData["perfilId"] = new SelectList(_context.Perfil, "Id", "utilizadorId");
            return View();
        }

        // POST: PerfilCategorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,perfilId,categoriaId")] PerfilCategoria perfilCategoria)
        {
            var utilizador = await _context.Users.SingleOrDefaultAsync(u => u.UserName == User.Identity.Name);

            if (utilizador != null)
            {
                var perfil = await _context.Perfil.SingleOrDefaultAsync(p => p.utilizadorId == utilizador.Id);
                if (perfil != null)
                {
                    perfilCategoria.perfil = perfil;
                    perfilCategoria.perfilId = perfil.Id;
                }

            }
            if (ModelState.IsValid)
            {
                _context.Add(perfilCategoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["categoriaId"] = new SelectList(_context.Categoria, "Id", "Nome", perfilCategoria.categoriaId);
            ViewData["perfilId"] = new SelectList(_context.Perfil, "Id", "utilizadorId", perfilCategoria.perfilId);
            return View(perfilCategoria);
        }

        // GET: PerfilCategorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PerfilCategoria == null)
            {
                return NotFound();
            }

            var perfilCategoria = await _context.PerfilCategoria.FindAsync(id);
            if (perfilCategoria == null)
            {
                return NotFound();
            }
            ViewData["categoriaId"] = new SelectList(_context.Categoria, "Id", "Nome", perfilCategoria.categoriaId);
            ViewData["perfilId"] = new SelectList(_context.Perfil, "Id", "utilizadorId", perfilCategoria.perfilId);
            return View(perfilCategoria);
        }

        // POST: PerfilCategorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,perfilId,categoriaId")] PerfilCategoria perfilCategoria)
        {
            if (id != perfilCategoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(perfilCategoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerfilCategoriaExists(perfilCategoria.Id))
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
            ViewData["categoriaId"] = new SelectList(_context.Categoria, "Id", "Nome", perfilCategoria.categoriaId);
            ViewData["perfilId"] = new SelectList(_context.Perfil, "Id", "utilizadorId", perfilCategoria.perfilId);
            return View(perfilCategoria);
        }

        // GET: PerfilCategorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PerfilCategoria == null)
            {
                return NotFound();
            }

            var perfilCategoria = await _context.PerfilCategoria
                .Include(p => p.Categoria)
                .Include(p => p.perfil)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perfilCategoria == null)
            {
                return NotFound();
            }

            return View(perfilCategoria);
        }

        // POST: PerfilCategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PerfilCategoria == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PerfilCategoria'  is null.");
            }
            var perfilCategoria = await _context.PerfilCategoria.FindAsync(id);
            if (perfilCategoria != null)
            {
                _context.PerfilCategoria.Remove(perfilCategoria);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerfilCategoriaExists(int id)
        {
          return _context.PerfilCategoria.Any(e => e.Id == id);
        }
    }
}
