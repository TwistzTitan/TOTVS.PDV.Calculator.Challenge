using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using TOTVS.PDV.Calculator.Challenge.Model;

namespace TOTVS.PDV.Calculator.Challenge.Data
{
    
    public class DbContextOperacao :  DbContext
    {
        
        public DbSet<Operacao> Operacoes { get; set; }

        public DbSet<PDVCalculadora> PDVCalculadoras { get; set; }

        public DbContextOperacao() : base("PDVCalculadora")
        {
            Database.SetInitializer<DbContextOperacao>(new CreateDatabaseIfNotExists<DbContextOperacao>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Fluent Map Operacao

            modelBuilder.Entity<Operacao>().HasIndex(op => op.OperacaoId).IsUnique();
            
            modelBuilder.Entity<Operacao>().Property(op => op.ValorPago).IsRequired().HasColumnName("VALORPAGO");
            
            modelBuilder.Entity<Operacao>().Property(op => op.ValorTotal).IsRequired().HasColumnName("VALORTOTAL");
            
            modelBuilder.Entity<Operacao>().Property(op => op.ValorTroco).HasColumnName("VALORTROCO");
            
            modelBuilder.Entity<Operacao>().Property(op => op.NomeOperador).IsRequired().HasMaxLength(60).HasColumnName("NOMEOPERADOR");
          
            modelBuilder.Entity<Operacao>().HasRequired<PDVCalculadora>(op => op.PDVCalculadora).WithMany( pdv => pdv.Operacoes).HasForeignKey(op => op.PDVCalculadoraId);

            #endregion

            #region Fluent Map PDVCalculadora
            
            modelBuilder.Entity<PDVCalculadora>().HasIndex(pdv => pdv.PDVCalculadoraId).IsUnique().HasName("PDVCALCULADORAID");

            #endregion

        }

    }


}
