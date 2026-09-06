using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using HelpDeskWeb.Data;
using HelpDeskWeb.Models;
using HelpDeskWeb.Helpers;

namespace HelpDeskWeb.Controllers
{
    [Authorize]
    public class ChamadosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ChamadosController(
            AppDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // LISTAR CHAMADOS
        public async Task<IActionResult> Index()
        {
            var usuario = await _userManager.GetUserAsync(User);

            if (usuario == null)
            {
                return Challenge();
            }

            if (User.IsInRole("Administrador"))
            {
                var todosChamados = _context.Chamados
                    .OrderByDescending(c => c.DataAbertura)
                    .ToList();

                return View(todosChamados);
            }

            if (User.IsInRole("Tecnico"))
            {
                var chamadosTecnico = _context.Chamados
                    .Where(c => c.TecnicoId == usuario.Id)
                    .OrderByDescending(c => c.DataAbertura)
                    .ToList();

                return View(chamadosTecnico);
            }

            var meusChamados = _context.Chamados
                .Where(c => c.UsuarioId == usuario.Id)
                .OrderByDescending(c => c.DataAbertura)
                .ToList();

            return View(meusChamados);
        }

        // ABRIR TELA DE NOVO CHAMADO
        [HttpGet]
        [Authorize(Roles = "Usuario,Administrador")]
        public IActionResult Criar()
        {
            return View();
        }

        // SALVAR NOVO CHAMADO
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Usuario,Administrador")]
        public async Task<IActionResult> Criar(Chamado chamado)
        {
            if (!ModelState.IsValid)
            {
                return View(chamado);
            }

            var usuario = await _userManager.GetUserAsync(User);

            if (usuario == null)
            {
                return Challenge();
            }

            chamado.Status = "Aberto";
            chamado.DataAbertura = HorarioBrasil.Agora();

            chamado.UsuarioId = usuario.Id;
            chamado.UsuarioEmail = usuario.Email;

            chamado.TecnicoId = null;
            chamado.TecnicoEmail = null;

            chamado.Solucao = null;
            chamado.DataConclusao = null;

            _context.Chamados.Add(chamado);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // VER DETALHES + CARREGAR HISTÓRICO
        public async Task<IActionResult> Detalhes(int id)
        {
            var chamado = _context.Chamados.Find(id);

            if (chamado == null)
            {
                return NotFound();
            }

            var usuario = await _userManager.GetUserAsync(User);

            if (usuario == null)
            {
                return Challenge();
            }

            if (User.IsInRole("Tecnico") &&
                chamado.TecnicoId != usuario.Id)
            {
                return Forbid();
            }

            if (!User.IsInRole("Administrador") &&
                !User.IsInRole("Tecnico") &&
                chamado.UsuarioId != usuario.Id)
            {
                return Forbid();
            }

            var interacoes = _context.InteracoesChamados
                .Where(i => i.ChamadoId == chamado.Id)
                .OrderBy(i => i.Data)
                .ToList();

            ViewBag.Interacoes = interacoes;

            return View(chamado);
        }

        // ADICIONAR INTERAÇÃO AO HISTÓRICO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarInteracao(
            int chamadoId,
            string mensagem)
        {
            if (string.IsNullOrWhiteSpace(mensagem))
            {
                return RedirectToAction(
                    nameof(Detalhes),
                    new { id = chamadoId }
                );
            }

            var chamado = _context.Chamados.Find(chamadoId);

            if (chamado == null)
            {
                return NotFound();
            }

            var usuario = await _userManager.GetUserAsync(User);

            if (usuario == null)
            {
                return Challenge();
            }

            // TÉCNICO só pode interagir em chamado atribuído a ele
            if (User.IsInRole("Tecnico") &&
                chamado.TecnicoId != usuario.Id)
            {
                return Forbid();
            }

            // USUÁRIO só pode interagir no próprio chamado
            if (!User.IsInRole("Administrador") &&
                !User.IsInRole("Tecnico") &&
                chamado.UsuarioId != usuario.Id)
            {
                return Forbid();
            }

            string perfil;

            if (User.IsInRole("Administrador"))
            {
                perfil = "Administrador";
            }
            else if (User.IsInRole("Tecnico"))
            {
                perfil = "Tecnico";
            }
            else
            {
                perfil = "Usuario";
            }

            var interacao = new InteracaoChamado
            {
                ChamadoId = chamado.Id,
                Mensagem = mensagem.Trim(),
                Data = HorarioBrasil.Agora(),
                UsuarioId = usuario.Id,
                UsuarioEmail = usuario.Email,
                Perfil = perfil
            };

            _context.InteracoesChamados.Add(interacao);
            await _context.SaveChangesAsync();

            return RedirectToAction(
                nameof(Detalhes),
                new { id = chamado.Id }
            );
        }

        // ABRIR TELA DE EDIÇÃO
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var chamado = _context.Chamados.Find(id);

            if (chamado == null)
            {
                return NotFound();
            }

            var usuario = await _userManager.GetUserAsync(User);

            if (usuario == null)
            {
                return Challenge();
            }

            if (User.IsInRole("Administrador"))
            {
                return View(chamado);
            }

            if (User.IsInRole("Tecnico"))
            {
                if (chamado.TecnicoId != usuario.Id)
                {
                    return Forbid();
                }

                return RedirectToAction(
                    nameof(Atender),
                    new { id = chamado.Id }
                );
            }

            if (chamado.UsuarioId != usuario.Id)
            {
                return Forbid();
            }

            return View(chamado);
        }

