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
    public class GeneroController : Controller
    {
        private readonly LocadoraContext _context;

        public GeneroController(LocadoraContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todos os generos
        /// </summary>
        /// <returns>Retorna uma view com todos os generos</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Genero.ToListAsync());
        }

        /// <summary>
        /// Mostra os detalhes do genero
        /// </summary>
        /// <param name="id">Id do genero para ver os detalhes</param>
        /// <returns>Retorna uma view com o genero</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genero = await _context.Genero
                .FirstOrDefaultAsync(m => m.GeneroId == id);
            if (genero == null)
            {
                return NotFound();
            }

            return View(genero);
        }

        /// <summary>
        /// Retorna a pagina de criação do genero
        /// </summary>
        /// <returns>Retorna a view de criação do genero</returns>
        public IActionResult Create()
        {
            return View();
        }


        /// <summary>
        /// Recebe o novo genero para ser persistido.
        /// </summary>
        /// <param name="genero">Detalhes do genero que vai ser criado </param>
        /// <returns>Retorna para a index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GeneroId,Nome,DataCriacao,Ativo")] Genero genero)
        {
            if (ModelState.IsValid)
            {
                _context.Add(genero);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genero);
        }

        /// <summary>
        /// Retorna os dados para serem editados
        /// </summary>
        /// <param name="id">Id do genero para ser editado</param>
        /// <returns>Retorna uma view com o genero a ser editado</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genero = await _context.Genero.FindAsync(id);
            if (genero == null)
            {
                return NotFound();
            }
            return View(genero);
        }

        /// <summary>
        /// Edita o genero
        /// </summary>
        /// <param name="id">O id do genero que vai ser editado</param>
        /// <returns>Retorna para a index </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GeneroId,Nome,DataCriacao,Ativo")] Genero genero)
        {
            if (id != genero.GeneroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genero);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GeneroExists(genero.GeneroId))
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
            return View(genero);
        }

        /// <summary>
        /// Retorna os dados para serem excluidos
        /// </summary>
        /// <param name="id">Id do genero para ser excluido</param>
        /// <returns>Retorna uma view com o genero a ser excluido</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genero = await _context.Genero
                .FirstOrDefaultAsync(m => m.GeneroId == id);
            if (genero == null)
            {
                return NotFound();
            }

            return View(genero);
        }

        /// <summary>
        /// Exclui o genero
        /// </summary>
        /// <param name="id">O id do genero que vai ser excluido</param>
        /// <returns>Retorna para a index </returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genero = await _context.Genero.FindAsync(id);
            _context.Genero.Remove(genero);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GeneroExists(int id)
        {
            return _context.Genero.Any(e => e.GeneroId == id);
        }
    }
}
