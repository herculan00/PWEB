using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PWEB.Data;
using PWEB.Models;

namespace PWEB.Controllers
{
    public class AvaliacaosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Utilizador> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AvaliacaosController(ApplicationDbContext context
            , UserManager<Utilizador> userManager
            , RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: Avaliacaos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Avaliacoes.Include(a => a.Cliente);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Avaliacaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Avaliacoes == null)
            {
                return NotFound();
            }

            var avaliacao = await _context.Avaliacoes
                .Include(a => a.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (avaliacao == null)
            {
                return NotFound();
            }

            return View(avaliacao);
        }

        // GET: Avaliacaos/Create
        public async Task<IActionResult> Create(int rId)
        {

            if (_context.Reserva == null)
            {
                return NotFound();
            }
            var reservas = await _context.Reserva.
                Include(r => r.Avaliacao).
                Include(r => r.Entrega).
                Include(r => r.Recolha).
                Include(r => r.Veiculo).
                Include(r => r.empresa).
                Include(r => r.EmpregadoCliente).
                FirstOrDefaultAsync(r => r.Id == rId);

            if (reservas == null)
            {
                return NotFound();
            }

            // encontrar os cliente associado a reserva
            string cId="";

            foreach (var c in reservas.EmpregadoCliente)
            {
                // so me intressa o cliente e não o funcionario que tem um EmpresaId != null
                if (c.EmpresaId == null)
                {
                    cId = c.Id;
                }
            }

            
            ViewData["ClienteId"] = new SelectList(_context.Users, "Id", "Id",cId);
            ViewData["rId"] = rId;

            return View();
        }

        // POST: Avaliacaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int rId, [Bind("Id,Valor,TempoLevantamento,LimpezaCarro,FacilidadeEncontrar,Prestabilidade,VelocidadeDevolucao,CondicaoCarro,ClienteId")] Avaliacao avaliacao)
        {
            ModelState.Remove(nameof(avaliacao.Cliente));
            if (ModelState.IsValid)
            {
                // actulizar o valor da avaliação com base nos parametros

                avaliacao.Valor = (avaliacao.TempoLevantamento + avaliacao.FacilidadeEncontrar +
                    avaliacao.LimpezaCarro + avaliacao.Prestabilidade + avaliacao.VelocidadeDevolucao +
                    avaliacao.CondicaoCarro) / 6.0;

                // primeiro adicionar avaliacao a BD e actualizar
                _context.Add(avaliacao);
                await _context.SaveChangesAsync();

                // adicionar a avaliacao a reseverva
                if (_context.Reserva == null || _context.Empresas == null)
                {
                    return NotFound();
                }

                // actualizar a reserva com a avaliacao e adicionar a BD
                var r = await _context.Reserva
                .Include(r => r.Avaliacao).
                Include(r => r.Entrega).
                Include(r => r.Recolha).
                Include(r => r.Veiculo).
                Include(r => r.empresa).
                Include(r => r.EmpregadoCliente).
                FirstOrDefaultAsync(r => r.Id == rId);
                r.AvaliacaoId = avaliacao.Id;

                _context.Update(r);
                await _context.SaveChangesAsync();

                var e = await _context.Empresas
                     .Include(e => e.Empregados)
                     .Include(e => e.Veiculos)
                     .Include(e => e.Subscricoes)
                     .Include(e => e.Avaliacoes)
                     .Include(e => e.Reservas)
                     .FirstOrDefaultAsync(e => e.Id == r.EmpresaId);

                e.Reservas.Add(r);

                _context.Update(e);

                // por fin guardar as akteraçoes na BD
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Users, "Id", "Id", avaliacao.ClienteId);
            ViewData["rId"] = rId;
            return View(avaliacao);
        }

        // GET: Avaliacaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Avaliacoes == null)
            {
                return NotFound();
            }

            var avaliacao = await _context.Avaliacoes.FindAsync(id);
            if (avaliacao == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Users, "Id", "Id", avaliacao.ClienteId);
            return View(avaliacao);
        }

        // POST: Avaliacaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Valor,TempoLevantamento,LimpezaCarro,FacilidadeEncontrar,Prestabilidade,VelocidadeDevolucao,CondicaoCarro,ClienteId")] Avaliacao avaliacao)
        {
            if (id != avaliacao.Id)
            {
                return NotFound();
            }

            ModelState.Remove(nameof(avaliacao.Cliente));
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avaliacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvaliacaoExists(avaliacao.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Users, "Id", "Id", avaliacao.ClienteId);
            return View(avaliacao);
        }

        // GET: Avaliacaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Avaliacoes == null)
            {
                return NotFound();
            }

            var avaliacao = await _context.Avaliacoes
                .Include(a => a.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (avaliacao == null)
            {
                return NotFound();
            }

            return View(avaliacao);
        }

        // POST: Avaliacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Avaliacoes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Avaliacoes'  is null.");
            }
            var avaliacao = await _context.Avaliacoes.FindAsync(id);
            if (avaliacao != null)
            {
                _context.Avaliacoes.Remove(avaliacao);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvaliacaoExists(int id)
        {
          return _context.Avaliacoes.Any(e => e.Id == id);
        }
    }
}
