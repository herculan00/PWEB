using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PWEB.Data;
using PWEB.Helpers;
using PWEB.Models;
using PWEB.ViewModel;
using System.Diagnostics;

namespace PWEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Utilizador> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context
            , UserManager<Utilizador> userManager
            , RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewData["tipo"]= new SelectList(_context.TiposVeis, "Id", "Name");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Localizacao,Tipo,Levantamento,Entrega")] InputInicial ip)
        {

            if (_context.Empresas == null || _context.Veiculos == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                // Primeiro encontra os veiculos que se encotram na localização desejada

                var c = await _context.Veiculos
                    .Include(v => v.Tipo)
                    .Include(v => v.empresa)
                    .Where(v => v.Localização.Contains(ip.Localizacao))
                    .ToListAsync();

                // Verificar se eles estao disponiveis na data desejada pelo utilizador 

                var e = await _context.Empresas.ToListAsync();

                var x = 45;

                //if (reserva.DataDeEntrega.CompareTo(reserva.DataDeLevantaneto) <= 0)
                //{
                //    ErroViewModel e = new ErroViewModel();
                //    e.Mensagem = "A data de entrega não pode ser anterior á data de levantamento!";
                //    e.Controller = "Reservas";

                //    return View("Erro", e);
                //}
            }

            //ViewData["AvaliacaoId"] = new SelectList(_context.Avaliacoes, "Id", "Id", reserva.AvaliacaoId);
            //ViewData["EntregaId"] = new SelectList(_context.Entregas, "Id", "Id", reserva.EntregaId);
            //ViewData["RecolhaId"] = new SelectList(_context.Recolhas, "Id", "Id", reserva.RecolhaId);
            //ViewData["VeiculoId"] = new SelectList(_context.Veiculos, "Id", "Id", reserva.VeiculoId);

            ViewData["tipo"] = new SelectList(_context.TiposVeis, "Id", "Name");

            return View(ip);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}