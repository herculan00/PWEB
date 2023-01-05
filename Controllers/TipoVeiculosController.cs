using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PWEB.Data;
using PWEB.Models;

namespace PWEB.Controllers
{
    public class TipoVeiculosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoVeiculosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TipoVeiculoes
        public async Task<IActionResult> Index()
        {
              return View(await _context.TiposVeis.ToListAsync());
        }

        // GET: TipoVeiculoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TiposVeis == null)
            {
                return NotFound();
            }

            var tipoVeiculo = await _context.TiposVeis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoVeiculo == null)
            {
                return NotFound();
            }

            return View(tipoVeiculo);
        }

        // GET: TipoVeiculoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoVeiculoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TipoVeiculo tipoVeiculo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoVeiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoVeiculo);
        }

        // GET: TipoVeiculoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TiposVeis == null)
            {
                return NotFound();
            }

            var tipoVeiculo = await _context.TiposVeis.FindAsync(id);
            if (tipoVeiculo == null)
            {
                return NotFound();
            }
            return View(tipoVeiculo);
        }

        // POST: TipoVeiculoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TipoVeiculo tipoVeiculo)
        {
            if (id != tipoVeiculo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoVeiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoVeiculoExists(tipoVeiculo.Id))
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
            return View(tipoVeiculo);
        }

        // GET: TipoVeiculoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TiposVeis == null)
            {
                return NotFound();
            }

            var tipoVeiculo = await _context.TiposVeis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoVeiculo == null)
            {
                return NotFound();
            }

            return View(tipoVeiculo);
        }

        // POST: TipoVeiculoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TiposVeis == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TiposVeis'  is null.");
            }
            var tipoVeiculo = await _context.TiposVeis.FindAsync(id);
            if (tipoVeiculo != null)
            {
                _context.TiposVeis.Remove(tipoVeiculo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoVeiculoExists(int id)
        {
          return _context.TiposVeis.Any(e => e.Id == id);
        }
    }
}
