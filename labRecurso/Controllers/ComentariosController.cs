using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using labRecurso.Data;
using labRecurso.Models;

namespace labRecurso.Controllers
{
    public class ComentariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComentariosController(ApplicationDbContext context)
        {
            _context = context;
        }



        public IActionResult Comentar(int? id)
        {

            if (_context.Jogos.Any(j => j.Id == id) && User.Identity.IsAuthenticated)
            {
                ViewData["JogoId"] = id;
                return View();
            }
            else
            {
                return NotFound();
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comentar(int id, [Bind("Name,Message")] Comentario comentario)
        {
            comentario.JogoId = id;
            comentario.Jogo = _context.Jogos.SingleOrDefault(j => j.Id == id);

            if (ModelState.IsValid)
            {
                _context.Add(comentario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Jogos", new { id = comentario.JogoId });
            }
            ViewData["JogoId"] = id;
            return View(comentario);
        }
    }
}
