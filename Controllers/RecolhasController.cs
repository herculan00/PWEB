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
    public class RecolhasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Utilizador> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public RecolhasController(ApplicationDbContext context
            , UserManager<Utilizador> userManager
            , RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: Recolhas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Recolhas.Include(r => r.Empregado);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Recolhas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recolhas == null)
            {
                return NotFound();
            }

            var recolha = await _context.Recolhas
                .Include(r => r.Empregado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recolha == null)
            {
                return NotFound();
            }

            return View(recolha);
        }

        // GET: Recolhas/Create
        public async Task<IActionResult> Create(int? id, string rId, string? fId)
        {

            if (_context.Reserva == null)
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

            // Verificar se a reserga ja tem uma recolha
            if (reserva.RecolhaId != null)
            {
                ErroViewModel e = new ErroViewModel();
                e.Mensagem = "A reserva ja foi recolhida";
                e.Controller = "Reservas";

                return View("Erro", e);
            }

            // Verificar se a reserga ja tem uma entrega
            if (reserva.EntregaId == null)
            {
                ErroViewModel e = new ErroViewModel();
                e.Mensagem = "Primeiro tem de ser realizada a entrega";
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

            ViewData["EmpregadoId"] = new SelectList(_context.Users, "Id", "Email", fId);
            ViewData["Reserva"] = rId;

            return View();
        }

        // POST: Recolhas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int rId, [Bind("Id,Kilometros,Danos,Observaçoes,EmpregadoId")] Recolha recolha)
        {
            ModelState.Remove(nameof(recolha.Empregado));
            if (ModelState.IsValid)
            {

                // adicionar a recolha a reseverva
                if (_context.Reserva == null)
                {
                    return NotFound();
                }

                // primeiro adicionar recolha a BD e actualizar
                _context.Add(recolha);
                await _context.SaveChangesAsync();

                // actualizar a reserva com a recolha e adicionar a BD
                var r = await _context.Reserva
                .Include(r => r.Avaliacao).
                Include(r => r.Entrega).
                Include(r => r.Recolha).
                Include(r => r.Veiculo).
                Include(r => r.empresa).
                Include(r => r.EmpregadoCliente).
                FirstOrDefaultAsync(r => r.Id == rId);
                r.RecolhaId = recolha.Id;

                _context.Update(r);

                // por fin guardar as akteraçoes na BD
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpregadoId"] = new SelectList(_context.Users, "Id", "Email", recolha.EmpregadoId);
            return View(recolha);
        }

        // GET: Recolhas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Recolhas == null)
            {
                return NotFound();
            }

            var recolha = await _context.Recolhas.FindAsync(id);
            if (recolha == null)
            {
                return NotFound();
            }
            ViewData["EmpregadoId"] = new SelectList(_context.Users, "Id", "Id", recolha.EmpregadoId);
            return View(recolha);
        }

        // POST: Recolhas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Kilometros,Danos,Observaçoes,EmpregadoId")] Recolha recolha)
        {
            if (id != recolha.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recolha);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecolhaExists(recolha.Id))
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
            ViewData["EmpregadoId"] = new SelectList(_context.Users, "Id", "Id", recolha.EmpregadoId);
            return View(recolha);
        }

        // GET: Recolhas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Recolhas == null)
            {
                return NotFound();
            }

            var recolha = await _context.Recolhas
                .Include(r => r.Empregado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recolha == null)
            {
                return NotFound();
            }

            return View(recolha);
        }

        // POST: Recolhas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Recolhas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Recolhas'  is null.");
            }
            var recolha = await _context.Recolhas.FindAsync(id);
            if (recolha != null)
            {
                _context.Recolhas.Remove(recolha);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecolhaExists(int id)
        {
          return _context.Recolhas.Any(e => e.Id == id);
        }
    }
}
