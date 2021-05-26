using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TOTVS.PDV.Calculator.Challenge.Data
{
    public interface IRepository<T>
    {
        public T Obter(int i);

        public bool Registrar(T ob);

        public List<T> ObterTodos();
    }
}
