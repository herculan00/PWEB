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
    public class EntregasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EntregasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Entregas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Entregas.Include(e => e.Empregado);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Entregas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Entregas == null)
            {
                return NotFound();
            }

            var entrega = await _context.Entregas
                .Include(e => e.Empregado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entrega == null)
            {
                return NotFound();
            }

            return View(entrega);
        }

        // GET: Entregas/Create
        public IActionResult Create(int? id ,string? rId, string? fId)
        {
            ViewData["EmpregadoId"] = new SelectList(_context.Users, "Id", "Email",fId);
            ViewData["Reserva"] = rId;

            return View();
        }

        // POST: Entregas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int rId,[Bind("Id,Kilometros,Danos,Observaçoes,EmpregadoId")] Entrega entrega)
        {

            ModelState.Remove(nameof(entrega.Empregado));
            if (ModelState.IsValid)
            {
                // adicionar a entrega a reseverva
                if (_context.Reserva == null)
                {
                    return NotFound();
                }

                // primeiro adicionar entrega a BD e actualizar
                _context.Add(entrega);
               await _context.SaveChangesAsync();

                // actualizar a reserva com a entrega e adicionar a BD
                var r = await _context.Reserva
                .Include(r => r.Avaliacao).
                Include(r => r.Entrega).
                Include(r => r.Recolha).
                Include(r => r.Veiculo).
                Include(r => r.empresa).
                Include(r => r.EmpregadoCliente).
                FirstOrDefaultAsync(r => r.Id == rId);
                r.EntregaId = entrega.Id;

                _context.Update(r);

                // por fin guardar as akteraçoes na BD
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpregadoId"] = new SelectList(_context.Users, "Id", "Email", entrega.EmpregadoId);
            return View(entrega);
        }

        // GET: Entregas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Entregas == null)
            {
                return NotFound();
            }

            var entrega = await _context.Entregas.FindAsync(id);
            if (entrega == null)
            {
                return NotFound();
            }
            ViewData["EmpregadoId"] = new SelectList(_context.Users, "Id", "Email", entrega.EmpregadoId);
            return View(entrega);
        }

        // POST: Entregas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Kilometros,Danos,Observaçoes,EmpregadoId")] Entrega entrega)
        {
            if (id != entrega.Id)
            {
                return NotFound();
            }

            ModelState.Remove(nameof(entrega.Empregado));
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entrega);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntregaExists(entrega.Id))
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
            ViewData["EmpregadoId"] = new SelectList(_context.Users, "Id", "Email", entrega.EmpregadoId);
            return View(entrega);
        }

        // GET: Entregas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Entregas == null)
            {
                return NotFound();
            }

            var entrega = await _context.Entregas
                .Include(e => e.Empregado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entrega == null)
            {
                return NotFound();
            }

            return View(entrega);
        }

        // POST: Entregas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Entregas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Entregas'  is null.");
            }
            var entrega = await _context.Entregas.FindAsync(id);
            if (entrega != null)
            {
                _context.Entregas.Remove(entrega);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntregaExists(int id)
        {
          return _context.Entregas.Any(e => e.Id == id);
        }
    }
}
