using System;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskWeb.Models
{
    public class Chamado
    {
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        [Required]
        public string Status { get; set; } = "Aberto";

        [Required]
        public string Prioridade { get; set; } = string.Empty;

        public DateTime DataAbertura { get; set; } = DateTime.Now;

        // USUÁRIO QUE ABRIU O CHAMADO
        public string? UsuarioId { get; set; }

        public string? UsuarioEmail { get; set; }

        // TÉCNICO RESPONSÁVEL PELO CHAMADO
        public string? TecnicoId { get; set; }

        public string? TecnicoEmail { get; set; }

        // ATENDIMENTO TÉCNICO
        public string? Solucao { get; set; }

        public DateTime? DataConclusao { get; set; }
    }
}
