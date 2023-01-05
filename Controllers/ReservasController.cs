using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PWEB.Data;
using PWEB.Models;
using PWEB.ViewModel;

namespace PWEB.Controllers
{
    public class ReservasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Utilizador> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public ReservasController(ApplicationDbContext context
            , UserManager<Utilizador> userManager
            , RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: Reservas
        [Authorize(Roles ="Admin,Funcionario,Gestor")]
        public async Task<IActionResult> Index()
        {
            if ( _context.Reserva == null)
            {
                return NotFound();
            }
            var applicationDbContext = _context.Reserva.
                Include(r => r.Avaliacao).
                Include(r => r.Entrega).
                Include(r => r.Recolha).
                Include(r => r.Veiculo).
                Include(r => r.empresa);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = "Admin,Funcionario,Gestor,Cliente")]
        public  IActionResult Criar()
        {
            return RedirectToAction("Create");
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reserva == null || _context.Empresas == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .Include(r => r.Avaliacao)
                .Include(r => r.Entrega)
                .Include(r => r.Recolha)
                .Include(r => r.Veiculo)
                .Include(r => r.empresa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        public IActionResult Create()
        {
            ViewData["AvaliacaoId"] = new SelectList(_context.Avaliacoes, "Id", "Id");
            ViewData["EntregaId"] = new SelectList(_context.Entregas, "Id", "Id");
            ViewData["RecolhaId"] = new SelectList(_context.Recolhas, "Id", "Id");
            ViewData["VeiculoId"] = new SelectList(_context.Veiculos, "Id", "Id");
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataDeLevantaneto,DataDeEntrega,Eliminar,VeiculoId,EntregaId,RecolhaId,AvaliacaoId,EmpresaId")] Reserva reserva)
        {

            if ( _context.Empresas == null || _context.Veiculos == null)
            {
                return NotFound();
            }

            ModelState.Remove(nameof(reserva.Avaliacao));
            ModelState.Remove(nameof(reserva.Entrega));
            ModelState.Remove(nameof(reserva.Recolha));
            ModelState.Remove(nameof(reserva.Veiculo));
            ModelState.Remove(nameof(reserva.empresa));
            ModelState.Remove(nameof(reserva.EmpregadoCliente));

            if (ModelState.IsValid)
            {

                if (reserva.DataDeEntrega.CompareTo(reserva.DataDeLevantaneto) <= 0)
                {
                    ErroViewModel e = new ErroViewModel();
                    e.Mensagem = "A data de entrega não pode ser anterior á data de levantamento!";
                    e.Controller = "Reservas";

                    return View("Erro", e);
                }

                reserva.EmpregadoCliente = new List<Utilizador>();

                // adicionar o utilizador que esta a realizar a reserva á reserva
                var userCliente= await _userManager.GetUserAsync(User);
                if (userCliente == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

                }
                // ver a que empresa pertence o veiculo
                var empVeic = await _context.Veiculos.Where(v=>v.Id == reserva.VeiculoId).FirstOrDefaultAsync();
                reserva.EmpresaId = empVeic.EmpresaId;

                // adicionar a reserlva a empresa do veiculo
                var rEmp = await _context.Empresas.Where(e => e.Id == empVeic.EmpresaId).Include(e=>e.Reservas).FirstOrDefaultAsync();
                rEmp.Reservas.Add(reserva);

                reserva.EmpregadoCliente.Add(userCliente);
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AvaliacaoId"] = new SelectList(_context.Avaliacoes, "Id", "Id", reserva.AvaliacaoId);
            ViewData["EntregaId"] = new SelectList(_context.Entregas, "Id", "Id", reserva.EntregaId);
            ViewData["RecolhaId"] = new SelectList(_context.Recolhas, "Id", "Id", reserva.RecolhaId);
            ViewData["VeiculoId"] = new SelectList(_context.Veiculos, "Id", "Id", reserva.VeiculoId);
            return View(reserva);
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reserva == null || _context.Empresas == null || _context.Veiculos == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            var rEmpresa =await _context.Empresas.Where(e => e.Reservas.Any(r => r.Id == reserva.Id)).Include(e => e.Reservas).FirstOrDefaultAsync();
 

            ViewData["AvaliacaoId"] = new SelectList(_context.Avaliacoes, "Id", "Id", reserva.AvaliacaoId);
            ViewData["EntregaId"] = new SelectList(_context.Entregas, "Id", "Id", reserva.EntregaId);
            ViewData["RecolhaId"] = new SelectList(_context.Recolhas, "Id", "Id", reserva.RecolhaId);

                                   
            ViewData["VeiculoId"] = new SelectList(_context.Veiculos.Where(v => v.EmpresaId == rEmpresa.Id && v.Disponivel == true).Include(v => v.empresa), "Id", "Id", reserva.VeiculoId);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataDeLevantaneto,DataDeEntrega,Eliminar,VeiculoId,EntregaId,RecolhaId,AvaliacaoId,EmpresaId")] Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            ModelState.Remove(nameof(reserva.Avaliacao));
            ModelState.Remove(nameof(reserva.Entrega));
            ModelState.Remove(nameof(reserva.Recolha));
            ModelState.Remove(nameof(reserva.Veiculo));
            ModelState.Remove(nameof(reserva.empresa));
            ModelState.Remove(nameof(reserva.EmpregadoCliente));

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.Id))
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
            ViewData["AvaliacaoId"] = new SelectList(_context.Avaliacoes, "Id", "Id", reserva.AvaliacaoId);
            ViewData["EntregaId"] = new SelectList(_context.Entregas, "Id", "Id", reserva.EntregaId);
            ViewData["RecolhaId"] = new SelectList(_context.Recolhas, "Id", "Id", reserva.RecolhaId);
            ViewData["VeiculoId"] = new SelectList(_context.Veiculos, "Id", "Id", reserva.VeiculoId);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reserva == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .Include(r => r.Avaliacao)
                .Include(r => r.Entrega)
                .Include(r => r.Recolha)
                .Include(r => r.Veiculo)
                .Include(r => r.empresa)           
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }
        
            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reserva == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Reserva'  is null.");
            }
            var reserva = await _context.Reserva
                .Include(r => r.Avaliacao)
                .Include(r => r.Entrega)
                .Include(r => r.Recolha)
                .Include(r => r.Veiculo)
                .Include(r => r.empresa)
                .Include(r => r.EmpregadoCliente)
                .FirstAsync(m => m.Id == id);

            if (reserva != null)
            {
                reserva.EmpregadoCliente.Clear();
                _context.Reserva.Remove(reserva);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
          return _context.Reserva.Any(e => e.Id == id);
        }

        // GET: Reservas/Aceitar/5
        public async Task<IActionResult> Aceitar(int? id)
        {
            if (id == null || _context.Reserva == null || _context.Empresas == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .Include(r => r.Avaliacao)
                .Include(r => r.Entrega)
                .Include(r => r.Recolha)
                .Include(r => r.Veiculo)
                .Include(r => r.empresa)
                .Include(r => r.EmpregadoCliente)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (reserva == null)
            {
                return NotFound();
            }

            if (reserva.EmpregadoCliente.Count() >= 2)
            {
                ErroViewModel e = new ErroViewModel();
                e.Mensagem = "Esta reserva já está atribuida!";
                e.Controller = "Reservas";

                return View("Erro", e);
            }

            return View(reserva);
        }

        // POST: Reservas/Aceitar/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Aceitar(int id, [Bind("Id,DataDeLevantaneto,DataDeEntrega,Eliminar,VeiculoId,EntregaId,RecolhaId,AvaliacaoId,EmpresaId")] Reserva reserva)
        {
            if (id != reserva.Id || _context.Reserva == null)
            {
                return NotFound();
            }

            // necessario para aceder a lista de EmoregadoCliente
            var fReserva = await _context.Reserva
                .Include(r => r.Avaliacao)
                .Include(r => r.Entrega)
                .Include(r => r.Recolha)
                .Include(r => r.Veiculo)
                .Include(r => r.empresa)
                .Include(r => r.EmpregadoCliente)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (fReserva == null)
            {
                return NotFound();
            }

            ModelState.Remove(nameof(reserva.Avaliacao));
            ModelState.Remove(nameof(reserva.Entrega));
            ModelState.Remove(nameof(reserva.Recolha));
            ModelState.Remove(nameof(reserva.Veiculo));
            ModelState.Remove(nameof(reserva.empresa));
            ModelState.Remove(nameof(reserva.EmpregadoCliente));

            if (ModelState.IsValid)
            {
                try
                {
                    // adicionar o funcionario que esta a acitar a reserva 
                    var userFunci = await _userManager.GetUserAsync(User);
                    if (userFunci == null)
                    {
                        return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

                    }
                    var array = fReserva.EmpregadoCliente;

                    fReserva.EmpregadoCliente.Add(userFunci);

                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.Id))
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

            return View(reserva);
        }
    }
}
