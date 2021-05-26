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

        public DbContextOperacao() 
        {
        
        }
    }


}
