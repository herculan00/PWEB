using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PWEB.Data;
using PWEB.Models;
using PWEB.ViewModel;

namespace PWEB.Controllers
{
    public class UtilizadoresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Utilizador> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UtilizadoresController(ApplicationDbContext context
            , UserManager<Utilizador> userManager
            , RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

            }

            var userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles.Contains(Roles.Admin.ToString()))
            {
                return View(_userManager.Users.Include(e => e.empresa));
            }

            if (userRoles.Contains(Roles.Gestor.ToString()))
            {
                return View(_userManager.Users.Include(e => e.empresa).Where(u => u.EmpresaId == user.EmpresaId));
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: Utilizador/Edit/5
        public async Task<IActionResult> Edit(String? id)
        {
            if (id == null || _userManager.Users == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

            }

            var utilizador = await _userManager.FindByIdAsync(id);
            if (utilizador == null)
            {
                return NotFound();
            }

            // nao pode desativar  o seu registo
            if (user.Id == utilizador.Id)
            {
                ViewData["Desativar"] = true;
            }
            else { ViewData["Desativar"] = false; }


            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "Nome");
            return View(utilizador);
        }

        // POST: Utilizador/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(String id, [Bind("Id,Nome,Apelido,NIF,Morada,DataNascimento," +
            "NumeroCartaoMultibanco,ValidadeCartaoMultibanco,CvdCartaoMultibanco,Disponivel," +
            "Eliminar,EmpresaId,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed," +
            "PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed," +
            "TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Utilizador utilizador)
        {
            if (id != utilizador.Id)
            {
                return NotFound();
            }

            ModelState.Remove(nameof(utilizador.empresa));
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utilizador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(utilizador);
        }

        // GET: Utilizador/Details/5
        public async Task<IActionResult> Details(String? id)
        {
            if (id == null || _userManager.Users == null)
            {
                return NotFound();
            }

            var utilizador = await _userManager.FindByIdAsync(id);

            if (utilizador == null)
            {
                return NotFound();
            }



            return View(await _userManager.Users.Include(e => e.empresa).Where(u => u.Id == id).FirstOrDefaultAsync());
        }

        // GET: Utilizador/Create
        public IActionResult Create()
        {
            List<String> lista = new List<String>();
            lista.Add(Roles.Funcionario.ToString());
            lista.Add(Roles.Gestor.ToString());

            ViewData["RolesId"] = new SelectList(lista);
            return View();
        }

        // POST: Utilizador/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("role")] String role, [Bind("Id,Nome,Apelido,NIF,Morada,DataNascimento," +
            "NumeroCartaoMultibanco,ValidadeCartaoMultibanco,CvdCartaoMultibanco,Disponivel," +
            "Eliminar,Email")] Utilizador utilizador)
        {
            ModelState.Remove(nameof(utilizador.empresa));
            if (ModelState.IsValid)
            {
                var userLog = await _userManager.GetUserAsync(User);

                if (userLog == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

                }
                utilizador.UserName = utilizador.Email;
                utilizador.EmailConfirmed = true;
                utilizador.PhoneNumberConfirmed = true;
                utilizador.EmpresaId = userLog.EmpresaId;

                var user = await _userManager.FindByEmailAsync(utilizador.Email);
                if (user == null)
                {
                    await _userManager.CreateAsync(utilizador, "Util10.");
                    await _userManager.AddToRoleAsync(utilizador, role);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            List<String> lista = new List<String>();
            lista.Add(Roles.Funcionario.ToString());
            lista.Add(Roles.Gestor.ToString());

            ViewData["RolesId"] = new SelectList(lista);
            return View(utilizador);
        }

        // GET: Utilizador/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _userManager.Users == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

            }

            var utilizador = await _userManager.FindByIdAsync(id);
            if (utilizador == null)
            {
                return NotFound();
            }

            // nao pode apagar o seu registo
            if (user.Id == utilizador.Id)
            {
                ErroViewModel e = new ErroViewModel();
                e.Mensagem = "O Utilizador não pode eliminar o seu registo!";
                e.Controller = "Utilizadores";

                return View("Erro", e);

            }

            var empresa = await _context.Empresas.Where(e => e.Id == utilizador.EmpresaId).FirstOrDefaultAsync();
            if (empresa == null)
            {
                return NotFound();
            }
            utilizador.empresa = empresa;


            return View(utilizador);
        }

    }
}
