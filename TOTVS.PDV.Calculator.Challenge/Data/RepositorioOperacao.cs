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


        public Operacao ObterOperacao(int id)
        {
            Operacao operacao;

            using (_contexto = new DbContextOperacao()) 
            {
                operacao = _contexto.Operacoes.Where(op => op.OperacaoId == id).SingleOrDefault();    
            }
           
           return operacao;
        }
        

        public bool RegistrarOperacao(Operacao op) 
        {
            int saved = 0;
            
            using (_contexto = new DbContextOperacao()) 
            {
                _contexto.Operacoes.Add(op);
                saved = _contexto.SaveChanges();
            }

            return saved == 0 ? false : true;
        }


        public List<Operacao> FindAll()
        {
               throw new NotImplementedException();
        }
    }
}
