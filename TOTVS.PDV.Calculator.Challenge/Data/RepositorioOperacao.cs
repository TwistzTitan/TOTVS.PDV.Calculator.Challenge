using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TOTVS.PDV.Calculator.Challenge.Model;

namespace TOTVS.PDV.Calculator.Challenge.Data
{
    public class RepositorioOperacao : IRepository<Operacao>
    {

        private DbContextOperacao _contexto;

        public Operacao Obter(int id)
        {
            Operacao operacao;

            using (_contexto = new DbContextOperacao()) 
            {
                operacao = _contexto.Operacoes.Where(op => op.OperacaoId == id).SingleOrDefault();    
            }
           
           return operacao;
        }
        
        public List<Operacao>ObterTodos() 
        {

            using (_contexto = new DbContextOperacao())
            {
                
                try
                {
                    return _contexto.Operacoes.ToList();
                    
                }
                catch
                {
                    return new List<Operacao>();
                }
            }


        }
        
        public bool Registrar(Operacao op) 
        {
            
            using (_contexto = new DbContextOperacao()) 
            {
                _contexto.Operacoes.Add(op);

                try 
                {
                    _contexto.SaveChanges();
                    return true;
                }
                catch 
                {
                    return false;
                }
            }

            
        }

        
    }
}
