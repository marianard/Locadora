using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Locadora.Data;
using Locadora.Models;

namespace Locadora.Controllers
{
    public class FilmeController : Controller
    {
        private readonly LocadoraContext _context;

        public FilmeController(LocadoraContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todos os filmes
        /// </summary>
        /// <returns>Retorna uma view com todos os filmes</returns>
        public async Task<IActionResult> Index()
        {
            var locadoraContext = _context.Filme.Include(f => f.Genero);
            return View(await locadoraContext.ToListAsync());
        }

        /// <summary>
        /// Mostra os detalhes do filme
        /// </summary>
        /// <param name="id">Id do filme para ver os detalhes</param>
        /// <returns>Retorna uma view com o filme</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filme = await _context.Filme
                .Include(f => f.Genero)
                .FirstOrDefaultAsync(m => m.FilmeId == id);
            if (filme == null)
            {
                return NotFound();
            }

            return View(filme);
        }

        /// <summary>
        /// Retorna a pagina de criação do filme
        /// </summary>
        /// <returns>Retorna a view de criação do filme</returns>
        public IActionResult Create()
        {
            ViewData["GeneroId"] = new SelectList(_context.Genero, "GeneroId", "Nome");
            return View();
        }

         /// <summary>
         /// Recebe o novo filme para ser persistido.
         /// </summary>
         /// <param name="filme">Detalhes do filme que vai ser criado </param>
         /// <returns>Retorna para a index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FilmeId,Nome,DataCriacao,Ativo,GeneroId")] Filme filme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeneroId"] = new SelectList(_context.Genero, "GeneroId", "Nome", filme.GeneroId);
            return View(filme);
        }

        /// <summary>
        /// Retorna os dados para serem editados
        /// </summary>
        /// <param name="id">Id do filme para ser editado</param>
        /// <returns>Retorna uma view com o filme a ser editado</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filme = await _context.Filme.FindAsync(id);
            if (filme == null)
            {
                return NotFound();
            }
            ViewData["GeneroId"] = new SelectList(_context.Genero, "GeneroId", "Nome", filme.GeneroId);
            return View(filme);
        }


        /// <summary>
        /// Edita o filme
        /// </summary>
        /// <param name="id">O id do filme que vai ser editado</param>
        /// <returns>Retorna para a index </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FilmeId,Nome,DataCriacao,Ativo,GeneroId")] Filme filme)
        {
            if (id != filme.FilmeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmeExists(filme.FilmeId))
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
            ViewData["GeneroId"] = new SelectList(_context.Genero, "GeneroId", "Nome", filme.GeneroId);
            return View(filme);
        }

        /// <summary>
        /// Retorna os dados para serem excluidos
        /// </summary>
        /// <param name="id">Id do filme para ser excluido</param>
        /// <returns>Retorna uma view com o filme a ser excluido</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filme = await _context.Filme
                .Include(f => f.Genero)
                .FirstOrDefaultAsync(m => m.FilmeId == id);
            if (filme == null)
            {
                return NotFound();
            }

            return View(filme);
        }

        /// <summary>
        /// Exclui o filme
        /// </summary>
        /// <param name="id">O id do filme que vai ser excluido</param>
        /// <returns>Retorna para a index </returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filme = await _context.Filme.FindAsync(id);
            _context.Filme.Remove(filme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmeExists(int id)
        {
            return _context.Filme.Any(e => e.FilmeId == id);
        }
    }
}
