using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TOTVS.PDV.Calculator.Challenge.Data
{
    public interface IRepository
    {
        public void Obter<T>(int i);

        public void Registrar<T>(T ob);

    }
}
