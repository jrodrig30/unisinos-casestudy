
using Microsoft.EntityFrameworkCore;
using System;
using Unisinos.CaseStudy.Data.Models;

namespace Unisinos.CaseStudy.Data
{
    public class BemPromotoraContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public BemPromotoraContext(DbContextOptions<BemPromotoraContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        // protected override void OnModelCreating(ModelBuilder modelBuilder) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Proposta> Propostas { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<PropostaHistorico> PropostaHistoricos { get; set; }
    }
}
