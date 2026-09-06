using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using HelpDeskWeb.Data;
using HelpDeskWeb.Models;

namespace HelpDeskWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(
            ILogger<HomeController> logger,
            AppDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Visitante vê a landing page pública
            if (User.Identity?.IsAuthenticated != true)
            {
                return View();
            }

            // Administrador vai direto para o painel administrativo antigo
            if (User.IsInRole("Administrador"))
            {
                return RedirectToAction(
                    "Index",
                    "Admin"
                );
            }

            var usuario = await _userManager.GetUserAsync(User);

            if (usuario == null)
            {
                return View();
            }

            IQueryable<Chamado> chamados;

            // Técnico vê apenas chamados atribuídos a ele
            if (User.IsInRole("Tecnico"))
            {
                chamados = _context.Chamados
                    .Where(c => c.TecnicoId == usuario.Id);
            }
            // Usuário comum vê apenas os próprios chamados
            else
            {
                chamados = _context.Chamados
                    .Where(c => c.UsuarioId == usuario.Id);
            }

            ViewBag.TotalChamados = chamados.Count();

            ViewBag.Abertos = chamados
                .Count(c => c.Status == "Aberto");

            ViewBag.EmAtendimento = chamados
                .Count(c => c.Status == "Em atendimento");

            ViewBag.Resolvidos = chamados
                .Count(c => c.Status == "Resolvido");

            ViewBag.ChamadosRecentes = chamados
                .OrderByDescending(c => c.DataAbertura)
                .Take(5)
                .ToList();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId =
                        Activity.Current?.Id ??
                        HttpContext.TraceIdentifier
                }
            );
        }
    }
}