        // SALVAR EDIÇÃO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Chamado chamado)
        {
            if (!ModelState.IsValid)
            {
                return View(chamado);
            }

            var chamadoBanco = _context.Chamados.Find(chamado.Id);

            if (chamadoBanco == null)
            {
                return NotFound();
            }

            var usuario = await _userManager.GetUserAsync(User);

            if (usuario == null)
            {
                return Challenge();
            }

            if (User.IsInRole("Administrador"))
            {
                chamadoBanco.Titulo = chamado.Titulo;
                chamadoBanco.Descricao = chamado.Descricao;
                chamadoBanco.Prioridade = chamado.Prioridade;
                chamadoBanco.Status = chamado.Status;

                if (chamadoBanco.Status == "Resolvido" &&
                    chamadoBanco.DataConclusao == null)
                {
                    chamadoBanco.DataConclusao = HorarioBrasil.Agora();
                }

                if (chamadoBanco.Status != "Resolvido")
                {
                    chamadoBanco.DataConclusao = null;
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            if (User.IsInRole("Tecnico"))
            {
                return RedirectToAction(
                    nameof(Atender),
                    new { id = chamado.Id }
                );
            }

            if (chamadoBanco.UsuarioId != usuario.Id)
            {
                return Forbid();
            }

            chamadoBanco.Titulo = chamado.Titulo;
            chamadoBanco.Descricao = chamado.Descricao;
            chamadoBanco.Prioridade = chamado.Prioridade;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // TELA DE ATENDIMENTO DO TÉCNICO
        [HttpGet]
        [Authorize(Roles = "Tecnico")]
        public async Task<IActionResult> Atender(int id)
        {
            var chamado = _context.Chamados.Find(id);

            if (chamado == null)
            {
                return NotFound();
            }

            var tecnico = await _userManager.GetUserAsync(User);

            if (tecnico == null)
            {
                return Challenge();
            }

            if (chamado.TecnicoId != tecnico.Id)
            {
                return Forbid();
            }

            return View(chamado);
        }

        // SALVAR ATENDIMENTO
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Tecnico")]
        public async Task<IActionResult> Atender(
            int id,
            string status,
            string? solucao)
        {
            var chamado = _context.Chamados.Find(id);

            if (chamado == null)
            {
                return NotFound();
            }

            var tecnico = await _userManager.GetUserAsync(User);

            if (tecnico == null)
            {
                return Challenge();
            }

            if (chamado.TecnicoId != tecnico.Id)
            {
                return Forbid();
            }

            chamado.Status = status;
            chamado.Solucao = solucao;

            if (status == "Resolvido")
            {
                chamado.DataConclusao = HorarioBrasil.Agora();
            }
            else
            {
                chamado.DataConclusao = null;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(
                nameof(Detalhes),
                new { id = chamado.Id }
            );
        }

        // ABRIR TELA DE ATRIBUIÇÃO
        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Atribuir(int id)
        {
            var chamado = _context.Chamados.Find(id);

            if (chamado == null)
            {
                return NotFound();
            }

            var tecnicos =
                await _userManager.GetUsersInRoleAsync("Tecnico");

            ViewBag.Tecnicos = tecnicos;

            return View(chamado);
        }

        // ATRIBUIR CHAMADO AO TÉCNICO
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Atribuir(
            int id,
            string tecnicoId)
        {
            var chamado = _context.Chamados.Find(id);

            if (chamado == null)
            {
                return NotFound();
            }

            var tecnico =
                await _userManager.FindByIdAsync(tecnicoId);

            if (tecnico == null)
            {
                return NotFound();
            }

            if (!await _userManager.IsInRoleAsync(
                    tecnico,
                    "Tecnico"))
            {
                return BadRequest();
            }

            chamado.TecnicoId = tecnico.Id;
            chamado.TecnicoEmail = tecnico.Email;
            chamado.Status = "Em atendimento";

            await _context.SaveChangesAsync();

            return RedirectToAction(
                nameof(Detalhes),
                new { id = chamado.Id }
            );
        }

        // TELA DE CONFIRMAÇÃO PARA EXCLUIR
        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public IActionResult Excluir(int id)
        {
            var chamado = _context.Chamados.Find(id);

            if (chamado == null)
            {
                return NotFound();
            }

            return View(chamado);
        }

        // EXCLUIR DO BANCO
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ConfirmarExclusao(int id)
        {
            var chamado = _context.Chamados.Find(id);

            if (chamado == null)
            {
                return NotFound();
            }

            // Remove o histórico antes de remover o chamado
            var interacoes = _context.InteracoesChamados
                .Where(i => i.ChamadoId == chamado.Id)
                .ToList();

            _context.InteracoesChamados.RemoveRange(interacoes);
            _context.Chamados.Remove(chamado);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
