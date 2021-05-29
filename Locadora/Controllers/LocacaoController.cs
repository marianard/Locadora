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
    public class LocacaoController : Controller
    {
        private readonly LocadoraContext _context;

        public LocacaoController(LocadoraContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todos as locações
        /// </summary>
        /// <returns>Retorna uma view com todas as locações</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Locacao.ToListAsync());
        }

        /// <summary>
        /// Mostra os detalhes das locações
        /// </summary>
        /// <param name="id">Id da locação para ver os detalhes</param>
        /// <returns>Retorna uma view com a locação</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locacao = await _context.Locacao
                .FirstOrDefaultAsync(m => m.LocacaoId == id);
            if (locacao == null)
            {
                return NotFound();
            }

            var listaFilme = await _context.Locacao
                .Where(m => m.LocacaoId == id)
                .SelectMany(m => m.ListaFilme)
                .ToListAsync();

            ViewData["ListaFilme"] = listaFilme;

            return View(locacao);
        }

        /// <summary>
        /// Retorna a pagina de criação das locação
        /// </summary>
        /// <returns>Retorna a view de criação da locação</returns>
        public IActionResult Create()
        {
            ViewData["FilmeId"] = new SelectList(_context.Filme, "FilmeId", "Nome");
            return View();
        }

        /// <summary>
        /// Recebe a nova locação para ser persistida.
        /// </summary>
        /// <param name="locacao">Detalhes da locação que vai ser criada</param>
        /// <returns>Retorna para a index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocacaoId,CpfCliente,DataLocacao")] Locacao locacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(locacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           

            return View(locacao);
        }

        /// <summary>
        /// Retorna os dados para serem editados
        /// </summary>
        /// <param name="id">Id da locação a ser editada</param>
        /// <returns>Retorna uma view com a locação a ser editada</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locacao = await _context.Locacao.FindAsync(id);
            if (locacao == null)
            {
                return NotFound();
            }

            var listaFilme = await _context.Locacao
                .Where(m => m.LocacaoId == id)
                .SelectMany(m => m.ListaFilme)
                .ToListAsync();

            ViewData["ListaFilme"] = listaFilme;

            return View(locacao);
        }

        /// <summary>
        /// Edita a locação
        /// </summary>
        /// <param name="id">O id da locação a ser editada</param>
        /// <returns>Retorna para a index </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LocacaoId,CpfCliente,DataLocacao")] Locacao locacao)
        {
            if (id != locacao.LocacaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocacaoExists(locacao.LocacaoId))
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
            return View(locacao);
        }

        /// <summary>
        /// Retorna os dados para serem excluidos
        /// </summary>
        /// <param name="id">Id da locação para ser excluido</param>
        /// <returns>Retorna uma view com a locação a ser excluida</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locacao = await _context.Locacao
                .FirstOrDefaultAsync(m => m.LocacaoId == id);
            if (locacao == null)
            {
                return NotFound();
            }

            return View(locacao);
        }

        /// <summary>
        /// Exclui a locação
        /// </summary>
        /// <param name="id">O id da locação que vai ser excluida</param>
        /// <returns>Retorna para a index </returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var locacao = await _context.Locacao.FindAsync(id);
            _context.Locacao.Remove(locacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocacaoExists(int id)
        {
            return _context.Locacao.Any(e => e.LocacaoId == id);
        }
    }
}
