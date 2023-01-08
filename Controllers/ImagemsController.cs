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
using PWEB.ViewModel;

namespace PWEB.Controllers
{
    public class ImagemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Utilizador> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public ImagemsController(ApplicationDbContext context
            , UserManager<Utilizador> userManager
            , RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: Imagems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Imagens.Include(i => i.Recolha);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Imagems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Imagens == null)
            {
                return NotFound();
            }

            var imagem = await _context.Imagens
                .Include(i => i.Recolha)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imagem == null)
            {
                return NotFound();
            }

            return View(imagem);
        }

        // GET: Imagems/Create
        public IActionResult Create(int rId)
        {
            ViewData["RecolhaId"] = new SelectList(_context.Recolhas, "Id", "Id",rId);
            return View();
        }

        // POST: Imagems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile ImgFile2 ,[Bind("Id,Name,Observaçoes,imagem,RecolhaId")] Imagem I)
        {


            ModelState.Remove(nameof(I.Recolha));
            if (ModelState.IsValid)
            {
                 // verificar se o ficheiro não é demasiado grande ou formatos nao suportado

                if (ImgFile2 != null)
                {
                    if (ImgFile2.Length > (300 * 1024))
                    {
                        ErroViewModel e = new ErroViewModel();
                        e.Mensagem = "Ficheiro demasiado grande";
                        e.Controller = "Imagems";

                        return View("Erro", e);
                    }
                    // método a implementar – verifica se a extensão é .png,.jpg,.jpeg
                    var nomeFile = ImgFile2.FileName;
                    if (nomeFile.Contains(".PNG") && nomeFile.Contains(".JPG") && nomeFile.Contains(".JPEG"))
                    {
                        ErroViewModel e = new ErroViewModel();
                        e.Mensagem = "Formato não suportado ( Suportados: .png,.jpg,.jpeg)";
                        e.Controller = "Imagems";

                        return View("Erro", e);
                    }

                    // converter o ficheiro em array de byte
                    var dataStream = new MemoryStream();
                    await ImgFile2.CopyToAsync(dataStream);

                    I.imagem = dataStream.ToArray();

                }

                // primeiro adicionar a imagem a BD
                _context.Add(I);
                await _context.SaveChangesAsync();

                // adicionar a imagem a recolhas
                if (_context.Recolhas == null)
                {
                    return NotFound();
                }

                var r = await _context.Recolhas 
                    .Include(r => r.Empregado)
                    .FirstOrDefaultAsync(r => r.Id == I.RecolhaId);

                _context.Update(r);

                // por fin guardar as akteraçoes na BD
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));
            }
            ViewData["RecolhaId"] = new SelectList(_context.Recolhas, "Id", "Id", I.RecolhaId);
            return View(I);
        }

        // GET: Imagems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Imagens == null)
            {
                return NotFound();
            }

            var imagem = await _context.Imagens.FindAsync(id);
            if (imagem == null)
            {
                return NotFound();
            }
            ViewData["RecolhaId"] = new SelectList(_context.Recolhas, "Id", "Id", imagem.RecolhaId);
            return View(imagem);
        }

        // POST: Imagems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Observaçoes,imagem,RecolhaId")] Imagem imagem)
        {
            if (id != imagem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imagem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImagemExists(imagem.Id))
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
            ViewData["RecolhaId"] = new SelectList(_context.Recolhas, "Id", "Id", imagem.RecolhaId);
            return View(imagem);
        }

        // GET: Imagems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Imagens == null)
            {
                return NotFound();
            }

            var imagem = await _context.Imagens
                .Include(i => i.Recolha)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imagem == null)
            {
                return NotFound();
            }

            return View(imagem);
        }

        // POST: Imagems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Imagens == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Imagens'  is null.");
            }
            var imagem = await _context.Imagens.FindAsync(id);
            if (imagem != null)
            {
                _context.Imagens.Remove(imagem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImagemExists(int id)
        {
          return _context.Imagens.Any(e => e.Id == id);
        }
    }
}
