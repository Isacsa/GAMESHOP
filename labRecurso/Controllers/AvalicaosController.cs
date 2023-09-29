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
    public class AvalicaosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AvalicaosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Avalicaos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Avalicao.Include(a => a.Jogo);
            return View(await applicationDbContext.ToListAsync());
        }



        public IActionResult Avaliar(int? id)
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
        public async Task<IActionResult> Avaliar(int id, [Bind("Name,pontuacao")] Avalicao avalicao)
        {
            avalicao.JogoId = id;
            avalicao.Jogo = _context.Jogos.SingleOrDefault(j => j.Id == id);
            if (!_context.Avalicao.Where(a => a.JogoId == avalicao.JogoId).Any(a => a.Name == User.Identity.Name))
            {
                if (ModelState.IsValid)
                {
                    _context.Add(avalicao);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Jogos", new { id = avalicao.JogoId });
                }
            }
            else
            {
                ModelState.AddModelError("Name", "Já existe uma avaliação sua");
            }

            ViewData["JogoId"] = id;
            return View(avalicao);

        }


    }
}
