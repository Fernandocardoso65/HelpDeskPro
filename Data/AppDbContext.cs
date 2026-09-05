using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HelpDeskWeb.Models;

namespace HelpDeskWeb.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Chamado> Chamados { get; set; }

        public DbSet<InteracaoChamado> InteracoesChamados { get; set; }
    }
}
