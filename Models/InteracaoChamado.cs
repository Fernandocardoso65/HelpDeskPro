using System;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskWeb.Models
{
    public class InteracaoChamado
    {
        public int Id { get; set; }

        [Required]
        public int ChamadoId { get; set; }

        [Required]
        public string Mensagem { get; set; } = string.Empty;

        public DateTime Data { get; set; } = DateTime.Now;

        public string? UsuarioId { get; set; }

        public string? UsuarioEmail { get; set; }

        // Ex.: Administrador, Tecnico ou Usuario
        public string? Perfil { get; set; }

        public Chamado? Chamado { get; set; }
    }
}
