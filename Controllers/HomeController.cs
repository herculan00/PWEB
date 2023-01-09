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
        public async Task<IActionResult> Create([Bind("Localizacao,TipoId,Levantamento,Entrega")] InputInicial ip)
        {
            if (_context.Veiculos == null || _context.Empresas == null || _context.Reserva == null)
            {
                return NotFound();
            }

            var rs = await _context.Reserva.
                 Include(r => r.Avaliacao).
                 Include(r => r.Entrega).
                 Include(r => r.Recolha).
                 Include(r => r.Veiculo).
                 Include(r => r.empresa).ToArrayAsync();

            var es = await _context.Empresas
                .Include(e => e.Empregados)
                .Include(e => e.Veiculos)
                .Include(e => e.Subscricoes)
                .Include(e => e.Avaliacoes)
                .Include(e => e.Reservas).ToArrayAsync();


            ViewData["rs"] = rs;
            ViewData["es"] = es;

            if (_context.Empresas == null || _context.Veiculos == null)
            {
                return NotFound();
            }

            List<Veiculo> vArray = new List<Veiculo>();

            if (ModelState.IsValid)
            {
                // Primeiro encontra os veiculos que se encotram na localização desejada

                var c = await _context.Veiculos
                    .Include(v => v.Tipo)
                    .Include(v => v.empresa)
                    .Where(v => v.Localização.Contains(ip.Localizacao) && v.Tipo.Id == ip.TipoId)
                    .ToListAsync();

                if (c.Count == 0)
                {
                                
                    ErroViewModel e = new ErroViewModel();
                    e.Mensagem = "Não existem veiculos disponiveis com essas caracteristicas";
                    e.Controller = "Home";

                    return View("Erro", e);

                }
     
                // Verificar se eles estao disponiveis na empresa na data desejada pelo utilizador 

                foreach (var carro in c)
                {
                    var emp = await _context.Empresas
                     .Include(e => e.Empregados)
                     .Include(e => e.Veiculos)
                     .Include(e => e.Subscricoes)
                     .Include(e => e.Avaliacoes)
                     .Include(e => e.Reservas)
                     .FirstOrDefaultAsync(e => e.Id == carro.EmpresaId);

                    foreach (var r in emp.Reservas)
                    {
                        if (r.VeiculoId == carro.Id)
                        {
                            // se a data pedida for antes da reserva existe te entao é valido
                            if (ip.Entrega.CompareTo(r.DataDeLevantaneto) < 0)
                            {
                                if (!vArray.Contains(carro))
                                {
                                    vArray.Add(carro);
                                }
                            }
                            // se a data pedida for depois da reserva existe te entao é valido
                            if (ip.Levantamento.CompareTo(r.DataDeEntrega) > 0)
                            {
                                if (!vArray.Contains(carro))
                                {
                                    vArray.Add(carro);
                                }
                            }

                        }
                    }
                }


            }

            if (vArray.Count == 0)
            {

                ErroViewModel e = new ErroViewModel();
                e.Mensagem = "Não existem veiculos disponiveis com essas caracteristicas";
                e.Controller = "Home";

                return View("Erro", e);

            }

            ViewData["tipo"] = new SelectList(_context.TiposVeis, "Id", "Name");
            ViewData["lVeiculos"] = vArray;
            return RedirectToAction("Criar", "Reservas");
        }

      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}