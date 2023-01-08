using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PWEB.Data;
using PWEB.Models;
using PWEB.ViewModel;

namespace PWEB.Controllers
{
    public class EntregasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Utilizador> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public EntregasController(ApplicationDbContext context
            , UserManager<Utilizador> userManager
            , RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
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
        public async Task<IActionResult> Create(int? id ,string rId, string? fId)
        {
            if ( _context.Reserva == null)
            {
                return NotFound();
            }

            
            var user = await _userManager.GetUserAsync(User);
            var reserva = await _context.Reserva
                .Include(r => r.EmpregadoCliente)
                .FirstOrDefaultAsync(r => r.Id == Int32.Parse(rId));

            if (fId == null)
            {         
                fId = user.Id;
            }

            // Verificar se a reserga ja tem uma entrega
            if (reserva.EntregaId != null)
            {
                ErroViewModel e = new ErroViewModel();
                e.Mensagem = "A reserva ja esta entrege";
                e.Controller = "Reservas";

                return View("Erro", e);
            }

            // verificar se a reserva ja foi confirmada 
            if (reserva.Confirmado != true)
            {
                ErroViewModel e = new ErroViewModel();
                e.Mensagem = "A reserva ainda não foi confirmada";
                e.Controller = "Reservas";

                return View("Erro", e);
            }

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

                // primeiro adicionar entrega a BD e actualizar
                _context.Add(entrega);
               await _context.SaveChangesAsync();

                // adicionar a entrega a reseverva
                if (_context.Reserva == null)
                {
                    return NotFound();
                }
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
            ViewData["Reserva"] = rId;
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
