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

namespace PWEB.Controllers
{
    public class EmpresasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Utilizador> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public EmpresasController(ApplicationDbContext context
            , UserManager<Utilizador> userManager
            , RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: Empresas
        public async Task<IActionResult> Index()
        {
              return View(await _context.Empresas.ToListAsync());
        }

        // GET: Empresas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Empresas == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // GET: Empresas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empresas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Disponivel,Eliminar")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                //inicializar 

                empresa.Empregados = new List<Utilizador>();
                empresa.Veiculos= new List<Veiculo>();
                empresa.Reservas = new List<Reserva>();
                empresa.Avaliacoes = new List<Avaliacao>();
                empresa.Subscricoes = new List<Subscricao>();

                // criar o gestor

                var defaultUser = new Utilizador
                {
                    UserName = "gestor"+empresa.Nome + "@localhost.com",
                    Email = "gestor" + empresa.Nome + "@localhost.com",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    Nome = "Gestor",
                    Apelido = "Gestor",
                    NIF = 0,
                    Morada = "",
                    DataNascimento = new DateTime(),
                    NumeroCartaoMultibanco = 0,
                    ValidadeCartaoMultibanco = new DateTime(),
                    CvdCartaoMultibanco = 0,
                    Disponivel = true

                };
                //Adicionar o gestor a empresa

                empresa.Empregados.Add(defaultUser);

                // adicionar pass e role de gestor
                var user = await _userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await _userManager.CreateAsync(defaultUser, "Gestor10.");
                    await _userManager.AddToRoleAsync(defaultUser,
                    Roles.Gestor.ToString());
                }

                _context.Add(empresa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empresa);
        }

        // GET: Empresas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Empresas == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }
            return View(empresa);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Disponivel,Eliminar")] Empresa empresa)
        {
            if (id != empresa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empresa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresaExists(empresa.Id))
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
            return View(empresa);
        }

        // GET: Empresas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Empresas == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Empresas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Empresas'  is null.");
            }
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa != null)
            {
                if (empresa.Veiculos == null )
                {
                    // remover os funcionarios antes de remover a empresa 
                    var utizadores =  _userManager.Users.Where(u => u.EmpresaId == id);
                    foreach (var item in utizadores)
                    {

                        _context.Users.Remove(item);
                    }
                       
                    _context.Empresas.Remove(empresa);
 
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                // nao se pode remover empresa se esta possui veiculos 
                if (empresa.Veiculos.Count() != 0)
                {
                    return Problem("Nao e possivel remover empresa, pois esta possui veiculos. ");
                }
                _context.Empresas.Remove(empresa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpresaExists(int id)
        {
          return _context.Empresas.Any(e => e.Id == id);
        }
    }
}
