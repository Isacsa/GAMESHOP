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
    public class PerfilsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public PerfilsController(ApplicationDbContext context , UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Perfils
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users;
            var model = new List<UsersRolesViewModel>();
            foreach (var user in users)
            {
                var roles = _userManager.GetRolesAsync(user).Result;
                model.Add(new UsersRolesViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = roles.SingleOrDefault()
                });
            }
            return View(model);
        }

        // GET: Perfils/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Perfil == null)
            {
                return NotFound();
            }

            var perfil = await _context.Perfil
                .Include(p => p.utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perfil == null)
            {
                return NotFound();
            }

            return View(perfil);
        }

        // GET: Perfils/Create
        public IActionResult Create()
        {
            ViewData["utilizadorId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Perfils/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,utilizadorId,saldo")] Perfil perfil)
        {
            if (ModelState.IsValid)
            {
                _context.Add(perfil);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["utilizadorId"] = new SelectList(_context.Users, "Id", "Id", perfil.utilizadorId);
            return View(perfil);
        }
        public async Task<IActionResult> Edit(string? id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var roles = _roleManager.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();

            var model = new UsersRolesViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Role = (await _userManager.GetRolesAsync(user)).SingleOrDefault(),
                Roles = roles
            };

            return View(model);
        }

        // POST: PerfilModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UsersRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Remove the user from all roles
            var result = await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "An error occurred while updating the user's roles.");
                return View(model);
            }

            // Add the user to the selected role
            result = await _userManager.AddToRoleAsync(user, model.Role);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "An error occurred while updating the user's role.");
                return View(model);
            }

            return RedirectToAction("Index");
        }


        // GET: Perfils/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Perfil == null)
            {
                return NotFound();
            }

            var perfil = await _context.Perfil
                .Include(p => p.utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perfil == null)
            {
                return NotFound();
            }

            return View(perfil);
        }

        // POST: Perfils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Perfil == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Perfil'  is null.");
            }
            var perfil = await _context.Perfil.FindAsync(id);
            if (perfil != null)
            {
                _context.Perfil.Remove(perfil);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerfilExists(int id)
        {
          return _context.Perfil.Any(e => e.Id == id);
        }
    }
}
